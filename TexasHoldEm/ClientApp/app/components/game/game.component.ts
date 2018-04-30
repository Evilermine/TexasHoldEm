import { Component, OnInit, OnChanges, DoCheck, SimpleChanges } from '@angular/core';
import { GameService } from '../../services/game.service';
import { AuthService } from '../../services/auth.service';
import { Player } from '../../services/player.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit, DoCheck {
    public assets: string;
    public layout: string;
    private bid: number;
    private user: Player;
    private playerCardsUrl: string[];
    private cards: string[];
    private playerTurn: boolean;
    
    constructor(private gameService: GameService, private authService: AuthService) {
        this.layout = 'assets/texas_holdem_layout.png';
        this.bid = 0;
        this.playerCardsUrl = [];
    }

    ngOnInit() {
        this.fetchCards();
    }
    
    ngDoCheck() {
        if (this.cards != this.gameService.cards) {
            this.cards = this.gameService.cards;

            for (let card of this.gameService.cards) {
                this.playerCardsUrl.push('assets/cards/' + card);
            }
        }
    }

    onBid() {
        this.gameService.onAction(this.bid);
        this.bid = 0;
    }

    onCheck() {
        this.gameService.onAction(0);
        this.bid = 0;
    }

    onAllIn() {
        this.gameService.onAction(this.authService.currentUser.wallet);
        this.bid = 0;
    }

    onFold() {
        this.gameService.onAction(-1);
        this.bid = 0;
    }

    fetchCards(){
        this.gameService.setCards();
    }

    incrementBid() { this.bid += 20; }

    decrementBid() { this.bid -= 20; }
}
