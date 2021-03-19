using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            return await _context.Students
                .Where(x => x.ClassId == request.ClassId)
                .ProjectToType<StudentDto>(_mapper.Config)
                .ToListAsync();
        }
    }
}
