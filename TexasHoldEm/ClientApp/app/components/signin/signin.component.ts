import { Component, OnInit, Inject, DoCheck } from '@angular/core';
import { Router, Route, RouterLink } from '@angular/router';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'sign-up',
    templateUrl: './signin.component.html',
})
export class SignInComponent implements OnInit, DoCheck {
    private form: FormGroup;
    private credentials: {
        username: string,
        password: string
    };

    title: string;


    constructor(private router: Router,
                private authService: AuthService,
                private formBuilder: FormBuilder,
                @Inject('BASE_URL') private baseUrl: string) {
        this.title = "Sign in";
        this.credentials = {username: "", password: ""};
    }

    ngOnInit() {
        this.form = this.formBuilder.group({
            username: [null, Validators.required],
            password: [null, Validators.required]
        });
    }

    ngDoCheck() {
    }

    onSubmit() {
        this.authService.login(this.credentials);
    }
}

