import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AccountModule } from './account/account.module';
import { LoginFormComponent } from './account/login-form/login-form.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeLayoutComponent } from './layouts/home-layout.component';
import { LoginLayoutComponent } from './layouts/login-layout.component';
import { AuthService } from './auth/auth.service';
import { AuthGuard } from './auth/auth.guard';
import { AppMaterialModule } from './app-material/app-material.module';
import { CertificateValidatorComponent, FileNameDialogComponent } from './certificate-validator/certificate-validator.component';
import { fakeBackendProvider } from './auth/fake-backend';
import { JwtInterceptor } from './auth/jwt.interceptor';
import { TransactionsComponent, TransactionDetailsDialogComponent } from './transactions/transactions.component';
import { CreateCertificateComponent } from './create-certificate/create-certificate.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    HomeLayoutComponent,
    CounterComponent,
    FetchDataComponent,
    CertificateValidatorComponent,
    CreateCertificateComponent,
    TransactionsComponent,
    FileNameDialogComponent,
    TransactionDetailsDialogComponent
  ],
  imports: [
    AccountModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    AppRoutingModule,
    AppMaterialModule,
    BrowserAnimationsModule
  ],
  entryComponents :[FileNameDialogComponent,TransactionDetailsDialogComponent],
  providers: [
    AuthService,
    AuthGuard,
    fakeBackendProvider,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
  }
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
