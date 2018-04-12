import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/userservice.service';

@Component({
    selector: 'sign-up',
    templateUrl: './signup.component.html'
})

export class SignupComponent implements OnInit {
    UserForm: FormGroup;
    title: string = "Sign up";
    id: number;
    errorMessage: any;

    constructor(private form: FormBuilder, private route: ActivatedRoute,
        private userService: UserService, private router: Router) {

        if (this.route.snapshot.params["id"]) {
            this.id = this.route.snapshot.params["id"];
        }

        this.UserForm = this.form.group({
            username: ['', Validators.required],
            wallet: ['', Validators.required]
        })
    }

    ngOnInit() {

    }

    save() {
        if (!this.UserForm.valid)
            return;

            this.userService.InsertUser(this.UserForm.value)
                .subscribe((data) => {
                    this.router.navigate(['/FetchAll']);
                }, error => this.errorMessage = error)
    }

    cancel() {
        this.router.navigate(['/FetchAll']);
    }

    getUsername() { return this.UserForm.get('username'); }
    getWallet() { return this.UserForm.get('wallet'); }
}