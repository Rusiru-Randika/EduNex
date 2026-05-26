using System;

namespace EduNex.Models
{
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } // Present, Absent, Late, Leave
        public string Remarks { get; set; }
        public int TeacherID { get; set; }
    }
}
