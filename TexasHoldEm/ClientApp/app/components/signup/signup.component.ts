import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs/Observable';
import { Player } from '../../services/player.service';
import { AuthService } from '../../services/auth.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import * as crypto from 'crypto-js';

@Component({
    selector: 'sign-up',
    templateUrl: './signup.component.html',
})

export class SignupComponent implements OnInit {
    public playerList: Observable<Player[]>;
    myName: string;
    player: Player;
    title: string;

    form: FormGroup

    constructor(private authService: AuthService, private formBuilder: FormBuilder) {
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
    }

    public AddPlayer(item: Player) {
       
        // item.password = crypto.enc.Base64.stringify(crypto.SHA512(item.password));
        let todoId = this.authService.AddPlayer(item);
    }
}