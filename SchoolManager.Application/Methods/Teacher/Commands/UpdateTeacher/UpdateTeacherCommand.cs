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

namespace SchoolManager.Application.Methods.Teacher.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<Unit>
    {
        public int ClassId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Unit>
    {
        private readonly ISchoolDbContext _context;
        public UpdateTeacherCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes
               .Where(x => x.Id == request.ClassId)
               .Include(x => x.Teacher)
               .FirstOrDefaultAsync(cancellationToken);

            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");

            if (classEntity.Teacher == null) throw new NotFoundException($"Class with id: {request.ClassId} does not have a teacher");

            classEntity.Teacher.FirstName = request.FirstName;
            classEntity.Teacher.LastName = request.LastName;
            classEntity.Teacher.Pesel = request.Pesel;
            classEntity.Teacher.DateOfBirth = request.DateOfBirth;
            classEntity.Teacher.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
