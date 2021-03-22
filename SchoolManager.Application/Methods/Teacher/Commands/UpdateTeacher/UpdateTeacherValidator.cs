using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Methods.Teacher.Commands.UpdateTeacher
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name can not be an empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can not be an empty.");

            RuleFor(x => x.Pesel)
                .NotEmpty().WithMessage("Pesel can not be an empty.")
                .Length(11).WithMessage("Incorrect pesel.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth can not be an empty.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can not be an empty.")
                .Length(9).WithMessage("Incorrect number. Phone number need to have 9 digits.");
        }
    }
}
