using MediatR;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public int ClassId { get; set; }

    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly ISchoolDbContext _context;

        public CreateStudentCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes.FindAsync(request.ClassId);
            var studentEntity = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Pesel = request.Pesel,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                ClassId = request.ClassId
            };
            await _context.Students.AddAsync(studentEntity);
            await _context.SaveChangesAsync();

            return studentEntity.Id;

        }
    }
}
