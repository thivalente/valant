import { Injectable } from '@angular/core';
import { HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { mergeMap, catchError } from 'rxjs/operators';
import { ValantDemoApiClient } from '../../api-client/api-client';

import { ApiResponse } from '../../api-client/api-response.model';
import { blobToText, throwException } from '../../_utils/utils';

@Injectable({
  providedIn: 'root',
})
export class MazeService {
  private _domain: string = 'maze';

  constructor(private valiantClient: ValantDemoApiClient.Client) {}

  public getMaze(): Observable<ApiResponse<any>> {
    return this.valiantClient.get(this._domain).pipe(mergeMap((response_ : any) => {
        return this.processMaze(response_);
    })).pipe(catchError((response_: any) => {
        if (response_ instanceof HttpResponseBase) {
            try {
                return this.processMaze(<any>response_);
            } catch (e) {
                return <Observable<ApiResponse<any>>><any>catchError(e);
            }
        } else
            return <Observable<ApiResponse<any>>><any>catchError(response_);
    }));
  }

  private processMaze(response: HttpResponseBase): Observable<ApiResponse<any>> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    
    if (status === 200) {
        return blobToText(responseBlob).pipe(mergeMap(_responseText => {
        let result200: any = null;
        result200 = _responseText === "" ? null : <ApiResponse<any>>JSON.parse(_responseText, this.valiantClient.jsonParseReviver);
        return of(result200);
        }));
    } else if (status !== 200 && status !== 204) {
        return blobToText(responseBlob).pipe(mergeMap(_responseText => {
        return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }));
    }
    return of<ApiResponse<any>>(<any>null);
  }
}

