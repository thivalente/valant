import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ValantDemoApiClient } from '../../api-client/api-client';

import { ApiResponse } from '../../api-client/api-response.model';

@Injectable({ providedIn: 'root' })
export class PersonalRecordService {
    private _domain: string = 'personal-records';

    constructor(private valiantClient: ValantDemoApiClient.Client) {}

    public add(totalMistakes: number, totalSeconds: number): Observable<ApiResponse<any>> {
        const body = { totalMistakes, totalSeconds };
        return this.valiantClient.post(this._domain, body);
    }

    public getTop5Records(): Observable<ApiResponse<any>> {
        const url = `${this._domain}/top5`;
        return this.valiantClient.get(url);
    }
}