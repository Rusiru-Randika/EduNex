# Copilot Instructions

## Project Guidelines
- The user wants to replace in-memory storage with MySQL in DatabaseHelper.cs using the MySql.Data NuGet package.using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace EduNex
{
    public class DatabaseHelper
    {
        // Connection string for local MySQL. 
        // Update 'YOUR_PASSWORD' with your actual root password or Azure credentials.
        private static string _connectionString = "Server=localhost;Database=EduNex;User Id=root;Password=YOUR_PASSWORD;";

        public static void SetConnectionString(string connString)
        {
            _connectionString = connString;
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        /// <summary>
        /// Tests if the application can successfully connect to the MySQL database.
        /// </summary>
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
        #endregion

        #region Class Methods
        public static void AddClass(Models.Class c)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Classes (ClassName, Section, ClassTeacherID, ClassTeacherName, TotalStudents, Room, Schedule, CreatedDate, IsActive) VALUES (@Name, @Sec, @TID, @TName, @Total, @Room, @Sch, @Date, @Active)";
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
                                ClassTeacherName = r["ClassTeacherName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            return list;
        }
        #endregion
        
        // Note: Implement Fee, ExamResult and Notification Methods in the same pattern as above.
    }
}