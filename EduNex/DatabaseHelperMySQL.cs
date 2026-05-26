using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EduNex
{
    /// <summary>
    /// DatabaseHelper for MySQL Integration
    /// This is the MYSQL version - Replace existing DatabaseHelper.cs with this file
    /// </summary>
    public class DatabaseHelperMySQL
    {
        // MySQL Connection String
        private static string _connectionString = 
            "Server=localhost;Database=EduNex;User Id=root;Password=;";

        public static void SetConnectionString(string connString)
        {
            _connectionString = connString;
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        /// <summary>
        /// Test database connection
        /// </summary>
        public static bool TestDatabaseConnection()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    MessageBox.Show("✅ Database connection successful!", "Connection Test", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Connection failed:\n" + ex.Message, "Connection Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                @ParentName, @ParentPhoneNumber, @ParentEmail, @EnrollmentDate, @Class, @MonthlyFee, @IsActive)";

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
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        students.Add(new Models.Student
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
                        });
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
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving student: " + ex.Message);
            }
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
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
            }
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
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student: " + ex.Message);
            }
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
            catch (Exception ex)
            {
                MessageBox.Show("Error adding teacher: " + ex.Message);
            }
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
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        teachers.Add(new Models.Teacher
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
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving teachers: " + ex.Message);
            }
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
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving teacher: " + ex.Message);
            }
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
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving teacher: " + ex.Message);
            }
            return null;
        }

        public static void UpdateTeacher(Models.Teacher teacher)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Teachers SET 
                        Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber,
                        Subject = @Subject, Class = @Class, IsActive = @IsActive
                        WHERE TeacherID = @TeacherID";

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
            catch (Exception ex)
            {
                MessageBox.Show("Error updating teacher: " + ex.Message);
            }
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
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting teacher: " + ex.Message);
            }
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
                    string query = @"INSERT INTO Attendance 
                        (StudentID, StudentName, AttendanceDate, Status, Remarks, TeacherID)
                        VALUES (@StudentID, @StudentName, @AttendanceDate, @Status, @Remarks, @TeacherID)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", attendance.StudentID);
                    cmd.Parameters.AddWithValue("@StudentName", attendance.StudentName ?? "");
                    cmd.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
                    cmd.Parameters.AddWithValue("@Status", attendance.Status ?? "");
                    cmd.Parameters.AddWithValue("@Remarks", attendance.Remarks ?? "");
                    cmd.Parameters.AddWithValue("@TeacherID", attendance.TeacherID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding attendance: " + ex.Message);
            }
        }

        public static List<Models.Attendance> GetAttendanceByDate(DateTime date)
        {
            List<Models.Attendance> attendances = new List<Models.Attendance>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Attendance WHERE DATE(AttendanceDate) = @Date ORDER BY StudentID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        attendances.Add(new Models.Attendance
                        {
                            AttendanceID = Convert.ToInt32(reader["AttendanceID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                            Status = reader["Status"].ToString(),
                            Remarks = reader["Remarks"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving attendance: " + ex.Message);
            }
            return attendances;
        }

        public static List<Models.Attendance> GetAttendanceByStudent(int studentId)
        {
            List<Models.Attendance> attendances = new List<Models.Attendance>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Attendance WHERE StudentID = @StudentID ORDER BY AttendanceDate DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        attendances.Add(new Models.Attendance
                        {
                            AttendanceID = Convert.ToInt32(reader["AttendanceID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                            Status = reader["Status"].ToString(),
                            Remarks = reader["Remarks"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving attendance: " + ex.Message);
            }
            return attendances;
        }

        public static List<Models.Attendance> GetAllAttendances()
        {
            List<Models.Attendance> attendances = new List<Models.Attendance>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Attendance ORDER BY AttendanceDate DESC, StudentID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        attendances.Add(new Models.Attendance
                        {
                            AttendanceID = Convert.ToInt32(reader["AttendanceID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            AttendanceDate = Convert.ToDateTime(reader["AttendanceDate"]),
                            Status = reader["Status"].ToString(),
                            Remarks = reader["Remarks"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving attendance: " + ex.Message);
            }
            return attendances;
        }

        public static void UpdateAttendance(Models.Attendance attendance)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Attendance SET 
                        Status = @Status, Remarks = @Remarks
                        WHERE AttendanceID = @AttendanceID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AttendanceID", attendance.AttendanceID);
                    cmd.Parameters.AddWithValue("@Status", attendance.Status ?? "");
                    cmd.Parameters.AddWithValue("@Remarks", attendance.Remarks ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating attendance: " + ex.Message);
            }
        }

        public static void DeleteAttendance(int attendanceId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Attendance WHERE AttendanceID = @AttendanceID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting attendance: " + ex.Message);
            }
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
                    string query = @"INSERT INTO ClassFees 
                        (StudentID, StudentName, Amount, DueDate, PaidDate, Status, Description, TeacherID)
                        VALUES (@StudentID, @StudentName, @Amount, @DueDate, @PaidDate, @Status, @Description, @TeacherID)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", fee.StudentID);
                    cmd.Parameters.AddWithValue("@StudentName", fee.StudentName ?? "");
                    cmd.Parameters.AddWithValue("@Amount", fee.Amount);
                    cmd.Parameters.AddWithValue("@DueDate", fee.DueDate);
                    cmd.Parameters.AddWithValue("@PaidDate", fee.PaidDate != DateTime.MinValue ? (object)fee.PaidDate : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", fee.Status ?? "");
                    cmd.Parameters.AddWithValue("@Description", fee.Description ?? "");
                    cmd.Parameters.AddWithValue("@TeacherID", fee.TeacherID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding fee: " + ex.Message);
            }
        }

        public static List<Models.ClassFee> GetAllFees()
        {
            List<Models.ClassFee> fees = new List<Models.ClassFee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ClassFees ORDER BY FeeID DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        fees.Add(new Models.ClassFee
                        {
                            FeeID = Convert.ToInt32(reader["FeeID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            PaidDate = reader["PaidDate"] != DBNull.Value ? Convert.ToDateTime(reader["PaidDate"]) : DateTime.MinValue,
                            Status = reader["Status"].ToString(),
                            Description = reader["Description"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving fees: " + ex.Message);
            }
            return fees;
        }

        public static List<Models.ClassFee> GetFeesByStudent(int studentId)
        {
            List<Models.ClassFee> fees = new List<Models.ClassFee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ClassFees WHERE StudentID = @StudentID ORDER BY DueDate DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        fees.Add(new Models.ClassFee
                        {
                            FeeID = Convert.ToInt32(reader["FeeID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            PaidDate = reader["PaidDate"] != DBNull.Value ? Convert.ToDateTime(reader["PaidDate"]) : DateTime.MinValue,
                            Status = reader["Status"].ToString(),
                            Description = reader["Description"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving fees: " + ex.Message);
            }
            return fees;
        }

        public static List<Models.ClassFee> GetFeesByStatus(string status)
        {
            List<Models.ClassFee> fees = new List<Models.ClassFee>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ClassFees WHERE Status = @Status ORDER BY DueDate";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Status", status);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        fees.Add(new Models.ClassFee
                        {
                            FeeID = Convert.ToInt32(reader["FeeID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            PaidDate = reader["PaidDate"] != DBNull.Value ? Convert.ToDateTime(reader["PaidDate"]) : DateTime.MinValue,
                            Status = reader["Status"].ToString(),
                            Description = reader["Description"].ToString(),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving fees: " + ex.Message);
            }
            return fees;
        }

        public static void UpdateFee(Models.ClassFee fee)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE ClassFees SET 
                        Status = @Status, PaidDate = @PaidDate
                        WHERE FeeID = @FeeID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FeeID", fee.FeeID);
                    cmd.Parameters.AddWithValue("@Status", fee.Status ?? "");
                    cmd.Parameters.AddWithValue("@PaidDate", fee.PaidDate != DateTime.MinValue ? (object)fee.PaidDate : DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating fee: " + ex.Message);
            }
        }

        public static void DeleteFee(int feeId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM ClassFees WHERE FeeID = @FeeID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FeeID", feeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting fee: " + ex.Message);
            }
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
                    string query = @"INSERT INTO ExamResults 
                        (StudentID, StudentName, ExamName, Subject, MarksObtained, TotalMarks, Percentage, Grade, ExamDate, TeacherID)
                        VALUES (@StudentID, @StudentName, @ExamName, @Subject, @MarksObtained, @TotalMarks, @Percentage, @Grade, @ExamDate, @TeacherID)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", result.StudentID);
                    cmd.Parameters.AddWithValue("@StudentName", result.StudentName ?? "");
                    cmd.Parameters.AddWithValue("@ExamName", result.ExamName ?? "");
                    cmd.Parameters.AddWithValue("@Subject", result.Subject ?? "");
                    cmd.Parameters.AddWithValue("@MarksObtained", result.MarksObtained);
                    cmd.Parameters.AddWithValue("@TotalMarks", result.TotalMarks);
                    cmd.Parameters.AddWithValue("@Percentage", result.Percentage);
                    cmd.Parameters.AddWithValue("@Grade", result.Grade ?? "");
                    cmd.Parameters.AddWithValue("@ExamDate", result.ExamDate);
                    cmd.Parameters.AddWithValue("@TeacherID", result.TeacherID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding exam result: " + ex.Message);
            }
        }

        public static List<Models.ExamResult> GetAllExamResults()
        {
            List<Models.ExamResult> results = new List<Models.ExamResult>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ExamResults ORDER BY ExamDate DESC, StudentID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        results.Add(new Models.ExamResult
                        {
                            ResultID = Convert.ToInt32(reader["ResultID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            ExamName = reader["ExamName"].ToString(),
                            Subject = reader["Subject"].ToString(),
                            MarksObtained = Convert.ToDecimal(reader["MarksObtained"]),
                            TotalMarks = Convert.ToDecimal(reader["TotalMarks"]),
                            Percentage = Convert.ToDecimal(reader["Percentage"]),
                            Grade = reader["Grade"].ToString(),
                            ExamDate = Convert.ToDateTime(reader["ExamDate"]),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving exam results: " + ex.Message);
            }
            return results;
        }

        public static List<Models.ExamResult> GetResultsByStudent(int studentId)
        {
            List<Models.ExamResult> results = new List<Models.ExamResult>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ExamResults WHERE StudentID = @StudentID ORDER BY ExamDate DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        results.Add(new Models.ExamResult
                        {
                            ResultID = Convert.ToInt32(reader["ResultID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            ExamName = reader["ExamName"].ToString(),
                            Subject = reader["Subject"].ToString(),
                            MarksObtained = Convert.ToDecimal(reader["MarksObtained"]),
                            TotalMarks = Convert.ToDecimal(reader["TotalMarks"]),
                            Percentage = Convert.ToDecimal(reader["Percentage"]),
                            Grade = reader["Grade"].ToString(),
                            ExamDate = Convert.ToDateTime(reader["ExamDate"]),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving exam results: " + ex.Message);
            }
            return results;
        }

        public static List<Models.ExamResult> GetResultsByExam(string examName)
        {
            List<Models.ExamResult> results = new List<Models.ExamResult>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ExamResults WHERE ExamName = @ExamName ORDER BY Percentage DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ExamName", examName);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        results.Add(new Models.ExamResult
                        {
                            ResultID = Convert.ToInt32(reader["ResultID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            ExamName = reader["ExamName"].ToString(),
                            Subject = reader["Subject"].ToString(),
                            MarksObtained = Convert.ToDecimal(reader["MarksObtained"]),
                            TotalMarks = Convert.ToDecimal(reader["TotalMarks"]),
                            Percentage = Convert.ToDecimal(reader["Percentage"]),
                            Grade = reader["Grade"].ToString(),
                            ExamDate = Convert.ToDateTime(reader["ExamDate"]),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving exam results: " + ex.Message);
            }
            return results;
        }

        public static void UpdateExamResult(Models.ExamResult result)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE ExamResults SET 
                        MarksObtained = @MarksObtained, TotalMarks = @TotalMarks, 
                        Percentage = @Percentage, Grade = @Grade
                        WHERE ResultID = @ResultID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ResultID", result.ResultID);
                    cmd.Parameters.AddWithValue("@MarksObtained", result.MarksObtained);
                    cmd.Parameters.AddWithValue("@TotalMarks", result.TotalMarks);
                    cmd.Parameters.AddWithValue("@Percentage", result.Percentage);
                    cmd.Parameters.AddWithValue("@Grade", result.Grade ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating exam result: " + ex.Message);
            }
        }

        public static void DeleteExamResult(int resultId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM ExamResults WHERE ResultID = @ResultID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ResultID", resultId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting exam result: " + ex.Message);
            }
        }

        #endregion

        #region Notification Methods

        public static void AddNotification(Models.Notification notification)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Notifications 
                        (StudentID, StudentName, ParentEmail, ParentPhoneNumber, Message, NotificationType, SentDate, IsSent, TeacherID)
                        VALUES (@StudentID, @StudentName, @ParentEmail, @ParentPhoneNumber, @Message, @NotificationType, @SentDate, @IsSent, @TeacherID)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", notification.StudentID);
                    cmd.Parameters.AddWithValue("@StudentName", notification.StudentName ?? "");
                    cmd.Parameters.AddWithValue("@ParentEmail", notification.ParentEmail ?? "");
                    cmd.Parameters.AddWithValue("@ParentPhoneNumber", notification.ParentPhoneNumber ?? "");
                    cmd.Parameters.AddWithValue("@Message", notification.Message ?? "");
                    cmd.Parameters.AddWithValue("@NotificationType", notification.NotificationType ?? "");
                    cmd.Parameters.AddWithValue("@SentDate", notification.SentDate);
                    cmd.Parameters.AddWithValue("@IsSent", notification.IsSent);
                    cmd.Parameters.AddWithValue("@TeacherID", notification.TeacherID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding notification: " + ex.Message);
            }
        }

        public static List<Models.Notification> GetAllNotifications()
        {
            List<Models.Notification> notifications = new List<Models.Notification>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Notifications ORDER BY SentDate DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        notifications.Add(new Models.Notification
                        {
                            NotificationID = Convert.ToInt32(reader["NotificationID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            ParentEmail = reader["ParentEmail"].ToString(),
                            ParentPhoneNumber = reader["ParentPhoneNumber"].ToString(),
                            Message = reader["Message"].ToString(),
                            NotificationType = reader["NotificationType"].ToString(),
                            SentDate = Convert.ToDateTime(reader["SentDate"]),
                            IsSent = Convert.ToBoolean(reader["IsSent"]),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving notifications: " + ex.Message);
            }
            return notifications;
        }

        public static List<Models.Notification> GetUnsentNotifications()
        {
            List<Models.Notification> notifications = new List<Models.Notification>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Notifications WHERE IsSent = FALSE ORDER BY SentDate";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        notifications.Add(new Models.Notification
                        {
                            NotificationID = Convert.ToInt32(reader["NotificationID"]),
                            StudentID = Convert.ToInt32(reader["StudentID"]),
                            StudentName = reader["StudentName"].ToString(),
                            ParentEmail = reader["ParentEmail"].ToString(),
                            ParentPhoneNumber = reader["ParentPhoneNumber"].ToString(),
                            Message = reader["Message"].ToString(),
                            NotificationType = reader["NotificationType"].ToString(),
                            SentDate = Convert.ToDateTime(reader["SentDate"]),
                            IsSent = Convert.ToBoolean(reader["IsSent"]),
                            TeacherID = Convert.ToInt32(reader["TeacherID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving notifications: " + ex.Message);
            }
            return notifications;
        }

        public static void MarkNotificationAsSent(int notificationId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Notifications SET IsSent = TRUE WHERE NotificationID = @NotificationID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error marking notification as sent: " + ex.Message);
            }
        }

        public static void DeleteNotification(int notificationId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Notifications WHERE NotificationID = @NotificationID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NotificationID", notificationId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting notification: " + ex.Message);
            }
        }

        #endregion

        #region Class Methods

        public static void AddClass(Models.Class @class)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO Classes 
                        (ClassName, Section, ClassTeacherID, ClassTeacherName, TotalStudents, Room, Schedule, CreatedDate, IsActive)
                        VALUES (@ClassName, @Section, @ClassTeacherID, @ClassTeacherName, @TotalStudents, @Room, @Schedule, @CreatedDate, @IsActive)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ClassName", @class.ClassName ?? "");
                    cmd.Parameters.AddWithValue("@Section", @class.Section ?? "");
                    cmd.Parameters.AddWithValue("@ClassTeacherID", @class.ClassTeacherID);
                    cmd.Parameters.AddWithValue("@ClassTeacherName", @class.ClassTeacherName ?? "");
                    cmd.Parameters.AddWithValue("@TotalStudents", @class.TotalStudents);
                    cmd.Parameters.AddWithValue("@Room", @class.Room ?? "");
                    cmd.Parameters.AddWithValue("@Schedule", @class.Schedule ?? "");
                    cmd.Parameters.AddWithValue("@CreatedDate", @class.CreatedDate);
                    cmd.Parameters.AddWithValue("@IsActive", @class.IsActive);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding class: " + ex.Message);
            }
        }

        public static List<Models.Class> GetAllClasses()
        {
            List<Models.Class> classes = new List<Models.Class>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Classes WHERE IsActive = TRUE ORDER BY ClassName";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        classes.Add(new Models.Class
                        {
                            ClassID = Convert.ToInt32(reader["ClassID"]),
                            ClassName = reader["ClassName"].ToString(),
                            Section = reader["Section"].ToString(),
                            ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                            ClassTeacherName = reader["ClassTeacherName"].ToString(),
                            TotalStudents = Convert.ToInt32(reader["TotalStudents"]),
                            Room = reader["Room"].ToString(),
                            Schedule = reader["Schedule"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving classes: " + ex.Message);
            }
            return classes;
        }

        public static Models.Class GetClassById(int classId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Classes WHERE ClassID = @ClassID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ClassID", classId);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Models.Class
                        {
                            ClassID = Convert.ToInt32(reader["ClassID"]),
                            ClassName = reader["ClassName"].ToString(),
                            Section = reader["Section"].ToString(),
                            ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                            ClassTeacherName = reader["ClassTeacherName"].ToString(),
                            TotalStudents = Convert.ToInt32(reader["TotalStudents"]),
                            Room = reader["Room"].ToString(),
                            Schedule = reader["Schedule"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving class: " + ex.Message);
            }
            return null;
        }

        public static Models.Class GetClassByName(string className)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Classes WHERE ClassName = @ClassName";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ClassName", className);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Models.Class
                        {
                            ClassID = Convert.ToInt32(reader["ClassID"]),
                            ClassName = reader["ClassName"].ToString(),
                            Section = reader["Section"].ToString(),
                            ClassTeacherID = Convert.ToInt32(reader["ClassTeacherID"]),
                            ClassTeacherName = reader["ClassTeacherName"].ToString(),
                            TotalStudents = Convert.ToInt32(reader["TotalStudents"]),
                            Room = reader["Room"].ToString(),
                            Schedule = reader["Schedule"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving class: " + ex.Message);
            }
            return null;
        }

        public static void UpdateClass(Models.Class @class)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Classes SET 
                        ClassName = @ClassName, Section = @Section, ClassTeacherID = @ClassTeacherID,
                        ClassTeacherName = @ClassTeacherName, TotalStudents = @TotalStudents, 
                        Room = @Room, Schedule = @Schedule, IsActive = @IsActive
                        WHERE ClassID = @ClassID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ClassID", @class.ClassID);
                    cmd.Parameters.AddWithValue("@ClassName", @class.ClassName ?? "");
                    cmd.Parameters.AddWithValue("@Section", @class.Section ?? "");
                    cmd.Parameters.AddWithValue("@ClassTeacherID", @class.ClassTeacherID);
                    cmd.Parameters.AddWithValue("@ClassTeacherName", @class.ClassTeacherName ?? "");
                    cmd.Parameters.AddWithValue("@TotalStudents", @class.TotalStudents);
                    cmd.Parameters.AddWithValue("@Room", @class.Room ?? "");
                    cmd.Parameters.AddWithValue("@Schedule", @class.Schedule ?? "");
                    cmd.Parameters.AddWithValue("@IsActive", @class.IsActive);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating class: " + ex.Message);
            }
        }

        public static void DeleteClass(int classId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Classes WHERE ClassID = @ClassID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ClassID", classId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting class: " + ex.Message);
            }
        }

        #endregion
    }
}
