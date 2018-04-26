import { Component} from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    private img_layout: string;


    constructor() {
        this.img_layout = "../../../assets/texas_holdem_layout.png";
    }
}
