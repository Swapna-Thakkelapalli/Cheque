import { FileDownloadService } from './../services/filedownload.service';
import { Cheque } from './../models/cheque.model';
import { ChequeService } from './../services/cheque.service';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-cheque',
  templateUrl: './cheque.component.html',
  styleUrls: ['./cheque.component.scss']
})
export class ChequeComponent implements OnInit {
  cheque:Cheque={};
  currencyList:string[]=[];
  showSpinner=false;
  minDate = new Date();
  constructor(private _chequeService:ChequeService,private _filedownload:FileDownloadService,private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this._chequeService.GetCurrencyList().subscribe(res =>{
      this.currencyList = res;
    });
  }
  GenerateCheque(form:any){
    form.submitted = true;
    if(!form.invalid){
      this.showSpinner = true;
      this.cheque.Date = moment(this.cheque.InputDate).format(
        "DD/MM/YYYY"
      );
    this._chequeService.GenerateCheque(this.cheque).subscribe(res=>{
      this._filedownload.DownloadPdfFileUsingBinary(res,'cheque');
      this.showSpinner = false;
      this._snackBar.open("Cheque Genrated.",'X',{
        horizontalPosition: 'right',
        verticalPosition: 'top',
        duration:2000
      })
    },
    error =>{
      this.showSpinner = false;
      this._snackBar.open("SOmething went wrong",'X',{
        horizontalPosition: 'right',
        verticalPosition: 'top',
      });
    });
  }
  }
}
