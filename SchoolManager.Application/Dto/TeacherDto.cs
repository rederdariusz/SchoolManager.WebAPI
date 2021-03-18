using Mapster;
using SchoolManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Dto
{
    public class TeacherDto : IRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ClassName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Teacher, TeacherDto>();

        }
    }
}
