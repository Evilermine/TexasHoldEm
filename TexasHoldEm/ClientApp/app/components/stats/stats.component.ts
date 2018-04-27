import { Component } from '@angular/core';

@Component({
    selector: 'stats',
    templateUrl: './stats.component.html'
})
export class StatsComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }
}
