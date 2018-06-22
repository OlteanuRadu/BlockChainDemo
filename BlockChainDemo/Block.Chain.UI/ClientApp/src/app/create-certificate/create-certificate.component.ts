import { Component, Inject, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpErrorResponse } from '@angular/common/http';
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
      constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute){}

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
        fd.append(this.param, file.data);
    var objectToSend = {"CustomerIdentifier": 'test', "File":file};
        const req = new HttpRequest('POST', this.target, fd, {
          reportProgress: true
        });
    
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