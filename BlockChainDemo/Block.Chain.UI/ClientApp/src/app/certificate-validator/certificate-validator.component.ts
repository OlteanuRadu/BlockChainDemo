import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-certificate-validator',
  templateUrl: './certificate-validator.component.html'
})

export class CertificateValidatorComponent {
  private Title: string = "";

  public validateCertificatebyId(): void {
    window.alert("test");
  }
}
