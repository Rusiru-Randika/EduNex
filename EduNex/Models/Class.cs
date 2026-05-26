using System;

namespace EduNex.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string ClassTeacherName { get; set; }
        public int ClassTeacherID { get; set; }
        public int TotalStudents { get; set; }
        public string Room { get; set; }
        public string Schedule { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
