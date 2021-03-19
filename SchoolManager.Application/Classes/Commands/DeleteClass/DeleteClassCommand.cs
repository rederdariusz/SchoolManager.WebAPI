using MediatR;
using SchoolManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManager.Application.Classes.Commands.DeleteClass
{
    public class DeleteClassCommand : IRequest
    {
        public int ClassId { get; set; }
    }
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand>
    {
        private readonly ISchoolDbContext _context;

        public DeleteClassCommandHandler(ISchoolDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var classEntity = await _context.Classes.FindAsync(request.ClassId);
            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
