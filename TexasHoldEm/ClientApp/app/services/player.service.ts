import { Injectable} from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import sha256, { Hash, HMAC } from 'fast-sha256';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';

@Injectable()
export class PlayerService {
    public PlayerList: Observable<Player[]> ;
    private _PlayerList: BehaviorSubject<Player[]>;
    private baseUrl: string = "";
    private dataStore: {
        PlayerList: Player[];
    };
    private cards: string[];

    constructor( private http: HttpClient) {
        this.baseUrl = '/api/';
        this.dataStore = { PlayerList: [] };
        this._PlayerList = <BehaviorSubject<Player[]>> new BehaviorSubject([]);
        this.PlayerList = this._PlayerList.asObservable();
    }

    FetchAllPlayers() {
        this.http.get(this.baseUrl + 'GetAllPlayers')
            .map((response: Response) => response.json())
            .subscribe(data => {
                this.dataStore.PlayerList = data;
                this._PlayerList.next(Object.assign({}, this.dataStore).PlayerList);
            }, error => console.log('could not load Player'));
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }
}

export class Player {
    username: string;
    password: string;
    firstname: string;
    lastname: string;
    email: string;
    wallet: number;
}