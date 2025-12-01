using FluentValidation;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Validators
{
    public class PersonalDetailsValidator: AbstractValidator<PersonalDetail>
    {
       public PersonalDetailsValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(p => p.IDNumber)
                .NotEmpty().WithMessage("ID Number is required.")
                .Length(13).WithMessage("ID Number must be exactly 13 characters.");
            RuleFor(c => c.DateOfBirth)
                   .LessThan(DateTime.Now.AddYears(-18))
                   .WithMessage("Customer must be at least 18 years old");


        }
    }
}
