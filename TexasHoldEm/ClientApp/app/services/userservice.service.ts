import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class UserService {
    AppUrl: string = "";

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.AppUrl = baseUrl;
    }

    FetchAllUsers() {
        return this._http.get(this.AppUrl + 'api/User/Index')
            .map((response: Response) => response.json())
            .catch(this.errorHandler);
    }

    InsertUser(user) {
        return this._http.post(this.AppUrl + 'api/User/Create', user)
            .map((response: Response) => response.json)
            .catch(this.errorHandler);
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}