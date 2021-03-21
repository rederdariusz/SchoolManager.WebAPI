using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Students.Commands.CreateStudent
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly ISchoolDbContext _context;

        public CreateStudentValidator(ISchoolDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name can not be an empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can not be an empty.");

            RuleFor(x => x.Pesel)
                .NotEmpty().WithMessage("Pesel can not be an empty.")
                .MustAsync(BeUniquePesel).WithMessage("The specified student already exists.")
                .MaximumLength(11).WithMessage("Incorrect pesel.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth can not be an empty.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can not be an empty.")
                .MaximumLength(9).WithMessage("Incorrect number. Phone number need to have 9 digits.");
            RuleFor(x => x.ClassId)
                .NotEmpty().WithMessage("Class id can not be an empty.");

        }
        private async Task<bool> BeUniquePesel(string pesel, CancellationToken cancellationToken)
                => await _context.Students.AllAsync(x => x.Pesel != pesel, cancellationToken);
    }
}
