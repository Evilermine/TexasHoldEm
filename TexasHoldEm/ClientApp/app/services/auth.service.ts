import { EventEmitter, Injectable} from "@angular/core";
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from "rxjs";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Player } from './player.service'
import * as crypto from 'crypto-js';
import 'rxjs/Rx';

@Injectable()
export class AuthService {
    public IsLoggedIn: boolean
    private _currentUser: BehaviorSubject<Player>;
    public currentUser: Player;
    private baseUrl: string;

    constructor(private http: HttpClient, private router: Router)
    {
        this.IsLoggedIn = false;
        this._currentUser = <BehaviorSubject<Player>>new BehaviorSubject(new Player());
        this.baseUrl = '/api/';
    }


    public AddPlayer(player: Player) {
        console.log("Adding new Player to database");
        var headers = new HttpHeaders();

        headers.append('Content-Type', 'application/json; charset=utf-8');
        console.log("adding: " + player);

        this.http.post(this.baseUrl + 'InsertPlayer/', player, { headers: headers })
            .subscribe((data: Player) => {
            }, error => console.log("could not create todo"));
    }

    login(credentials: any) {

        var headers = new HttpHeaders();
        headers.append('Content-Type', 'application/json; charset=utf-8');

        let response = this.http.post<Player>(this.baseUrl + 'login', credentials, { headers: headers })
            .subscribe(p => {
                this.router.navigate(['/home']);
                this.IsLoggedIn = true;
                this.currentUser = p;            
                
            }, error => console.log("wrong username or password"));
    }

    logout() {
        this.IsLoggedIn = false;
    }
    
    isLoggedIn(): boolean {
        return this.IsLoggedIn;
    }   

    getCurrentUser(): Player {
        return this.currentUser;
    }

    onBid(data: any) {
        var headers = new HttpHeaders();

        headers.append('Content-Type', 'application/json; charset=utf-8');


        this.http.put<number>("api/EditPlayer", data, { headers: headers })
            .subscribe(data => {
                this.currentUser.wallet = data;
            });
    }
}