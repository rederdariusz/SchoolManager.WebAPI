using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Application.Dto;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Classes.Queries.GetAllClasses
{
    public class GetAllClassesQuery : IRequest<IEnumerable<ClassDto>>
    {

    }

    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, IEnumerable<ClassDto>>
    {
        private readonly ISchoolDbContext _context;
        private readonly IMapper _mapper;

        public GetAllClassesQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ProjectToType<ClassDto>(_mapper.Config)
                .ToListAsync();

        }
    }
}
