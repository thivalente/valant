import { Component, OnInit } from '@angular/core';

import { LoggingService } from '../../logging/logging.service';
import { MazeService } from './maze.service';
import { ApiResponse } from '../../api-client/api-response.model';
import { MazeCurrent } from './_models/maze-current.model';
import { MazeStatusEnum } from './_models/maze-status.enum';

@Component({ selector: 'app-maze', templateUrl: './maze.component.html', styleUrls: ['./maze.component.less'] })
export class MazeComponent implements OnInit {
  public current: MazeCurrent;
  public mazeList: string[][] = [];
  public mazeStatus = MazeStatusEnum;

  constructor(private mazeService: MazeService, private logger: LoggingService) { }

  ngOnInit(): void {
    this.buildMaze();
  }

  private buildMaze() {
    this.mazeService.getMaze().subscribe({
      next: (res: ApiResponse<string[][]>) => {
        if (!res || !res.success) {
          this.logger.error('Erro loading maze: Maze is empty or not found');
          return;
        }

        this.mazeList = res.result;
        this.current = { column: 0, row: 0 };
      },
      error: (error) => {
        this.logger.error('Error getting maze: ', error);
      }
    });
  }
}