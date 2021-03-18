using Microsoft.EntityFrameworkCore;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Common.Interfaces
{
    public interface ISchoolDbContext
    {
        DbSet<Class> Classes { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
    }
}
