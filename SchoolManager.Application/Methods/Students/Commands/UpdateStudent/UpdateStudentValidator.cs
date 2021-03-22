using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Application.Students.Commands.UpdateStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Methods.Students.Commands.UpdateStudent
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly ISchoolDbContext _context;

        public UpdateStudentValidator(ISchoolDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name can not be an empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can not be an empty.");

            RuleFor(x => x.Pesel)
                .NotEmpty().WithMessage("Pesel can not be an empty.")
                .MustAsync(BeUniquePesel).WithMessage("The specified student already exists.")
                .Length(11).WithMessage("Incorrect pesel.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth can not be an empty.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can not be an empty.")
                .Length(9).WithMessage("Incorrect number. Phone number need to have 9 digits.");
            

        }
        private async Task<bool> BeUniquePesel(string pesel, CancellationToken cancellationToken)
                => await _context.Students.AllAsync(x => x.Pesel != pesel, cancellationToken);
    }

}
