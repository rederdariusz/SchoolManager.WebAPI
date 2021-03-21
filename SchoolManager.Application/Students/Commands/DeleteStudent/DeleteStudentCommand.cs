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

namespace SchoolManager.Application.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly ISchoolDbContext _context;

        public DeleteStudentCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes
                .Where(x => x.Id == request.ClassId)
                .Include(x => x.Students)
                .FirstOrDefaultAsync(cancellationToken);

            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");

            var studentEntity = classEntity.Students
                .Where(x => x.Id == request.StudentId)
                .FirstOrDefault();

            if (studentEntity == null) throw new NotFoundException($"Student with id: {request.StudentId} was not found");

            _context.Students.Remove(studentEntity);
            await _context.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
