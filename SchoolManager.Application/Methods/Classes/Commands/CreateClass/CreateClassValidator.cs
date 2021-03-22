using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Classes.Commands.CreateClass
{
    public class CreateClassValidator : AbstractValidator<CreateClassCommand>
    {
        private readonly ISchoolDbContext _context;

        public CreateClassValidator(ISchoolDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be an empty.")
                .MustAsync(BeUniqueName).WithMessage("The specified class already exists.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type can not be an empty.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
            => await _context.Classes.AllAsync(x => x.Name.ToLower() != name.ToLower(),cancellationToken);
                
    }
}
