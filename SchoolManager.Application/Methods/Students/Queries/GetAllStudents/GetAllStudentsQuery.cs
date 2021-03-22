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

namespace SchoolManager.Application.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQuery : IRequest<IEnumerable<StudentDto>>
    {
        public int ClassId { get; set; }
    }

    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
    {
        private readonly ISchoolDbContext _context;
        private readonly IMapper _mapper;

        public GetAllStudentsQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .Where(x => x.Id == request.ClassId)
                .FirstOrDefaultAsync();

            if (classEntity == null) throw new NotFoundException($"Class with id: {request.ClassId} was not found");`

            return _mapper.Map<List<StudentDto>>(classEntity.Students);
        }
    }
}
