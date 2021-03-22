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

namespace SchoolManager.Application.Methods.Teacher.Queries.GetTeacher
{
    public class GetTeacherQuery : IRequest<TeacherDto>
    {
        public int ClassId { get; set; }
    }

    public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherDto>
    {
        private readonly ISchoolDbContext _context;
        private readonly IMapper _mapper;

        public GetTeacherQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TeacherDto> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Teacher)
                .Where(x => x.Id == request.ClassId)
                .FirstOrDefaultAsync();

            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");
            if (classEntity.Teacher == null) throw new NotFoundException($"Class with id: {request.ClassId} does not have a teacher");

            return _mapper.Map<TeacherDto>(classEntity.Teacher);
        }
    }
}
