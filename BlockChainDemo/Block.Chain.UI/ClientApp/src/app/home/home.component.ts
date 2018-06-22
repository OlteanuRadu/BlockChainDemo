import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  private certificates: CertificateModel[];
  private fileAsString: string;
  private totalCertificates:number = 10;
  private currentUser:string = "Radu Olteanu";


  constructor(private httpClient: HttpClient, private router: Router) { }

  downloadFile(data: Response) {
    var blob = new Blob([data], { type: 'text/pdf' });
    var url = window.URL.createObjectURL(blob);
    window.open(url);
  }

  public getAllCertificates = () => this.httpClient
    .get<CertificateModel[]>("api/certificate")
    .subscribe(_ => this.certificates = _);

  public getCertificateById = (id: string) => {
    var win = window.open("api/certificate/1", "_blank");
    win.focus();
  }
  public navigateToMyCertificates(): void {
    var username = JSON.parse(localStorage.getItem('currentUser')).username;
    //var user = JSON.parse(currentUser.id).username;
    this.router.navigate(['/fetch-data'], { queryParams: { username: username } });
  }

  public navigateToAllCertificates(): void {
    this.router.navigate(['/fetch-data']);
  }
  public navigateToAllTransactions():void{
    this.router.navigate(['/transactions']);
  }

  public validateCertificate(): void {
    this.router.navigate(['/certificate-validator']);
  }

  public createCertificate(): void{
    this.router.navigate(['/create-certificate']);
  }
  public logout():void{
    this.router.navigate(['/login']);
  }
}


export class CertificateModel {
  public customerIdentifier: string;
  public blockType: string;

}
