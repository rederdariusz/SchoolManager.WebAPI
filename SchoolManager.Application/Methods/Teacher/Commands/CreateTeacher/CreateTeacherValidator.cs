using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Methods.Teacher.Commands.CreateTeacher
{
    public class CreateTeacherValidator : AbstractValidator<CreateTeacherCommand>
    {
        private readonly ISchoolDbContext _context;

        public CreateTeacherValidator(ISchoolDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name can not be an empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can not be an empty.");

            RuleFor(x => x.Pesel)
                .NotEmpty().WithMessage("Pesel can not be an empty.")
                .Length(11).WithMessage("Incorrect pesel. Pesel need to have 11 digits.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth can not be an empty.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number can not be an empty.")
                .Length(9).WithMessage("Incorrect number. Phone number need to have 9 digits.");

            RuleFor(x => x.ClassId)
                .MustAsync(BeOneTeacher).WithMessage("Class already have a teacher.");

        }
        private async Task<bool> BeOneTeacher(int classId, CancellationToken cancellationToken)
                => await _context.Teachers.AllAsync(x => x.ClassId != classId);
    }
}
