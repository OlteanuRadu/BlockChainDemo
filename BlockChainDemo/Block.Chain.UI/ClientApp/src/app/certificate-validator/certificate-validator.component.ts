import { Component, Inject } from '@angular/core';
import { CertificateModel } from '../home/home.component';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { ShipViewModel } from '../create-certificate/create-certificate.component';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-certificate-validator',
  templateUrl: './certificate-validator.component.html',
  styleUrls: ['./certificate-validator.scss']
})

export class CertificateValidatorComponent {
  private Title: string = "";
  private certificates: any;
  private ships:ShipViewModel[];
  private selectedCertificate : any;
  private messageValidate:string;
  private selectedShip : ShipViewModel;

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string,
              private route: ActivatedRoute, private dialog: MatDialog) {

    this._http.get<CertificateModel[]>('api/certificate/').subscribe(result => {
           this.certificates = result;
         }, error => console.error(error));

       this._http.get<ShipViewModel[]>('api/certificate/ships').subscribe(result => {
          this.ships = result;
        }, error => console.log(error)); 
  }

  displayCertificate =(item : any) => item.id;
  displayShipName = (ship: ShipViewModel) =>`${ship.shipName}`;


  public validateCertificatebyId(): void {

    let body = new FormData();
    body.append('certificateId', this.selectedCertificate.id);
    this._http.post<boolean>('api/certificate/ValidateById',body).subscribe(_ => {
     this.messageValidate=  _ == true ? "valid": "not valid";

      let dialogRef = this.dialog.open(FileNameDialogComponent,{
        data:{
          result :`Certificate is ${this.messageValidate}`
        }
      });
    });
  }
  
  public validateCertificatebyVesselId(): void{

    let body = new FormData();
    body.append('vesselId', this.selectedShip.id);
    this._http.post<boolean>('api/certificate/ValidateByVesselId',body).subscribe(_ => {
      this.messageValidate=  _ == true ? "valid": "not valid";
 
       let dialogRef = this.dialog.open(FileNameDialogComponent,{
         data:{
           result :`Certificate is ${this.messageValidate} for ${this.selectedShip.shipName}`
         }
       });
     });
  }
}

@Component({
  selector: 'dialog-data-example-dialog',
  templateUrl:'./dialog-overview-example-dialog.html'
})
export class FileNameDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
}

