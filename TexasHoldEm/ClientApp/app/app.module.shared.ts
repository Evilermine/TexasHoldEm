import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormControl } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { PlayerService } from './services/player.service';
import { AuthService } from './services/auth.service';
import { GameService } from './services/game.service';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { StatsComponent } from './components/stats/stats.component';
import { SignupComponent } from './components/signup/signup.component';
import { SignInComponent } from './components/signin/signin.component';
import { GameComponent } from './components/game/game.component';
 
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        StatsComponent,
        HomeComponent,
        SignupComponent,
        SignInComponent,
        GameComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: '/home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'stats', component: StatsComponent },
            { path: 'register', component: SignupComponent },
            { path: 'signin', component: SignInComponent },
            { path: '**', redirectTo: '/home' }
        ])
    ],
    providers: [
        PlayerService,
        AuthService,
        GameService
    ]
})
export class AppModuleShared {
}
