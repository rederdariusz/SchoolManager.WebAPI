using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Exceptions;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<StudentDto>
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
    }

    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto>
    {
        private readonly ISchoolDbContext _context;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
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

            return _mapper.Map<StudentDto>(studentEntity);


        }
    }
}
