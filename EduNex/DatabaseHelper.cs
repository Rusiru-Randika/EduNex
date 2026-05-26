using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EduNex
{
    public class DatabaseHelper
    {
        private static string _connectionString = "Server=localhost;Database=EduNex;User Id=root;Password=;";

        // Sample in-memory data storage (will be replaced with actual database later)
        private static List<Models.Student> _students = new List<Models.Student>();
        private static List<Models.Teacher> _teachers = new List<Models.Teacher>();
        private static List<Models.Attendance> _attendances = new List<Models.Attendance>();
        private static List<Models.ClassFee> _fees = new List<Models.ClassFee>();
        private static List<Models.ExamResult> _results = new List<Models.ExamResult>();
        private static List<Models.Notification> _notifications = new List<Models.Notification>();
        private static List<Models.Class> _classes = new List<Models.Class>();

        public static void SetConnectionString(string connString)
        {
            _connectionString = connString;
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        #region Student Methods
        public static void AddStudent(Models.Student student)
        {
            student.StudentID = _students.Count + 1;
            _students.Add(student);
        }

        public static List<Models.Student> GetAllStudents()
        {
            return _students.ToList();
        }

        public static Models.Student GetStudentById(int studentId)
        {
            return _students.FirstOrDefault(s => s.StudentID == studentId);
        }

        public static void UpdateStudent(Models.Student student)
        {
            var existing = _students.FirstOrDefault(s => s.StudentID == student.StudentID);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.RollNumber = student.RollNumber;
                existing.DateOfBirth = student.DateOfBirth;
                existing.Gender = student.Gender;
                existing.Address = student.Address;
                existing.PhoneNumber = student.PhoneNumber;
                existing.ParentName = student.ParentName;
                existing.ParentPhoneNumber = student.ParentPhoneNumber;
                existing.ParentEmail = student.ParentEmail;
                existing.Class = student.Class;
                existing.MonthlyFee = student.MonthlyFee;
                existing.IsActive = student.IsActive;
            }
        }

        public static void DeleteStudent(int studentId)
        {
            _students.RemoveAll(s => s.StudentID == studentId);
        }
        #endregion

        #region Teacher Methods
        public static void AddTeacher(Models.Teacher teacher)
        {
            teacher.TeacherID = _teachers.Count + 1;
            _teachers.Add(teacher);
        }

        public static List<Models.Teacher> GetAllTeachers()
        {
            return _teachers.ToList();
        }

        public static Models.Teacher GetTeacherById(int teacherId)
        {
            return _teachers.FirstOrDefault(t => t.TeacherID == teacherId);
        }

        public static Models.Teacher GetTeacherByEmail(string email)
        {
            return _teachers.FirstOrDefault(t => t.Email == email);
        }

        public static void UpdateTeacher(Models.Teacher teacher)
        {
            var existing = _teachers.FirstOrDefault(t => t.TeacherID == teacher.TeacherID);
            if (existing != null)
            {
                existing.Name = teacher.Name;
                existing.Email = teacher.Email;
                existing.PhoneNumber = teacher.PhoneNumber;
                existing.Subject = teacher.Subject;
                existing.Class = teacher.Class;
                existing.IsActive = teacher.IsActive;
            }
        }

        public static void DeleteTeacher(int teacherId)
        {
            _teachers.RemoveAll(t => t.TeacherID == teacherId);
        }
        #endregion

        #region Attendance Methods
        public static void AddAttendance(Models.Attendance attendance)
        {
            attendance.AttendanceID = _attendances.Count + 1;
            _attendances.Add(attendance);
        }

        public static List<Models.Attendance> GetAttendanceByDate(DateTime date)
        {
            return _attendances.Where(a => a.AttendanceDate.Date == date.Date).ToList();
        }

        public static List<Models.Attendance> GetAttendanceByStudent(int studentId)
        {
            return _attendances.Where(a => a.StudentID == studentId).ToList();
        }

        public static List<Models.Attendance> GetAllAttendances()
        {
            return _attendances.ToList();
        }

        public static void UpdateAttendance(Models.Attendance attendance)
        {
            var existing = _attendances.FirstOrDefault(a => a.AttendanceID == attendance.AttendanceID);
            if (existing != null)
            {
                existing.Status = attendance.Status;
                existing.Remarks = attendance.Remarks;
            }
        }

        public static void DeleteAttendance(int attendanceId)
        {
            _attendances.RemoveAll(a => a.AttendanceID == attendanceId);
        }
        #endregion

        #region Fee Methods
        public static void AddFee(Models.ClassFee fee)
        {
            fee.FeeID = _fees.Count + 1;
            _fees.Add(fee);
        }

        public static List<Models.ClassFee> GetAllFees()
        {
            return _fees.ToList();
        }

        public static List<Models.ClassFee> GetFeesByStudent(int studentId)
        {
            return _fees.Where(f => f.StudentID == studentId).ToList();
        }

        public static List<Models.ClassFee> GetFeesByStatus(string status)
        {
            return _fees.Where(f => f.Status == status).ToList();
        }

        public static void UpdateFee(Models.ClassFee fee)
        {
            var existing = _fees.FirstOrDefault(f => f.FeeID == fee.FeeID);
            if (existing != null)
            {
                existing.Status = fee.Status;
                existing.PaidDate = fee.PaidDate;
            }
        }

        public static void DeleteFee(int feeId)
        {
            _fees.RemoveAll(f => f.FeeID == feeId);
        }
        #endregion

        #region Exam Result Methods
        public static void AddExamResult(Models.ExamResult result)
        {
            result.ResultID = _results.Count + 1;
            _results.Add(result);
        }

        public static List<Models.ExamResult> GetAllExamResults()
        {
            return _results.ToList();
        }

        public static List<Models.ExamResult> GetResultsByStudent(int studentId)
        {
            return _results.Where(r => r.StudentID == studentId).ToList();
        }

        public static List<Models.ExamResult> GetResultsByExam(string examName)
        {
            return _results.Where(r => r.ExamName == examName).ToList();
        }

        public static void UpdateExamResult(Models.ExamResult result)
        {
            var existing = _results.FirstOrDefault(r => r.ResultID == result.ResultID);
            if (existing != null)
            {
                existing.MarksObtained = result.MarksObtained;
                existing.TotalMarks = result.TotalMarks;
                existing.Percentage = result.Percentage;
                existing.Grade = result.Grade;
            }
        }

        public static void DeleteExamResult(int resultId)
        {
            _results.RemoveAll(r => r.ResultID == resultId);
        }
        #endregion

        #region Notification Methods
        public static void AddNotification(Models.Notification notification)
        {
            notification.NotificationID = _notifications.Count + 1;
            _notifications.Add(notification);
        }

        public static List<Models.Notification> GetAllNotifications()
        {
            return _notifications.ToList();
        }

        public static List<Models.Notification> GetUnsentNotifications()
        {
            return _notifications.Where(n => !n.IsSent).ToList();
        }

        public static void MarkNotificationAsSent(int notificationId)
        {
            var notification = _notifications.FirstOrDefault(n => n.NotificationID == notificationId);
            if (notification != null)
            {
                notification.IsSent = true;
            }
        }

        public static void DeleteNotification(int notificationId)
        {
            _notifications.RemoveAll(n => n.NotificationID == notificationId);
        }
        #endregion

        #region Class Methods
        public static void AddClass(Models.Class @class)
        {
            @class.ClassID = _classes.Count + 1;
            _classes.Add(@class);
        }

        public static List<Models.Class> GetAllClasses()
        {
            return _classes.ToList();
        }

        public static Models.Class GetClassById(int classId)
        {
            return _classes.FirstOrDefault(c => c.ClassID == classId);
        }

        public static Models.Class GetClassByName(string className)
        {
            return _classes.FirstOrDefault(c => c.ClassName == className);
        }

        public static void UpdateClass(Models.Class @class)
        {
            var existing = _classes.FirstOrDefault(c => c.ClassID == @class.ClassID);
            if (existing != null)
            {
                existing.ClassName = @class.ClassName;
                existing.Section = @class.Section;
                existing.ClassTeacherName = @class.ClassTeacherName;
                existing.ClassTeacherID = @class.ClassTeacherID;
                existing.TotalStudents = @class.TotalStudents;
                existing.Room = @class.Room;
                existing.Schedule = @class.Schedule;
                existing.IsActive = @class.IsActive;
            }
        }

        public static void DeleteClass(int classId)
        {
            _classes.RemoveAll(c => c.ClassID == classId);
        }
        #endregion
    }
}
