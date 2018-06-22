import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule }  from '@angular/forms';
import { SharedModule }   from '../shared/modules/shared.module';
 

import { EmailValidator } from '../directives/email.validator.directive';

import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { FacebookLoginComponent } from './facebook-login/facebook-login.component';
import { AppMaterialModule } from '../app-material/app-material.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,ReactiveFormsModule, AppMaterialModule
  ],
  declarations: [RegistrationFormComponent,
    EmailValidator, 
    LoginFormComponent, 
    FacebookLoginComponent]
})
export class AccountModule { }
