using FluentValidation;

namespace TestApp.API.Model.Validators
{
    // Validators/PersonValidator.cs
    

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
        }
    }

}
