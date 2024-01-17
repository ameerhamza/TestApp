import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DataService } from "./data.service";

@Injectable({
  providedIn: "root",
})
export class PersonService {
  constructor(private dataService: DataService) {}

  createPerson(person: any): Observable<any> {
    return this.dataService.postData(person, "Persons");
  }
}
