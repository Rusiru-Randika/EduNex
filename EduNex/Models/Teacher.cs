using System;

namespace EduNex.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
    }
}
