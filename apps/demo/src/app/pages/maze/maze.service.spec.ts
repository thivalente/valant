import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { MazeService } from './maze.service';

describe('MazeService', () => {
  let service: MazeService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(MazeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
