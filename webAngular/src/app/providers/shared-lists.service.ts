import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class SharedListsService {

  public listCoverageType;
  constructor(public _http:HttpClient) {

   }
   getCoverageType(){
    let url ='http://localhost:62108/api/Listas/GetCoverageType';
    this.listCoverageType = this._http.get(url).pipe(map(res => {return res}));
   }
}
