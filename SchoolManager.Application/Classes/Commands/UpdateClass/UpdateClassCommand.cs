using MediatR;
using SchoolManager.Application.Common.Exceptions;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Classes.Commands.UpdateClass
{
    public class UpdateClassCommand : IRequest
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public ClassType Type { get; set; }
    }

    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, Unit>
    {
        private readonly ISchoolDbContext _context;

        public UpdateClassCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var classEntity = _context.Classes.Where(x => x.Id == request.ClassId).FirstOrDefault();
            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");

            classEntity.Name = request.Name;
            classEntity.Type = request.Type;

            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
