import { EventEmitter, Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from '@angular/common';
import { Response, Headers, Http } from '@angular/http';

import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from "rxjs";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Player } from './PlayerService.service'
import * as crypto from 'crypto-js';
import 'rxjs/Rx';

@Injectable()
export class AuthService {
    IsLoggedIn: boolean
    public currentUser: Observable<Player>;
    private _currentUser: BehaviorSubject<Player>;
    private datastore: {
        currentUser: Player;
    };
    private user: any;

    constructor(private http: HttpClient,
        @Inject(PLATFORM_ID) private platformId: any) {
        this.IsLoggedIn = false;

    }

    login(username: string, password: string){

        var url = '/api/login';
       // password = crypto.enc.Base64.stringify(crypto.SHA512(password));

        var data = {
            username: username,
            password: password
        };

        var headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        this.http.post<Player>(url, data, { headers: headers })
            .subscribe(test => {
                this.user = JSON.stringify(test);
                this._currentUser.next(Object.assign({}, this.datastore).currentUser);
            }, error => console.log("wrong username or password"));


                console.log("Current user: " + this.user);
                // if the token is there, login has been successful
                if (this.currentUser != null) {
                    // store username and jwt token
                    this.IsLoggedIn = true;
                    // successful login
                
                }
    }

    logout() {
        this.IsLoggedIn = false;
    }
    
    isLoggedIn(): boolean {
        return this.IsLoggedIn;
    }   

    getCurrentUser(): Player {
        return this.datastore.currentUser;
    }

    onBid(data: any) {
        var headers = new HttpHeaders();

        headers.append('Content-Type', 'application/json; charset=utf-8');


        this.http.put<number>("api/EditPlayer", data, { headers: headers })
            .subscribe(data => {
                this.datastore.currentUser.wallet = data;
                this._currentUser.next(Object.assign({}, this.datastore).currentUser);
            });
    }
}