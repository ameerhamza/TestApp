import { Component } from "@angular/core";
import { CommonModule } from "@angular/common";
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from "@angular/forms";

import { PersonService } from "../data-service/person-service";

@Component({
  selector: "app-name-form",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./name-form.component.html",
  styleUrls: ["./name-form.component.css"],
})
export class NameFormComponent {
  constructor(private personService: PersonService) {}

  form = new FormGroup({
    firstName: new FormControl("", [
      Validators.required,
      Validators.pattern(/^[a-zA-Z]+$/),
    ]),
    lastName: new FormControl("", [
      Validators.required,
      Validators.pattern(/^[a-zA-Z]+$/),
    ]),
  });

  submitted = false;
  errorMessage: string | null = null;
  onSubmit() {
    this.submitted = false;
    this.markFormGroupTouched(this.form);

    if (this.form.valid) {
      console.log("valid");
      this.personService
        .createPerson({
          firstName: this.form.value.firstName,
          lastName: this.form.value.lastName,
        })
        .subscribe(
          (response) => {
            this.form.reset();
            this.submitted = true;
            this.errorMessage = null;
          },
          (error) => {
            this.errorMessage = error;
          }
        );
    }
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    (Object as any).values(formGroup.controls).forEach((control: any) => {
      control.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }
}
