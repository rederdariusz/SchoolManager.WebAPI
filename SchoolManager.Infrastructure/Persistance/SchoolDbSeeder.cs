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
    public  class SchoolDbSeeder : ISchoolDbSeeder
    {
        private readonly SchoolDbContext _context;

        public SchoolDbSeeder(SchoolDbContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
                if (!_context.Classes.Any())
                {
                    _context.Classes.AddRange(new Class[]
                    {
                            new Class
                            {
                                Name = "1LO",
                                Type = ClassType.Liceum,
                                Teacher  = new Teacher{FirstName = "Miroslawa", LastName = "Kowalski", DateOfBirth = new DateTime(1965,09,20), Pesel = "65200591611", PhoneNumber = "200200200"},
                                Students = new List<Student>{
                                    new Student{FirstName = "Jan", LastName = "Kowalski", DateOfBirth = new DateTime(2000,09,20), Pesel = "00200591611", PhoneNumber = "200200200"},
                                    new Student{FirstName = "Adam", LastName = "Malysz", DateOfBirth = new DateTime(1975,11,15), Pesel = "75200591611", PhoneNumber = "333222111"}
                                }
                            },

                            new Class
                            {
                                Name = "1aT",
                                Type = ClassType.Technikum,
                                Teacher  = new Teacher{FirstName = "Anastazja", LastName = "Linka", DateOfBirth = new DateTime(1967,09,20), Pesel = "67200591611", PhoneNumber = "200200200" },
                                Students = new List<Student>{
                                    new Student{FirstName = "Mariusz", LastName = "Linka", DateOfBirth = new DateTime(2001,09,20), Pesel = "01200591611", PhoneNumber = "200200200"},
                                    new Student{FirstName = "Mateusz", LastName = "Bolek", DateOfBirth = new DateTime(1981,11,15), Pesel = "81200591611", PhoneNumber = "111222111"}
                                }
                            },
                            new Class
                            {
                                Name = "1bT",
                                Type = ClassType.Technikum,
                                Teacher = new Teacher{FirstName = "Janisława", LastName = "Lolek", DateOfBirth = new DateTime(1969,09,20), Pesel = "69200591611", PhoneNumber = "200222200"},
                                Students = new List<Student>{
                                    new Student{FirstName = "Adrian", LastName = "Lolek", DateOfBirth = new DateTime(1991,09,20), Pesel = "91200591611", PhoneNumber = "200222200"},
                                    new Student{FirstName = "Izabela", LastName = "Kolinska", DateOfBirth = new DateTime(1960,11,15), Pesel = "60200591611", PhoneNumber = "112222111"}
                                }
                            } 
                    });
                

                    await _context.SaveChangesAsync();
            }
        }
    }
}
