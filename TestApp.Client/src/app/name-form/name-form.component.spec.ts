import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from "@angular/core/testing";
import { ReactiveFormsModule } from "@angular/forms";
import { NameFormComponent } from "./name-form.component";
import { PersonService } from "../data-service/person-service";
import { of, throwError } from "rxjs";

describe("NameFormComponent", () => {
  let component: NameFormComponent;
  let fixture: ComponentFixture<NameFormComponent>;
  let personServiceSpy: jasmine.SpyObj<PersonService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj("PersonService", ["createPerson"]);

    TestBed.configureTestingModule({
      declarations: [NameFormComponent],
      imports: [ReactiveFormsModule],
      providers: [{ provide: PersonService, useValue: spy }],
    });

    fixture = TestBed.createComponent(NameFormComponent);
    component = fixture.componentInstance;
    personServiceSpy = TestBed.inject(
      PersonService
    ) as jasmine.SpyObj<PersonService>;
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should mark form controls as touched on submit", () => {
    spyOn(component.form, "markAsTouched");
    component.onSubmit();
    expect(component.form.markAsTouched).toHaveBeenCalled();
  });

  it("should not call createPerson if form is invalid", () => {
    spyOn(personServiceSpy, "createPerson");
    component.onSubmit();
    expect(personServiceSpy.createPerson).not.toHaveBeenCalled();
  });

  it("should call createPerson if form is valid", fakeAsync(() => {
    component.form.setValue({
      firstName: "John",
      lastName: "Doe",
    });

    personServiceSpy.createPerson.and.returnValue(of({}));
    component.onSubmit();
    tick(); // wait for async operation to complete

    expect(personServiceSpy.createPerson).toHaveBeenCalledWith({
      firstName: "John",
      lastName: "Doe",
    });
  }));

  it("should reset form and set submitted to true on successful submission", fakeAsync(() => {
    component.form.setValue({
      firstName: "John",
      lastName: "Doe",
    });

    personServiceSpy.createPerson.and.returnValue(of({}));
    component.onSubmit();
    tick();

    expect(component.form.value).toEqual({ firstName: "", lastName: "" });
    expect(component.submitted).toBeTruthy();
  }));

  it("should set errorMessage on error", fakeAsync(() => {
    component.form.setValue({
      firstName: "John",
      lastName: "Doe",
    });

    const errorMessage = "Some error message";
    personServiceSpy.createPerson.and.returnValue(throwError(errorMessage));
    component.onSubmit();
    tick();

    expect(component.errorMessage).toEqual(errorMessage);
  }));
});
