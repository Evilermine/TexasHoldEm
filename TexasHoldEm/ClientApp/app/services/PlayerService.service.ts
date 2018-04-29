﻿import { Injectable} from '@angular/core';
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

    public AddPlayer(player: Player) {
        console.log("Adding new Player to database");
        var headers = new HttpHeaders();

        headers.append('Content-Type', 'application/json; charset=utf-8');
        console.log("adding: " + player);

        this.http.post(this.baseUrl + 'InsertPlayer/', player, { headers: headers })
            .subscribe((data: Player) => {
                this.dataStore.PlayerList.push(data);
                this._PlayerList.next(Object.assign(Player, this.dataStore).PlayerList);
            }, error => console.log("could not create todo"));
    }

    errorHandler(error: Response) {
        console.log(error);
        return Observable.throw(error);
    }

    setCards() {

        var user = {
            username: "EvilErmine"
        };

        var headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        let params = new HttpParams().set("username", "EvilErmine");

        this.http.get<string[]>(this.baseUrl + 'GetCard/EvilErmine', { headers: headers, params: params })
            .subscribe(data => this.cards = data);

        console.log(this.cards);
    }

    getCards(): string[] {
        return this.cards;
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