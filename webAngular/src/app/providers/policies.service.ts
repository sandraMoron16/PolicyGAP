import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { Headers, Http } from "@angular/http";
import { map, catchError, } from "rxjs/operators";
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PoliciesService {

  public listPolicies;

  constructor(public _http: HttpClient) {

  }

  getClients() {
    let url = 'http://localhost:62108/api/Listas/GetClient';
    return this._http.get(url).pipe(map(res => { return res }));
  }

  getPolicies() {
    let url = 'http://localhost:62108/api/Policies/GetAll';
    this.listPolicies = this._http.get(url).pipe(map(res => { return res }));
  }

  PostPolicy(model: any) {
    let url = 'http://localhost:62108/api/Policies/Post';
    let body = JSON.stringify(model);
    let headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this._http.post(url, body, { headers: headers }).pipe(
      catchError(this.handleError)
    )
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
