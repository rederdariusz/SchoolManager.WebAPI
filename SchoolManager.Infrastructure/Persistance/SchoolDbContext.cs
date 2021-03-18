using Microsoft.EntityFrameworkCore;
using SchoolManager.Application.Common.Interfaces;
using SchoolManager.Domain.Entities;
using System.Reflection;

namespace SchoolManager.Infrastructure.Persistance
{
    public class SchoolDbContext : DbContext,ISchoolDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
