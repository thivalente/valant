import { Observable, throwError } from "rxjs";
import { ValantDemoApiClient } from "../api-client/api-client";

export const throwException = (message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> => {
    if (result !== null && result !== undefined)
        return throwError(result);
    else
        return throwError(new ValantDemoApiClient.ApiException(message, status, response, headers, null));
}

export const blobToText = (blob: any): Observable<string> => {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}