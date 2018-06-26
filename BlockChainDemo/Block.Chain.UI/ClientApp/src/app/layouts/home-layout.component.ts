import { Component } from '@angular/core';

@Component({
  selector: 'app-home-layout',
  template: `
      <app-nav-menu></app-nav-menu>
      <router-outlet>
      </router-outlet>
  `,
  styles: []
})
export class HomeLayoutComponent { }
