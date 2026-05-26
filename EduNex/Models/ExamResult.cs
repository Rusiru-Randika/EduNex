using System;

namespace EduNex.Models
{
    public class ExamResult
    {
        public int ResultID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string ExamName { get; set; }
        public string Subject { get; set; }
        public decimal MarksObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal Percentage { get; set; }
        public string Grade { get; set; }
        public DateTime ExamDate { get; set; }
        public int TeacherID { get; set; }
    }
}
