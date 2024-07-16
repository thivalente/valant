import { Component, OnInit } from '@angular/core';
import { LoggingService } from './logging/logging.service';
import { MazeService } from './pages/maze/maze.service';

@Component({
  selector: 'valant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit {
  public title = 'Thiago Valente demo';
  public data: string[];

  constructor(private logger: LoggingService, private stuffService: MazeService) {}

  ngOnInit() {
    this.logger.log('Welcome to the Maze Demo');
  }
}
