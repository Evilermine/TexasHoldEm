import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { PlayerService } from './PlayerService.service';

@Injectable()
export class GameService {
    private baseUrl: string;
    public pot: number = 0;
    private playerTurn: number;
    private playerNumber: number;


    constructor(private http: HttpClient, private playerService: PlayerService) {
        this.baseUrl = '/api/';
    }

    async onAction(bid: number) {
        console.log("onBid()");

        var data = {
            action: bid,
            user: "EvilErmine"
        };

        await this.http.post(this.baseUrl + 'onAction/', data)
            .subscribe(data => {
            }, error => ("Incorrect value or not enough funds"));

        await this.http.post<number>(this.baseUrl + 'BidPlayer/', data)
            .subscribe(data => {
                bid = data;
            }, error => ("incorrect value or not enough funds"));

        this.pot += bid;
    }

    async getPot() {
        let pot: number = 0;

        await this.http.get<number>(this.baseUrl + 'getPot')
            .subscribe(data => {
                pot = data;
            });

        return pot;
    }

    async fetchPlayerTurn() {

        do {
            await this.http.get<number>(this.baseUrl + 'getPlayerTurn')
                .subscribe(p => this.playerTurn = p);
        } while(this.playerTurn != this.playerNumber);
    }

    getPlayerTurn(): boolean {
        if (this.playerTurn == this.playerNumber)
            return true;

        return false;
    }

    getCards(): string[] {
        this.playerService.setCards();
        return this.playerService.getCards();
    }
}