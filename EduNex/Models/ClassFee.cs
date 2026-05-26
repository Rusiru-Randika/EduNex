using System;

namespace EduNex.Models
{
    public class ClassFee
    {
        public int FeeID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string Status { get; set; } // Pending, Paid, Overdue
        public string Description { get; set; }
        public int TeacherID { get; set; }
    }
}
