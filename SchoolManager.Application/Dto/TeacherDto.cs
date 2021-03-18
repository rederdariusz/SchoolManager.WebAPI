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
        public string FullName { get; set; }
        public string Pesel { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string ClassName { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Teacher, TeacherDto>()
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}")
                .Map(dest => dest.Age, src => (int)(DateTime.Now - src.DateOfBirth).TotalDays/365); ;

        }
    }
}
