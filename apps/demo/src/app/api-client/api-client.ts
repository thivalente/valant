import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

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

            return this.http.request("get", url_, options_);
        }
    }

    export class ApiException extends Error {
        message: string;
        status: number;
        response: string;
        headers: { [key: string]: any; };
        result: any;

        constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
            super();

            this.message = message;
            this.status = status;
            this.response = response;
            this.headers = headers;
            this.result = result;
        }

        protected isApiException = true;

        static isApiException(obj: any): obj is ApiException {
            return obj.isApiException === true;
        }
    }
}