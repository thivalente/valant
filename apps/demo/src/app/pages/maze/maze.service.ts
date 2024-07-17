import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ValantDemoApiClient } from '../../api-client/api-client';

import { ApiResponse } from '../../api-client/api-response.model';

@Injectable({
  providedIn: 'root',
})
export class MazeService {
  private _domain: string = 'mazes';

  constructor(private valiantClient: ValantDemoApiClient.Client) {}

  public add(name: string, path: string[][]) {
    const body = { name, path };
    return this.valiantClient.post(this._domain, body);
  }

  public getAll(): Observable<ApiResponse<any>> {
    const url = `${this._domain}`;
    return this.valiantClient.get(url);
  }

  public getById(id: string): Observable<ApiResponse<any>> {
    const url = `${this._domain}/${id}`;
    return this.valiantClient.get(url);
  }

  public getRandomMaze(): Observable<ApiResponse<any>> {
    const url = `${this._domain}/random`;
    return this.valiantClient.get(url);
  }
}

