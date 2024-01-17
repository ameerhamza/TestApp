import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class DataService {
  private apiUrl = "http://localhost:5012/api/";

  constructor(private http: HttpClient) {}

  postData(data: any, serviceName: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
      }),
    };

    return this.http.post(this.apiUrl + serviceName, data, httpOptions).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = "Something went wrong!";

        if (error.status === 400) {
          errorMessage = "Bad Request. Please check your data.";
        } else if (error.status === 409) {
          errorMessage = "User already exists.";
        } else if (error.status === 500) {
          errorMessage = "Internal Server Error. Please try again later.";
        }

        return throwError(errorMessage);
      })
    );
  }
}
