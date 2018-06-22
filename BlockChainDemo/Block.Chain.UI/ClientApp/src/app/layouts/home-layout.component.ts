import { Component } from '@angular/core';

@Component({
  selector: 'app-home-layout',
  template: `
    <div class='container-fluid'>
  <div class='row'>
    <div class='col-sm-3'>
      <app-nav-menu></app-nav-menu>
    </div>
    <div class='col-sm-9 body-content'>
      <router-outlet></router-outlet>
    </div>
  </div>
</div>
  `,
  styles: []
})
export class HomeLayoutComponent { }
