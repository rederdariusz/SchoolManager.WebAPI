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

namespace SchoolManager.Application.Classes.Queries.GetClassById
{
    public class GetClassByIdQuery : IRequest<ClassDto>
    {
        public int ClassId { get; set; }
    }

    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ClassDto>
    {
        private readonly ISchoolDbContext _context;
        private readonly IMapper _mapper;

        public GetClassByIdQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClassDto> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Classes
                .Where(x => x.Id == request.ClassId)
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ProjectToType<ClassDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
