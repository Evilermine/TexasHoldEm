﻿import { Http, Response, Headers } from '@angular/http';
import { Injectable, Inject } from '@angular/core';
import { PlayerService } from './PlayerService.service';

@Injectable()
export class GameService {
    private baseUrl: string;

    constructor(private http: Http, private playerService: PlayerService) {
        this.baseUrl = '/api/';
    }

    onBid(bid: number) {
        console.log("onBid()");

        var data = {
            action: bid,
            user: "EvilErmine"
        };

        this.http.post(this.baseUrl + 'onAction/', data)
            .map((res: Response) => res.json())
            .subscribe(data => { },
            error => ("Incorrect value or not enough funds"));

        this.http.post(this.baseUrl + 'BidPlayer/', data)
            .map((res: Response) => res.json())
            .subscribe(data => { bid = data; },
            error => ("incorrect value or not enough funds"));
    }

    getCards() {
        this.playerService.setCards();
    }
}