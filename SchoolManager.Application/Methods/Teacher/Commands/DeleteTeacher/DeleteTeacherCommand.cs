using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Exceptions;
using SchoolManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Methods.Teacher.Commands.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest<Unit>
    {
        public int ClassId { get; set; }
    }
    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, Unit>
    {
        private readonly ISchoolDbContext _context;

        public DeleteTeacherCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes
                .Where(x => x.Id == request.ClassId)
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(cancellationToken);

            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");
            if(classEntity.Teacher == null) throw new NotFoundException($"Class with id: {request.ClassId} does not have a teacher");

            _context.Teachers.Remove(classEntity.Teacher);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
