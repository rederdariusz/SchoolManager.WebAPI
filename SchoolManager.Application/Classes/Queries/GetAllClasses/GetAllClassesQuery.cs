using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Classes.Queries.GetAllClasses
{
    public class GetAllClassesQuery : IRequest<IEnumerable<Class>>
    {

    }

    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, IEnumerable<Class>>
    {
        private readonly ISchoolDbContext _context;

        public GetAllClassesQueryHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Class>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ToListAsync();

        }
    }
}
