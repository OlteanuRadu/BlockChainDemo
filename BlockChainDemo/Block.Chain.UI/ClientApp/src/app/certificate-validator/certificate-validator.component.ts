import { Component, Inject } from '@angular/core';
import { CertificateModel } from '../home/home.component';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { ShipViewModel } from '../create-certificate/create-certificate.component';

@Component({
  selector: 'app-certificate-validator',
  templateUrl: './certificate-validator.component.html',
  styleUrls: ['./certificate-validator.scss']
})

export class CertificateValidatorComponent {
  private Title: string = "";
  private certificates: any;
  private ships:ShipViewModel[];

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute){

    this._http.get<CertificateModel[]>('https://localhost:44388/api/certificate/').subscribe(result => {
           this.certificates = result;
         }, error => console.error(error));

       this._http.get<ShipViewModel[]>('https://localhost:44388/api/certificate/ships').subscribe(result => {
          this.ships = result;
        }, error => console.log(error)); 
  }

  displayCertificate =(item : CertificateModel) => item.customerIdentifier;
  displayShipName = (ship: ShipViewModel) =>`${ship.shipName}`;

  onClick() {window.alert("test");}

  public validateCertificatebyId(): void {
    window.alert("test");
  }
}
