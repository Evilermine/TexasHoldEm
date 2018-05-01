import { HttpClient, HttpResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Player } from './player.service';

@Injectable()
export class GameService {
    private baseUrl: string;
    public pot: number = 0;
    private playerTurn: number;
    private playerNumber: number;
    public cards: string[];


    constructor(private http: HttpClient, private authService: AuthService) {
        this.baseUrl = '/api/';
    }

    async onAction(bid: number) {

        var data = {
            action: bid,
            user: this.authService.getCurrentUser().username
        };

        console.log(data);

        await this.http.post(this.baseUrl + 'action/', data)
            .subscribe(data => {
            }, error => ("Incorrect value or not enough funds"));

        if (bid > 0) {
            await this.http.post<Player>(this.baseUrl + 'BidPlayer/', data)
                .subscribe(data => {
                    this.authService.currentUser = data;
                }, error => ("incorrect value or not enough funds"));

            this.pot += bid;
        }
    }

    async getRiver() {

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

     setCards(){
        var user = {
            username: "EvilErmine"
        };

        var headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        let params = new HttpParams().set("username", "EvilErmine");

        return this.http.get<string[]>(this.baseUrl + 'GetCard/EvilErmine', { headers: headers, params: params })
            .subscribe(p => this.cards = p);
    }
}