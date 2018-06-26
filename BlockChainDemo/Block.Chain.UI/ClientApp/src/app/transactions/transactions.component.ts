import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { MatDialog, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material';



  export class State {
    constructor(public name: string, public population: string, public flag: string) { }
  }
  @Component({
    selector: 'app-transactions',
    templateUrl: './app-transactions.component.html',
    styleUrls: ['./app-transactions.component.scss']
  })
  export class TransactionsComponent {

    public transactions: TransactionViewModel[];
    private Title:string;
    private http:HttpClient;
    private isBusy:boolean = true;
    stateCtrl: FormControl;
  filteredStates: Observable<any[]>;

  states: string[] = [
    
  ];

    displayedColumns = ['blockType','created','button1'];
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, 
                private route: ActivatedRoute,private dialog: MatDialog) {

        this.stateCtrl = new FormControl();
        this.http = http;
        
        this.route.queryParams.subscribe(_ => {
              this.Title = "Transactions";
              http.get<TransactionViewModel[]>('api/certificate/Transaction')
                  .subscribe(result => {
                 this.transactions = result;
                 this.isBusy = false;
                 var s =[];
                 this.transactions.forEach(_ => s.push(_.blockType));
                 this.states = Array.from(new Set(s));
                // this.filteredStates(this.states);
                this.filteredStates = this.stateCtrl.valueChanges
                .pipe(
                  startWith(''),
                  map(state => state ? this.filterStates(state) : this.states.slice()));
               }, error => console.error(error));
            });
            
    }

     formatDate(date) {
         debugger;
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0'+minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return date.getMonth()+1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
      }

    filterStates(name: string) {
        return this.states.filter(state =>
          state.toLowerCase().indexOf(name.toLowerCase()) === 0);
      }

      public viewTransactionDetails(selectedCertificate :TransactionViewModel): void {
        let dialogRef = this.dialog.open(TransactionDetailsDialogComponent,{
          data:{
            result :selectedCertificate
          }
        });
      }

      getTransactionByType(state) : void {
          this.isBusy = true;
        this.http.get<TransactionViewModel[]>('api/certificate/transaction/'+state).subscribe(result => {
        this.transactions = result;
        this.isBusy = false;
      }, error => console.error(error));
      }
  }

  export class TransactionViewModel{
        public blockType : string;
        public from: string;
        public to:string;
        public created : Date
  }

  @Component({
    selector: 'dialog-data-example-dialog',
    templateUrl:'./dialog-overview-transaction-dialog.html',
    styleUrls: ['./dialog-overview-transaction-dialog.scss']
  })
  export class TransactionDetailsDialogComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data: any) {

    }
  }