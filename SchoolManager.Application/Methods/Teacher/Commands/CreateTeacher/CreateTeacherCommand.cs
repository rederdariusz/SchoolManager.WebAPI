using MediatR;
using SchoolManager.Application.Common.Exceptions;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Methods.Teacher.Commands.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int ClassId { get; set; }
    }

    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Unit>
    {
        private readonly ISchoolDbContext _context;
        public CreateTeacherCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
           
            var classEntity = await _context.Classes.FindAsync(request.ClassId);
            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");

            var teacherEntity = new SchoolManager.Domain.Entities.Teacher
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Pesel = request.Pesel,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                ClassId = request.ClassId
            };
            await _context.Teachers.AddAsync(teacherEntity);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
