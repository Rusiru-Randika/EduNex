using System;

namespace EduNex.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string ParentEmail { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; } // Attendance, Fee, Exam, General
        public DateTime SentDate { get; set; }
        public bool IsSent { get; set; }
        public int TeacherID { get; set; }
    }
}
