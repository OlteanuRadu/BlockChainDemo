import { Component, Inject, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { map, tap, last, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './create-certificate.html',
    styleUrls: ['./create-certificate.scss']
  })

  export class CreateCertificateComponent {
      
    public customers: CustomerViewModel[] = [];
    public ships:ShipViewModel[] = [];
    selectedCustomer : CustomerViewModel;
    selectedShip : ShipViewModel;
    selectedStartDate: string;
    selectedEndDate: string;
  @Input() text = 'Attach Document';
      /** Name used in form which will be sent in HTTP request. */
  @Input() param = 'file';
  /** Target URL for file uploading. */
  @Input() target = 'https://localhost:44388/api/certificate/upload';
  /** File extension that accepted, same as 'accept' of <input type="file" />. By the default, it's set to 'image/*'. */
  @Input() accept = 'image/*';
  /** Allow you to add handler after its completion. Bubble up response text from remote. */
  @Output() complete = new EventEmitter<string>();


  private files: Array<FileUploadModel> = [];

      constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute){
        _http.get<CustomerViewModel[]>('https://localhost:44388/api/certificate/customers').subscribe(result => {

          this.customers = result;
        }, error => console.log(error)); 

        _http.get<ShipViewModel[]>('https://localhost:44388/api/certificate/ships').subscribe(result => {
          this.ships = result;
        }, error => console.log(error)); 

      }

      displayShipName = (ship:ShipViewModel) =>`${ship.shipName}`;

      displayCustomer = (state:CustomerViewModel) => 
          `${state.firstName} ${state.lastName}`;

      onClick() {
        const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
        fileUpload.onchange = () => {
          for (let index = 0; index < fileUpload.files.length; index++) {
            const file = fileUpload.files[index];
            this.files.push({ data: file, state: 'in', inProgress: false, progress: 0, canRetry: false, canCancel: true });
          }
         this.uploadFiles();
        };
        fileUpload.click();
      }

      private uploadFiles() {
        const fileUpload = document.getElementById('fileUpload') as HTMLInputElement;
        fileUpload.value = '';
    
        this.files.forEach(file => {
          if (!file.inProgress) {
            this.uploadFile(file);
          }
        });
      }

      private uploadFile(file: FileUploadModel) {
        const fd = new FormData();
        fd.append("CustomerIdentifier",this.selectedCustomer.id);
        fd.append("VesselIdentifier",this.selectedShip.id);
        fd.append("StartDate",this.selectedStartDate);
        fd.append("EndDate",this.selectedEndDate);
        fd.append(this.param, file.data);

        const req = new HttpRequest('POST', this.target, fd, {
          reportProgress: true,
        });
        
        //this._http.post(this.target,fd);
        
        file.inProgress = true;
         file.sub = this._http.request(req).pipe(
          map(event => {
            switch (event.type) {
              case HttpEventType.UploadProgress:
                file.progress = Math.round(event.loaded * 100 / event.total);
                break;
              case HttpEventType.Response:
                return event;
            }
          }),
          tap(message => { }),
          last(),
          catchError((error: HttpErrorResponse) => {
            file.inProgress = false;
            file.canRetry = true;
            return of(`${file.data.name} upload failed.`);
          })
        ).subscribe(
          (event: any) => {
            if (typeof (event) === 'object') {
              this.removeFileFromArray(file);
              this.complete.emit(event.body);
            }
          }
        );
      }

      private removeFileFromArray(file: FileUploadModel) {
        const index = this.files.indexOf(file);
        if (index > -1) {
          this.files.splice(index, 1);
        }
      }

  }
  export class FileUploadModel {
    //customerIdentifier:string
    data: File;
    state: string;
    inProgress: boolean;
    progress: number;
    canRetry: boolean;
    canCancel: boolean;
    sub?: Subscription;
  }

  export interface CustomerViewModel{
    firstName:string;
    id:string;
    lastName:string;
  }

  export interface ShipViewModel{
    shipName:string;
    id:string;
  }