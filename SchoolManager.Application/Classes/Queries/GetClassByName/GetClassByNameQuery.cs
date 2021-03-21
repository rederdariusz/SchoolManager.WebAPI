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

namespace SchoolManager.Application.Classes.Queries.GetClassByName
{
    public class GetClassByNameQuery : IRequest<ClassDto>
    {
        public string ClassName { get; set; }
    }

    public class GetClassByNameQueryHandler : IRequestHandler<GetClassByNameQuery, ClassDto>
    {
        private readonly ISchoolDbContext _context;
        private readonly IMapper _mapper;

        public GetClassByNameQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClassDto> Handle(GetClassByNameQuery request, CancellationToken cancellationToken)
        {
            var classDto = await _context.Classes
                .Where(x => x.Name == request.ClassName)
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ProjectToType<ClassDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            if (classDto == null) throw new NotFoundException($"Class with name: {request.ClassName} was not found");

            return classDto;
        }
    }
}
