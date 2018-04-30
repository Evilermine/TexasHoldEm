import { Component, DoCheck } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
})
export class NavMenuComponent implements DoCheck {
    private logged: boolean;

    constructor(private authService: AuthService) {
            }


    ngDoCheck() {
        if (this.authService.IsLoggedIn)
            this.logged = true;
        else
            this.logged = false;

        console.log(this.logged);
    }
    
    disconnect() {
        console.log("logout()");
        this.authService.logout();
    }
}
