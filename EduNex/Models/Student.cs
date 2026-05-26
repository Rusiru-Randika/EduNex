using System;

namespace EduNex.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string RollNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ParentName { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string ParentEmail { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Class { get; set; }
        public decimal MonthlyFee { get; set; }
        public bool IsActive { get; set; }
    }
}
