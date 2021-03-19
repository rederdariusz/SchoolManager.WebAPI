using Mapster;
using SchoolManager.Domain.Entities;
using SchoolManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Application.Dto
{
    public class ClassDto : IRegister
    {
       
        public string Name { get; set; }
        public string Type { get; set; }
        public TeacherDto Teacher { get; set; }
        public List<StudentDto> Students { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Class, ClassDto>()
                .Map(dest => dest.Type, src => src.Type.ToString());
        }
    }
}
