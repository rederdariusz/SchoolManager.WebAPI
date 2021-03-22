using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Exceptions;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly ISchoolDbContext _context;

        public UpdateStudentCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
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

            studentEntity.FirstName = request.FirstName;
            studentEntity.LastName = request.LastName;
            studentEntity.Pesel = request.Pesel;
            studentEntity.DateOfBirth = request.DateOfBirth;
            studentEntity.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();
            return Unit.Value;
            

        }
    }
}
