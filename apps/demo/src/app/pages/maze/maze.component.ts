import { Component, OnDestroy, OnInit } from '@angular/core';

import { LoggingService } from '../../logging/logging.service';
import { MazeService } from './maze.service';
import { PersonalRecordService } from './personal-record.service';

import { ApiResponse } from '../../api-client/api-response.model';
import { Maze } from './_models/maze.model';
import { MazeCurrent, MazeStep } from './_models/maze-current.model';
import { MazeStatusEnum } from './_models/maze-status.enum';
import { MazeStateEnum } from './_models/maze-state.enum';
import { PersonalRecord } from './_models/personal-record.model';

@Component({ selector: 'app-maze', templateUrl: './maze.component.html', styleUrls: ['./maze.component.less'] })
export class MazeComponent implements OnInit, OnDestroy {
  private _countdownDefault: number = 3;
  public countdown: number = this._countdownDefault;
  public current: MazeCurrent;
  private intervalCountdownId: any;
  private intervalTimerId: any;
  public mazeList: MazeStep[][] = [];
  public selectedMazeId: string = '';
  public mazeStatusEnum = MazeStatusEnum;
  public mistakes: number = 0;
  public myMazes: Maze[] = [];
  public records: PersonalRecord[] = [];
  public stateEnum = MazeStateEnum;
  public state: MazeStateEnum = MazeStateEnum.Empty;

  public minutes: string = '00';
  public seconds: string = '00';
  private totalTimeInSeconds: number = 0;

  constructor(
    private mazeService: MazeService,
    private personalRecordService: PersonalRecordService,
    private logger: LoggingService) { }

  ngOnInit(): void {
    this.loadMazes();
    this.loadRecords();
  }

  ngOnDestroy() {
    if (this.intervalCountdownId) {
      this.stopCountdown();
    }

    if (this.intervalTimerId) {
      this.stopTimer();
    }

    document.removeEventListener('keydown', this.handleKeyDown);
  }

  private buildMaze(path: string[][]) {
    this.state = MazeStateEnum.Empty;
    this.mistakes = 0;
    this.countdown = 3;

    this.stopCountdown();
    this.stopTimer();

    const steps: MazeStep[][] = path.map(row => row.map(cell => ({ step: cell, walked: true })));

    this.state = MazeStateEnum.Loaded;
    this.mazeList = steps;
    this.current = { column: 0, row: 0 };
  }

  private formatTime(value: number): string {
    return value < 10 ? `0${value}` : `${value}`;
  }

  private handleKeyDown = (event: KeyboardEvent) => {
    const arrowKeys = ['ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight'];

    if (arrowKeys.includes(event.key)) {
      let isArrived = false;

      switch (event.key) {
        case 'ArrowDown':
          isArrived = this.walk(1, 0);
          break;
        case 'ArrowLeft':
          isArrived = this.walk(0, -1);
          break;
        case 'ArrowRight':
          isArrived = this.walk(0, 1);
          break;
        case 'ArrowUp':
          isArrived = this.walk(-1, 0);
          break;
      }

      if (isArrived) {
        this.setAsCompleted();
      }
    }
  };

  private isInvalidPath(row: number, column: number): boolean {
    return row < 0 || row >= this.mazeList.length || column < 0 || column >= this.mazeList[row].length || (this.mazeList[row][column].step !== this.mazeStatusEnum.Go && this.mazeList[row][column].step !== this.mazeStatusEnum.End);
  }

  private loadMazes() {
    this.mazeService.getAll().subscribe({
      next: (res: ApiResponse<Maze[]>) => {
        if (!res || !res.success) {
          this.logger.error('Erro loading my mazes');
          return;
        }

        this.myMazes = res.result;
      },
      error: (error) => {
        this.logger.error('Error getting my mazes: ', error);
      }
    });
  }

