import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

import { mergeMap as _observableMergeMap, catchError as _observableCatch, mergeMap, catchError } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf, of } from 'rxjs';

import { ApiResponse } from './api-response.model';
import { blobToText, throwException } from '../_utils/utils';

export module ValantDemoApiClient {
    export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

    @Injectable()
    export class Client {
        private http: HttpClient;
        private baseUrl: string;
        public jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

        constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
            this.http = http;
            this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
        }

        get(url: string): Observable<any> {
            let url_ = `${this.baseUrl}/${url}`;
            url_ = url_.replace(/[?&]$/, "");

            let options_ : any = {
                observe: "response",
                responseType: "blob",
                headers: new HttpHeaders({
                    "Accept": "text/plain"
                })
            };

            return this.http.request("get", url_, options_).pipe(mergeMap((response_ : any) => {
                return this.processResponse(response_);
            })).pipe(catchError((response_: any) => {
                if (response_ instanceof HttpResponseBase) {
                    try {
                        return this.processResponse(<any>response_);
                    } catch (e) {
                        return <Observable<ApiResponse<any>>><any>catchError(e);
                    }
                } else
                    return <Observable<ApiResponse<any>>><any>catchError(response_);
            }));
        }

        post(url: string, body: any): Observable<any> {
            let url_ = `${this.baseUrl}/${url}`;
            url_ = url_.replace(/[?&]$/, "");

            const options_ : any = { body: body, responseType: "json" };

            return this.http.request("post", url_, options_);
        }

        private processResponse(response: HttpResponseBase): Observable<ApiResponse<any>> {
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
                result200 = _responseText === "" ? null : <ApiResponse<any>>JSON.parse(_responseText, this.jsonParseReviver);
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
}