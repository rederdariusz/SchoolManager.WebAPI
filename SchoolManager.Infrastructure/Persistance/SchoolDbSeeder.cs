using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Persistance
{
    public class SchoolDbSeeder : ISchoolDbSeeder
    {
        private readonly SchoolDbContext _context;

        public SchoolDbSeeder(SchoolDbContext context)
        {
            _context = context;
        }

        public async void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if (!_context.Classes.Any())
                {
                    _context.Classes.AddRange(new Class[]
                    {
                        new Class{Name = "1LO",Type = ClassType.Liceum},
                        new Class{Name = "1aT",Type = ClassType.Technikum},
                        new Class{Name = "1bT",Type = ClassType.Technikum}
                    });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
