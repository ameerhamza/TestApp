import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

import { DataService } from '../data-service/data.service';

@Component({
  selector: 'app-name-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './name-form.component.html',
  styleUrl: './name-form.component.css'
})
export class NameFormComponent {
  constructor(private dataService: DataService) {}
  form = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
  });
  submitted = false;


  onSubmit() {
    this.submitted = false;
    this.dataService.postData({ firstName: this.form.value.firstName, lastName: this.form.value.lastName}).subscribe((response) => {
      if (response) {
        this.form.reset();
        this.submitted = true;
      }
    })
  }
}
