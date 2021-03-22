using MediatR;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Classes.Commands.CreateClass
{
    public class CreateClassCommand : IRequest<int>
    {
        public string Name { get; set; }
        public ClassType Type { get; set; }
    }

    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, int>
    {
        private readonly ISchoolDbContext _context;

        public CreateClassCommandHandler(ISchoolDbContext context)
        {
            _context = context;       
        }
        public async Task<int> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        { 
            var classEntity = new Class { Name = request.Name, Type = request.Type };
            await _context.Classes.AddAsync(classEntity);
            await _context.SaveChangesAsync();
            return classEntity.Id;
        }
    }
}
