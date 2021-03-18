using SchoolManager.Domain.Common;
using SchoolManager.Domain.Enums;
using System.Collections.Generic;

namespace SchoolManager.Domain.Entities
{
    public class Class : Entity
    {
        public string Name { get; set; }
        public ClassType Type { get; set; }

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual List<Student> Students { get; set; }
    }
}
