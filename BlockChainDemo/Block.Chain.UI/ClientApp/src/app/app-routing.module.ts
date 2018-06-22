import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { LoginFormComponent } from './account/login-form/login-form.component';
import { LoginLayoutComponent } from './layouts/login-layout.component';
import { HomeComponent } from './home/home.component';
import { HomeLayoutComponent } from './layouts/home-layout.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CertificateValidatorComponent } from './certificate-validator/certificate-validator.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TransactionsComponent } from './transactions/transactions.component';
import { CreateCertificateComponent } from './create-certificate/create-certificate.component';

const routes: Routes = [
  {
    path: '',
    component: HomeLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', component: HomeComponent
      },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'certificate-validator', component: CertificateValidatorComponent },
      { path: 'transactions', component:TransactionsComponent},
      { path: 'create-certificate', component: CreateCertificateComponent },
    ]
  },
  { path: 'login', component: LoginFormComponent},
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),FormsModule,ReactiveFormsModule ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
