import { Component } from '@angular/core';
import { GameService } from '../../services/game.service';

import { Http, Response } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    private img_layout: string;
    private bid: number;

    constructor(private gameService: GameService, private http:Http) {
        this.img_layout = "../../../assets/texas_holdem_layout.png";
        this.bid = 0;
    }

    onBid() {
        this.gameService.onBid(this.bid);
    }

    getCards() {
        this.gameService.getCards();
    }

    incrementBid() {
        this.bid += 20;
    }

    decrementBid() {
        this.bid -= 20;
    }
}
