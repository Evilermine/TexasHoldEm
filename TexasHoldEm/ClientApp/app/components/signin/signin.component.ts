import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'sign-up',
    templateUrl: './signin.component.html',
    providers: [AuthService]
})
export class SignInComponent implements OnInit {
    private form: FormGroup;
    private username: string;
    private password: string;

    title: string;


    constructor(private router: Router,
                private authService: AuthService,
                private formBuilder: FormBuilder,
                @Inject('BASE_URL') private baseUrl: string) {
        this.title = "Sign in";
    }

    ngOnInit() {
        this.form = this.formBuilder.group({
            username: [null, Validators.required],
            password: [null, Validators.required]
        });
    }

    onSubmit() {
        console.log(this.username);
        this.authService.login(this.username, this.password);
    }
}

