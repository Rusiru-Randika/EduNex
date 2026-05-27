using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace EduNex
{
    public class DatabaseHelper
    {
        private static string _connectionString = "Server=localhost;Database=EduNex;User Id=root;Password=;";

        public static void SetConnectionString(string connString)
        {
            _connectionString = connString;
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        public static bool TestDatabaseConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Database connection failed!\n" + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #region Student Methods
        public static void AddStudent(Models.Student student)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Students 
                        (Name, RollNumber, DateOfBirth, Gender, Address, PhoneNumber, 
                         ParentName, ParentPhoneNumber, ParentEmail, EnrollmentDate, Class, MonthlyFee, IsActive)
                        VALUES (@Name, @RollNumber, @DateOfBirth, @Gender, @Address, @PhoneNumber,
                                @ParentName, @ParentPhoneNumber, @ParentEmail, @EnrollmentDate, @Class, @MonthlyFee, @IsActive);";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", student.Name ?? "");
                    cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber ?? "");
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender ?? "");
                    cmd.Parameters.AddWithValue("@Address", student.Address ?? "");
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@ParentName", student.ParentName ?? "");
                    cmd.Parameters.AddWithValue("@ParentPhoneNumber", student.ParentPhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@ParentEmail", student.ParentEmail ?? "");
                    cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);
                    cmd.Parameters.AddWithValue("@Class", student.Class ?? "");
                    cmd.Parameters.AddWithValue("@MonthlyFee", student.MonthlyFee);
                    cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student: " + ex.Message);
            }
        }

        public static List<Models.Student> GetAllStudents()
        {
            List<Models.Student> students = new List<Models.Student>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Students WHERE IsActive = TRUE ORDER BY StudentID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(MapStudent(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving students: " + ex.Message);
            }
            return students;
        }

        public static Models.Student GetStudentById(int studentId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Students WHERE StudentID = @StudentID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapStudent(reader);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return null;
        }

        public static void UpdateStudent(Models.Student student)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Students SET 
                        Name = @Name, RollNumber = @RollNumber, DateOfBirth = @DateOfBirth,
                        Gender = @Gender, Address = @Address, PhoneNumber = @PhoneNumber,
                        ParentName = @ParentName, ParentPhoneNumber = @ParentPhoneNumber,
                        ParentEmail = @ParentEmail, Class = @Class, MonthlyFee = @MonthlyFee, IsActive = @IsActive
                        WHERE StudentID = @StudentID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", student.StudentID);
                    cmd.Parameters.AddWithValue("@Name", student.Name ?? "");
                    cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber ?? "");
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", student.Gender ?? "");
                    cmd.Parameters.AddWithValue("@Address", student.Address ?? "");
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@ParentName", student.ParentName ?? "");
                    cmd.Parameters.AddWithValue("@ParentPhoneNumber", student.ParentPhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@ParentEmail", student.ParentEmail ?? "");
                    cmd.Parameters.AddWithValue("@Class", student.Class ?? "");
                    cmd.Parameters.AddWithValue("@MonthlyFee", student.MonthlyFee);
                    cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error updating student: " + ex.Message); }
        }

        public static void DeleteStudent(int studentId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Students WHERE StudentID = @StudentID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error deleting student: " + ex.Message); }
        }

        private static Models.Student MapStudent(MySqlDataReader reader)
        {
            return new Models.Student
            {
                StudentID = Convert.ToInt32(reader["StudentID"]),
                Name = reader["Name"].ToString(),
                RollNumber = reader["RollNumber"].ToString(),
                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : DateTime.MinValue,
                Gender = reader["Gender"].ToString(),
                Address = reader["Address"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                ParentName = reader["ParentName"].ToString(),
                ParentPhoneNumber = reader["ParentPhoneNumber"].ToString(),
                ParentEmail = reader["ParentEmail"].ToString(),
                EnrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]),
                Class = reader["Class"].ToString(),
                MonthlyFee = Convert.ToDecimal(reader["MonthlyFee"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }
        #endregion

        #region Teacher Methods
        public static void AddTeacher(Models.Teacher teacher)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Teachers (Name, Email, Password, PhoneNumber, Subject, Class, JoiningDate, IsActive)
                        VALUES (@Name, @Email, @Password, @PhoneNumber, @Subject, @Class, @JoiningDate, @IsActive)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", teacher.Name ?? "");
                    cmd.Parameters.AddWithValue("@Email", teacher.Email ?? "");
                    cmd.Parameters.AddWithValue("@Password", teacher.Password ?? "");
                    cmd.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@Subject", teacher.Subject ?? "");
                    cmd.Parameters.AddWithValue("@Class", teacher.Class ?? "");
                    cmd.Parameters.AddWithValue("@JoiningDate", teacher.JoiningDate);
                    cmd.Parameters.AddWithValue("@IsActive", teacher.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error adding teacher: " + ex.Message); }
        }

        public static List<Models.Teacher> GetAllTeachers()
        {
            List<Models.Teacher> teachers = new List<Models.Teacher>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Teachers WHERE IsActive = TRUE ORDER BY TeacherID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teachers.Add(MapTeacher(reader));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error retrieving teachers: " + ex.Message); }
            return teachers;
        }

        public static Models.Teacher GetTeacherById(int teacherId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Teachers WHERE TeacherID = @TeacherID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapTeacher(reader);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return null;
        }

        public static Models.Teacher GetTeacherByEmail(string email)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Teachers WHERE Email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapTeacher(reader);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return null;
        }

        public static void UpdateTeacher(Models.Teacher teacher)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Teachers SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber,
                        Subject = @Subject, Class = @Class, IsActive = @IsActive WHERE TeacherID = @TeacherID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                    cmd.Parameters.AddWithValue("@Name", teacher.Name ?? "");
                    cmd.Parameters.AddWithValue("@Email", teacher.Email ?? "");
                    cmd.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@Subject", teacher.Subject ?? "");
                    cmd.Parameters.AddWithValue("@Class", teacher.Class ?? "");
                    cmd.Parameters.AddWithValue("@IsActive", teacher.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static void DeleteTeacher(int teacherId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Teachers WHERE TeacherID = @TeacherID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private static Models.Teacher MapTeacher(MySqlDataReader reader)
        {
            return new Models.Teacher
            {
                TeacherID = Convert.ToInt32(reader["TeacherID"]),
                Name = reader["Name"].ToString(),
                Email = reader["Email"].ToString(),
                Password = reader["Password"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Subject = reader["Subject"].ToString(),
                Class = reader["Class"].ToString(),
                JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }
        #endregion

        #region Attendance Methods
        public static void AddAttendance(Models.Attendance attendance)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Attendance (StudentID, StudentName, AttendanceDate, Status, Remarks, TeacherID) VALUES (@SID, @SName, @Date, @Status, @Remarks, @TID)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", attendance.StudentID);
                    cmd.Parameters.AddWithValue("@SName", attendance.StudentName);
                    cmd.Parameters.AddWithValue("@Date", attendance.AttendanceDate);
                    cmd.Parameters.AddWithValue("@Status", attendance.Status);
                    cmd.Parameters.AddWithValue("@Remarks", attendance.Remarks);
                    cmd.Parameters.AddWithValue("@TID", attendance.TeacherID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static List<Models.Attendance> GetAttendanceByDate(DateTime date)
        {
            List<Models.Attendance> list = new List<Models.Attendance>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Attendance WHERE DATE(AttendanceDate) = @Date";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.Attendance {
                                AttendanceID = Convert.ToInt32(r["AttendanceID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                AttendanceDate = Convert.ToDateTime(r["AttendanceDate"]),
                                Status = r["Status"].ToString(),
                                Remarks = r["Remarks"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.Attendance> GetAttendanceByStudent(int studentId)
        {
            List<Models.Attendance> list = new List<Models.Attendance>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Attendance WHERE StudentID = @SID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", studentId);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.Attendance {
                                AttendanceID = Convert.ToInt32(r["AttendanceID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                AttendanceDate = Convert.ToDateTime(r["AttendanceDate"]),
                                Status = r["Status"].ToString(),
                                Remarks = r["Remarks"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.Attendance> GetAllAttendances()
        {
            List<Models.Attendance> list = new List<Models.Attendance>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Attendance ORDER BY AttendanceDate DESC", conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.Attendance {
                                AttendanceID = Convert.ToInt32(r["AttendanceID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                AttendanceDate = Convert.ToDateTime(r["AttendanceDate"]),
                                Status = r["Status"].ToString(),
                                Remarks = r["Remarks"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static void UpdateAttendance(Models.Attendance attendance)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Attendance SET Status = @Status, Remarks = @Remarks WHERE AttendanceID = @AID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AID", attendance.AttendanceID);
                    cmd.Parameters.AddWithValue("@Status", attendance.Status);
                    cmd.Parameters.AddWithValue("@Remarks", attendance.Remarks);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static void DeleteAttendance(int attendanceId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Attendance WHERE AttendanceID = @AID", conn);
                    cmd.Parameters.AddWithValue("@AID", attendanceId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
        #endregion

        #region Fee Methods
        public static void AddFee(Models.ClassFee fee)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO ClassFees (StudentID, StudentName, Amount, DueDate, PaidDate, Status, Description, TeacherID) VALUES (@SID, @Name, @Amt, @Due, @Paid, @Stat, @Desc, @TID)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", fee.StudentID);
                    cmd.Parameters.AddWithValue("@Name", fee.StudentName);
                    cmd.Parameters.AddWithValue("@Amt", fee.Amount);
                    cmd.Parameters.AddWithValue("@Due", fee.DueDate);
                    cmd.Parameters.AddWithValue("@Paid", fee.PaidDate != DateTime.MinValue ? fee.PaidDate : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Stat", fee.Status);
                    cmd.Parameters.AddWithValue("@Desc", fee.Description);
                    cmd.Parameters.AddWithValue("@TID", fee.TeacherID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static List<Models.ClassFee> GetAllFees()
        {
            List<Models.ClassFee> list = new List<Models.ClassFee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM ClassFees", conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.ClassFee {
                                FeeID = Convert.ToInt32(r["FeeID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                Amount = Convert.ToDecimal(r["Amount"]),
                                DueDate = Convert.ToDateTime(r["DueDate"]),
                                PaidDate = r["PaidDate"] != DBNull.Value ? Convert.ToDateTime(r["PaidDate"]) : DateTime.MinValue,
                                Status = r["Status"].ToString(),
                                Description = r["Description"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.ClassFee> GetFeesByStudent(int studentId)
        {
            List<Models.ClassFee> list = new List<Models.ClassFee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ClassFees WHERE StudentID = @SID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", studentId);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.ClassFee {
                                FeeID = Convert.ToInt32(r["FeeID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                Amount = Convert.ToDecimal(r["Amount"]),
                                Status = r["Status"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.ClassFee> GetFeesByStatus(string status)
        {
            List<Models.ClassFee> list = new List<Models.ClassFee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ClassFees WHERE Status = @Status";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Status", status);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.ClassFee {
                                FeeID = Convert.ToInt32(r["FeeID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                Amount = Convert.ToDecimal(r["Amount"]),
                                Status = r["Status"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static void UpdateFee(Models.ClassFee fee)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE ClassFees SET Status = @Status, PaidDate = @Paid WHERE FeeID = @FID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FID", fee.FeeID);
                    cmd.Parameters.AddWithValue("@Status", fee.Status);
                    cmd.Parameters.AddWithValue("@Paid", fee.PaidDate != DateTime.MinValue ? fee.PaidDate : (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static void DeleteFee(int feeId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM ClassFees WHERE FeeID = @FID", conn);
                    cmd.Parameters.AddWithValue("@FID", feeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
        #endregion

        #region Exam Result Methods
        public static void AddExamResult(Models.ExamResult result)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO ExamResults (StudentID, StudentName, ExamName, Subject, MarksObtained, TotalMarks, Percentage, Grade, ExamDate, TeacherID)
                        VALUES (@SID, @SName, @Exam, @Sub, @Marks, @Total, @Pct, @Grade, @Date, @TID)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", result.StudentID);
                    cmd.Parameters.AddWithValue("@SName", result.StudentName);
                    cmd.Parameters.AddWithValue("@Exam", result.ExamName);
                    cmd.Parameters.AddWithValue("@Sub", result.Subject);
                    cmd.Parameters.AddWithValue("@Marks", result.MarksObtained);
                    cmd.Parameters.AddWithValue("@Total", result.TotalMarks);
                    cmd.Parameters.AddWithValue("@Pct", result.Percentage);
                    cmd.Parameters.AddWithValue("@Grade", result.Grade);
                    cmd.Parameters.AddWithValue("@Date", result.ExamDate);
                    cmd.Parameters.AddWithValue("@TID", result.TeacherID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static List<Models.ExamResult> GetAllExamResults()
        {
            List<Models.ExamResult> list = new List<Models.ExamResult>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM ExamResults", conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.ExamResult {
                                ResultID = Convert.ToInt32(r["ResultID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                ExamName = r["ExamName"].ToString(),
                                Subject = r["Subject"].ToString(),
                                MarksObtained = Convert.ToDecimal(r["MarksObtained"]),
                                TotalMarks = Convert.ToDecimal(r["TotalMarks"]),
                                Percentage = Convert.ToDecimal(r["Percentage"]),
                                Grade = r["Grade"].ToString(),
                                ExamDate = Convert.ToDateTime(r["ExamDate"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.ExamResult> GetResultsByStudent(int studentId)
        {
            List<Models.ExamResult> list = new List<Models.ExamResult>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ExamResults WHERE StudentID = @SID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", studentId);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.ExamResult {
                                ResultID = Convert.ToInt32(r["ResultID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                ExamName = r["ExamName"].ToString(),
                                Percentage = Convert.ToDecimal(r["Percentage"]),
                                Grade = r["Grade"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.ExamResult> GetResultsByExam(string examName)
        {
            List<Models.ExamResult> list = new List<Models.ExamResult>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ExamResults WHERE ExamName = @Exam";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Exam", examName);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.ExamResult {
                                ResultID = Convert.ToInt32(r["ResultID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                Percentage = Convert.ToDecimal(r["Percentage"]),
                                Grade = r["Grade"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static void UpdateExamResult(Models.ExamResult result)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE ExamResults SET MarksObtained = @Marks, TotalMarks = @Total, Percentage = @Pct, Grade = @Grade WHERE ResultID = @RID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RID", result.ResultID);
                    cmd.Parameters.AddWithValue("@Marks", result.MarksObtained);
                    cmd.Parameters.AddWithValue("@Total", result.TotalMarks);
                    cmd.Parameters.AddWithValue("@Pct", result.Percentage);
                    cmd.Parameters.AddWithValue("@Grade", result.Grade);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static void DeleteExamResult(int resultId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM ExamResults WHERE ResultID = @RID", conn);
                    cmd.Parameters.AddWithValue("@RID", resultId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
        #endregion

        #region Notification Methods
        public static void AddNotification(Models.Notification n)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Notifications (StudentID, StudentName, ParentEmail, ParentPhoneNumber, Message, NotificationType, SentDate, IsSent, TeacherID)
                        VALUES (@SID, @SName, @Email, @Phone, @Msg, @Type, @Date, @Sent, @TID)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SID", n.StudentID);
                    cmd.Parameters.AddWithValue("@SName", n.StudentName);
                    cmd.Parameters.AddWithValue("@Email", n.ParentEmail);
                    cmd.Parameters.AddWithValue("@Phone", n.ParentPhoneNumber);
                    cmd.Parameters.AddWithValue("@Msg", n.Message);
                    cmd.Parameters.AddWithValue("@Type", n.NotificationType);
                    cmd.Parameters.AddWithValue("@Date", n.SentDate);
                    cmd.Parameters.AddWithValue("@Sent", n.IsSent);
                    cmd.Parameters.AddWithValue("@TID", n.TeacherID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static List<Models.Notification> GetAllNotifications()
        {
            List<Models.Notification> list = new List<Models.Notification>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Notifications", conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.Notification {
                                NotificationID = Convert.ToInt32(r["NotificationID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                StudentName = r["StudentName"].ToString(),
                                Message = r["Message"].ToString(),
                                IsSent = Convert.ToBoolean(r["IsSent"]),
                                NotificationType = r["NotificationType"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static List<Models.Notification> GetUnsentNotifications()
        {
            List<Models.Notification> list = new List<Models.Notification>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Notifications WHERE IsSent = FALSE", conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.Notification {
                                NotificationID = Convert.ToInt32(r["NotificationID"]),
                                StudentID = Convert.ToInt32(r["StudentID"]),
                                Message = r["Message"].ToString(),
                                IsSent = false
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static void MarkNotificationAsSent(int notificationId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Notifications SET IsSent = TRUE WHERE NotificationID = @NID", conn);
                    cmd.Parameters.AddWithValue("@NID", notificationId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static void DeleteNotification(int notificationId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Notifications WHERE NotificationID = @NID", conn);
                    cmd.Parameters.AddWithValue("@NID", notificationId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
        #endregion

        #region Class Methods
        public static void AddClass(Models.Class c)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Classes (ClassName, Section, ClassTeacherID, ClassTeacherName, TotalStudents, Room, Schedule, CreatedDate, IsActive)
                        VALUES (@Name, @Sec, @TID, @TName, @Total, @Room, @Sch, @Date, @Active)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", c.ClassName);
                    cmd.Parameters.AddWithValue("@Sec", c.Section);
                    cmd.Parameters.AddWithValue("@TID", c.ClassTeacherID);
                    cmd.Parameters.AddWithValue("@TName", c.ClassTeacherName);
                    cmd.Parameters.AddWithValue("@Total", c.TotalStudents);
                    cmd.Parameters.AddWithValue("@Room", c.Room);
                    cmd.Parameters.AddWithValue("@Sch", c.Schedule);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Active", c.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static List<Models.Class> GetAllClasses()
        {
            List<Models.Class> list = new List<Models.Class>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Classes", conn);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Models.Class {
                                ClassID = Convert.ToInt32(r["ClassID"]),
                                ClassName = r["ClassName"].ToString(),
                                Section = r["Section"].ToString(),
                                ClassTeacherName = r["ClassTeacherName"].ToString(),
                                TotalStudents = Convert.ToInt32(r["TotalStudents"]),
                                IsActive = Convert.ToBoolean(r["IsActive"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }

        public static Models.Class GetClassById(int classId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Classes WHERE ClassID = @CID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CID", classId);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                            return new Models.Class {
                                ClassID = Convert.ToInt32(r["ClassID"]),
                                ClassName = r["ClassName"].ToString(),
                                Section = r["Section"].ToString()
                            };
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return null;
        }

        public static Models.Class GetClassByName(string className)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Classes WHERE ClassName = @Name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", className);
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                            return new Models.Class {
                                ClassID = Convert.ToInt32(r["ClassID"]),
                                ClassName = r["ClassName"].ToString(),
                                Section = r["Section"].ToString()
                            };
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return null;
        }

        public static void UpdateClass(Models.Class c)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Classes SET ClassName = @Name, Section = @Sec, ClassTeacherID = @TID, ClassTeacherName = @TName,
                        TotalStudents = @Total, Room = @Room, Schedule = @Sch, IsActive = @Active WHERE ClassID = @CID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CID", c.ClassID);
                    cmd.Parameters.AddWithValue("@Name", c.ClassName);
                    cmd.Parameters.AddWithValue("@Sec", c.Section);
                    cmd.Parameters.AddWithValue("@TID", c.ClassTeacherID);
                    cmd.Parameters.AddWithValue("@TName", c.ClassTeacherName);
                    cmd.Parameters.AddWithValue("@Total", c.TotalStudents);
                    cmd.Parameters.AddWithValue("@Room", c.Room);
                    cmd.Parameters.AddWithValue("@Sch", c.Schedule);
                    cmd.Parameters.AddWithValue("@Active", c.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public static void DeleteClass(int classId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM Classes WHERE ClassID = @CID", conn);
                    cmd.Parameters.AddWithValue("@CID", classId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
        #endregion
    }
}
