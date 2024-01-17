import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AppComponent } from "./app.component";
import { NameFormComponent } from "./name-form/name-form.component";
import { HttpClientModule } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { PersonService } from "./data-service/person-service";
import { DataService } from "./data-service/data.service";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([{ path: "", component: NameFormComponent }]),
    BrowserAnimationsModule,
  ],
  declarations: [AppComponent],
  providers: [PersonService, DataService],
  exports: [CommonModule, FormsModule, ReactiveFormsModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