  private loadRecords() {
    this.personalRecordService.getTop5Records().subscribe({
      next: (res: ApiResponse<PersonalRecord[]>) => {
        if (!res || !res.success) {
          this.logger.error('Erro loading personal records');
          return;
        }

        this.records = res.result;
      },
      error: (error) => {
        this.logger.error('Error getting personal records: ', error);
      }
    });
  }

  selectMaze() {
    const id = this.selectedMazeId;

    this.mazeService.getById(id).subscribe({
      next: (res: ApiResponse<Maze>) => {
        if (!res || !res.success) {
          this.state = MazeStateEnum.Empty;
          this.logger.error('Erro loading maze: Maze is empty or not found');
          return;
        }

        this.buildMaze(res.result.path);
      },
      error: (error) => {
        this.logger.error('Error getting maze: ', error);
      }
    });
  }

  selectRandomMaze() {
    this.selectedMazeId = '';

    this.mazeService.getRandomMaze().subscribe({
      next: (res: ApiResponse<Maze>) => {
        if (!res || !res.success) {
          this.state = MazeStateEnum.Empty;
          this.logger.error('Erro loading maze: Maze is empty or not found');
          return;
        }

        this.buildMaze(res.result.path);
      },
      error: (error) => {
        this.logger.error('Error getting maze: ', error);
      }
    });
  }

  setAsWalked(row: number, column: number) {
    if (row >= 0 && column >= 0)
      this.mazeList[row][column].walked = true;
  }

  setAsCompleted() {
    document.removeEventListener('keydown', this.handleKeyDown);

    this.personalRecordService.add(this.mistakes, this.totalTimeInSeconds).subscribe({
      next: (res: ApiResponse<any>) => {
        this.stopTimer();
        this.state = MazeStateEnum.Completed;

        this.toogleMazeVisibility(true);

        this.loadRecords();
      },
      error: (error) => {
        this.logger.error('Error setting personal record: ', error);
      }
    });
    
    this.stopTimer();
    this.state = MazeStateEnum.Completed;

    this.toogleMazeVisibility(true);
  }

  start() {
    this.totalTimeInSeconds = 0;
    this.mistakes = 0;
    this.startCountdown();
  }

  private startCountdown() {
    this.state = MazeStateEnum.Starting;
    this.countdown = this._countdownDefault;

    this.intervalCountdownId = setInterval(() => {
      this.countdown--;

      if (this.countdown === 0) {
        this.stopCountdown();
        this.startTimer();
        
        this.toogleMazeVisibility(false);
        this.setAsWalked(0, 0);
        document.addEventListener('keydown', this.handleKeyDown);
      }

    }, 1000);
  }

  private startTimer() {
    this.state = MazeStateEnum.Started;

    this.intervalTimerId = setInterval(() => {
      this.totalTimeInSeconds++;
      this.updateTimeDisplay();
    }, 1000);
  }

  private stopCountdown() {
    clearInterval(this.intervalCountdownId);
  }

  private stopTimer() {
    this.updateTimeDisplay();
    clearInterval(this.intervalTimerId);
  }

  toogleMazeVisibility(show: boolean) {
    this.mazeList.forEach(r => r.forEach(c => c.walked = show));
  }

  private updateTimeDisplay() {
    const mins = Math.floor(this.totalTimeInSeconds / 60);
    const secs = this.totalTimeInSeconds % 60;
    this.minutes = this.formatTime(mins);
    this.seconds = this.formatTime(secs);
  }

  private walk(row: number, column: number): boolean {
    const nextRow = this.current.row + row;
    const nextColumn = this.current.column + column;

    if (this.isInvalidPath(nextRow, nextColumn)) {
      this.mistakes++;
      this.setAsWalked(nextRow, nextColumn);
      return false;
    }

    this.current.row = nextRow;
    this.current.column = nextColumn;
    this.setAsWalked(nextRow, nextColumn);

    return (this.mazeList[this.current.row][this.current.column].step === this.mazeStatusEnum.End);
  }
}