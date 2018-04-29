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
    public user: any;

    constructor(private http: HttpClient,
        @Inject(PLATFORM_ID) private platformId: any) {
        this.IsLoggedIn = false;
        this.datastore = { currentUser: new Player() };
        this._currentUser = <BehaviorSubject<Player>>new BehaviorSubject(new Player());
        this.currentUser = this._currentUser.asObservable();
        this.user = "asdfasdfasdf";
    }

    async login(username: string, password: string){

        var url = '/api/login';
       // password = crypto.enc.Base64.stringify(crypto.SHA512(password));

        var data = {
            username: username,
            password: password
        };

        var headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        let current: any;

        let response = await this.http.post<Player>(url, data, { headers: headers })
            .subscribe(p => {
                this.datastore.currentUser = p;
                this._currentUser.next(Object.assign({}, this.datastore).currentUser);
                console.log("test*:" + this.datastore.currentUser.username);
            }, error => console.log("wrong username or password"));

        console.log("Current user: " + this._currentUser.getValue().email);
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
        return this._currentUser.getValue();
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