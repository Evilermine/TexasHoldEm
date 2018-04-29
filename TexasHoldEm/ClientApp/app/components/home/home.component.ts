import { Component, OnInit, OnChanges, DoCheck, SimpleChanges } from '@angular/core';
import { GameService } from '../../services/game.service';
import { AuthService } from '../../services/auth.service';
import { Player } from '../../services/PlayerService.service';
import { Observable } from 'rxjs';

import { Http, Response } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styles: ['./home.component.css'],
    providers: [AuthService]
})
export class HomeComponent implements OnInit, OnChanges, DoCheck {
    private img_layout: string;
    private bid: number;
    private user: Player;
    private cards: string[];
    private playerTurn: boolean;

    constructor(private gameService: GameService, private authService: AuthService) {
        this.img_layout = "../../../assets/texas_holdem_layout.png";
        this.bid = 0;
    }

    ngOnInit() {
    //    while (this.gameService.getPlayers() == 0) {
            
    //    }
        this.cards = this.fetchCards();

        if (this.cards == null) {
            return;
        }
    }

    ngOnChanges(changes: SimpleChanges) {
    }

    ngDoCheck() {
        if (this.playerTurn != this.gameService.getPlayerTurn())
            return; 
    }

    onBid() {
        this.user = this.authService.getCurrentUser();
        this.gameService.onAction(this.bid);
        console.log(this.gameService.pot);
    }

    onCheck() {
        this.bid = 0;
        this.gameService.onAction(this.bid);
    }

    onAllIn() {
        this.bid = this.user.wallet;
        this.gameService.onAction(this.bid);
    }

    onFold() {
        this.bid = -1;
        this.gameService.onAction(this.bid);
    }

    fetchCards(): string[] { return this.gameService.getCards(); }

    incrementBid() { this.bid += 20; }

    decrementBid() { this.bid -= 20; }
}
