import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';
import { PlayerService, Player } from '../../services/PlayerService.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
    selector: 'sign-up',
    templateUrl: './signup.component.html',
    providers: [PlayerService]
})

export class SignupComponent implements OnInit {
    public playerList: Observable<Player[]>;
    showEditButton: true;
    myName: string;
    player: Player;
    title: string;

    form: FormGroup

    constructor(private playerService: PlayerService, private formBuilder: FormBuilder) {
        this.player = new Player();
        this.title = "Sign up";
    }

    ngOnInit() {
        console.log("in ngOnInit from SignupComponent");
        this.form = this.formBuilder.group({
            username: [null, Validators.required],
            password: [null, Validators.required],
            firstname: [null, Validators.required],
            lastname: [null, Validators.required],
            email: [null, [Validators.required, Validators.email]]
        });
        this.playerList = this.playerService.PlayerList;
        this.playerService.FetchAllPlayers();
    }

    public AddPlayer(item: Player) {
        let todoId = this.playerService.AddPlayer(item);
    }
}