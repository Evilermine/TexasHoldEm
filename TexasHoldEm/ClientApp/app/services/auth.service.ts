import { EventEmitter, Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import * as crypto from 'crypto-js';
import 'rxjs/Rx';

@Injectable()
export class AuthService {
    authKey: string;
    playerId: string;

    constructor(private http: HttpClient,
        @Inject(PLATFORM_ID) private platformId: any) {

    }

    login(username: string, password: string): Observable<boolean> {

        var url = "api/login";
        password = crypto.enc.Base64.stringify(crypto.SHA512(password));

        var data = {
            username: username,
            password: password
        };

        return this.http.post<TokenResponse>(url, data)
            .map((res) => {

                let token = res && res.token;
                // if the token is there, login has been successful
                if (token) {
                    // store username and jwt token
                    this.setAuth(res);
                    // successful login
                    return true;
                }
                // failed login
                return Observable.throw('Unauthorized');
            })
            .catch(error => {
                return new Observable<any>(error);
            })
    }

    logout(): boolean {
        this.setAuth(null);
        return true;
    }

    setAuth(auth: TokenResponse | null): boolean {
        if (isPlatformBrowser(this.platformId)) {
            if (auth) {
                localStorage.setItem(
                    this.authKey,
                    JSON.stringify(auth));
            }
            else {
                localStorage.removeItem(this.authKey);
            }
        }
        return true;
    }

    getAuth(): TokenResponse | null {
        if (isPlatformBrowser(this.platformId)) {
            var i = localStorage.getItem(this.authKey);
            if (i) {
                return JSON.parse(i);
            }
        }
        return null;
    }

    isLoggedIn(): boolean {
        if (isPlatformBrowser(this.platformId)) {
            return localStorage.getItem(this.authKey) != null;
        }
        return false;
    }

    
}

    interface TokenResponse {
        token: string,
        expiration: number,
        refresh_token: string
    }