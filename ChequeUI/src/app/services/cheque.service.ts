import { Cheque } from './../models/cheque.model';
import { UtilityService } from './utility.service';
import { Injectable } from "@angular/core";
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: "root"
})
export class ChequeService {
  constructor(
    private _utilitySerivce: UtilityService
  ) { }

  public GenerateCheque(cheque:Cheque) {
    return this._utilitySerivce.post('Cheque/GenerateCheque',cheque);
  }
  public GetCurrencyList():Observable<string[]>{
    var currencies = ["GBP","EUR","USD","INR","AUD"];
    return of(currencies);
  }
}
