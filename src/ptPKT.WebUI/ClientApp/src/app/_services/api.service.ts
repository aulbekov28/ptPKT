import { Inject } from '@angular/core';
//import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';
import { Observable } from "rxjs";
import { HttpErrorResponse, HttpClient } from "@angular/common/http";
import { RequestOptions } from '@angular/http';

//@Injectable
export abstract class ApiService {
    constructor( @Inject('BASE_URL') private baseUrl: string, private http: HttpClient) {
    }

    protected get(relativeURL: string) : Observable<any> {
        return this.http.get(this.getUrl(relativeURL))
                        .pipe(map(res => res))
    }

    protected post(relativeURL: string, body: any, option?: RequestOptions) : Observable<any> {
        return this.http.post(this.getUrl(relativeURL), body);
    }

    protected delete(relativeURL: string) : Observable<any>{
        return this.http.delete(this.getUrl(relativeURL));
    }

    protected put(relativeURL: string, putData: any) : Observable<any> {
        return this.http.put(this.getUrl(relativeURL),putData)
    }

    private getUrl(relativeUrl: string) : string {
        return this.baseUrl + relativeUrl;
    }
    
    private handleReponse(res: Response) {
    }
}