using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Persistance.Config
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.FirstName)
                .IsRequired();
            builder.Property(s => s.LastName)
                .IsRequired();
            builder.Property(s => s.Pesel)
                .IsRequired()
                .HasMaxLength(11);
            builder.Property(s => s.DateOfBirth)
                .IsRequired();
            builder.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(9);
        }
    }
}
