
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: "root"
})
export class UtilityService {
  httpOptions = {
    headers: new HttpHeaders({
      "Access-Control-Allow-Origin": "*",
      Accept: "application/ json"
    })
  };
  constructor(private http: HttpClient) {}

  get(_url: string, _body?: any) {
    return this.http.get(environment.apiUrl + _url, this.httpOptions);
  }

  post(_url:string, _body:any) {
    return this.http.post(environment.apiUrl + _url,
      _body,
      this.httpOptions
    );
  }
}
