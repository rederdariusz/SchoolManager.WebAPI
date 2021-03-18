using SchoolManager.Domain.Common;
using System;

namespace SchoolManager.Domain.Entities
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}