import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.scss']
})
export class FetchDataComponent {
  public forecasts: CertificateModel[];
  displayedColumns = ['customerIdentifier', 'vesselIdentifier', 'startDate', 'endDate','isValid','button1'];
  private Title: string;
  private isBusy:boolean = true;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) { 
    //http.get<CertificateModel[]>(baseUrl + 'api/certificate/').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));

    this.route.queryParams.subscribe(_ => {
      if (_.username != undefined) {
        this.Title = "My Certificates";
        http.get<CertificateModel[]>('https://localhost:44388/api/certificate/'+_.username).subscribe(result => {
          this.forecasts = result;
          this.isBusy= false;
        }, error => console.error(error)); 
      }
      else {
        this.Title = "All Certificates";
        http.get<CertificateModel[]>('https://localhost:44388/api/certificate/').subscribe(result => {
           this.forecasts = result;
           this.isBusy = false;
         }, error => console.error(error));
      }
    });
  }

  public getCertificateById = (selectedItem: CertificateModel) => {

    var item = {"CustomerIdentifier": selectedItem.customerIdentifier, "VesselIdentifier": selectedItem.vesselIdentifier};
    var win = window.open("https://localhost:44388/api/certificate/download/" +selectedItem.customerIdentifier);
    win.focus();
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

 interface CertificateModel {
   customerIdentifier: string;
   vesselIdentifier: string;
   startDate: Date;
   endDate: Date;
   isValid:boolean;
}
