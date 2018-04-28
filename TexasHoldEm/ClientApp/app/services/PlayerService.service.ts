import { Injectable} from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
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

    constructor(private _http: Http) {
        this.baseUrl = '/api/';
        this.dataStore = { PlayerList: [] };
        this._PlayerList = <BehaviorSubject<Player[]>> new BehaviorSubject([]);
        this.PlayerList = this._PlayerList.asObservable();
    }

    FetchAllPlayers() {
        this._http.get(this.baseUrl + 'GetAllPlayers')
            .map((response: Response) => response.json())
            .subscribe(data => {
                this.dataStore.PlayerList = data;
                this._PlayerList.next(Object.assign({}, this.dataStore).PlayerList);
            }, error => console.log('could not load Player'));
    }

    public AddPlayer(player: Player) {
        console.log("Adding new Player to database");
        var headers = new Headers();

        headers.append('Content-Type', 'application/json; charset=utf-8');
        console.log("adding: " + JSON.stringify(player));

        this._http.post(this.baseUrl + 'InsertPlayer/', JSON.stringify(player), { headers: headers })
            .map(response => response.json())
            .subscribe(data => {
                this.dataStore.PlayerList.push(data);
                this._PlayerList.next(Object.assign({}, this.dataStore).PlayerList);
            }, error => console.log("could not create todo"));
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