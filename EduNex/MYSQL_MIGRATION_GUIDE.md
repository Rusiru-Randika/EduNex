# EduNex - Complete MySQL Migration Guide

## 🚀 **STEP-BY-STEP MIGRATION TO MYSQL**

### **Overview**
This guide will help you migrate your EduNex project from in-memory List storage to a persistent MySQL database.

---

## **PHASE 1: MYSQL INSTALLATION & SETUP**

### **Step 1.1: Download and Install MySQL Server**

1. **Go to:** https://dev.mysql.com/downloads/mysql/
2. **Download:** MySQL Community Server (latest version)
3. **Run installer** and follow these settings:
   - **Setup Type:** Development Default
   - **MySQL Servers to Configure:** MySQL Server 8.0
   - **MySQL Port:** 3306 (default)
   - **MySQL Root Password:** Set a strong password (or use empty for dev)
   - **MySQL User:** root

**Verification:**
```powershell
# Open PowerShell and test MySQL is running
mysql -u root -p
# Enter password when prompted
# Type: SELECT VERSION();
# You should see the MySQL version
# Type: EXIT;
```

---

### **Step 1.2: Install MySQL Workbench (Optional but Recommended)**

1. **Download:** https://dev.mysql.com/products/workbench/
2. **Install** - GUI tool for database management
3. **Use to:** Create databases, run SQL scripts, test queries

---

### **Step 1.3: Create EduNex Database**

**Option A: Using MySQL Command Line**
```powershell
# Open PowerShell as Administrator
mysql -u root -p

# At MySQL prompt, execute:
CREATE DATABASE EduNex;
USE EduNex;

# Verify
SHOW DATABASES;
```

**Option B: Using MySQL Workbench**
1. Connect to MySQL Server
2. Right-click on Databases
3. Create New Database
4. Name: `EduNex`
5. Click "Apply"

---

## **PHASE 2: CREATE DATABASE TABLES**

### **Step 2.1: Run SQL Schema Scripts**

Copy ALL the following SQL scripts and execute them in order:

```sql
-- ==========================================
-- TEACHERS TABLE
-- ==========================================
CREATE TABLE Teachers (
	TeacherID INT PRIMARY KEY AUTO_INCREMENT,
	Name VARCHAR(100) NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Password VARCHAR(255) NOT NULL,
	PhoneNumber VARCHAR(15),
	Subject VARCHAR(50),
	Class VARCHAR(20),
	JoiningDate DATETIME,
	IsActive BOOLEAN DEFAULT TRUE,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ==========================================
-- STUDENTS TABLE
-- ==========================================
CREATE TABLE Students (
	StudentID INT PRIMARY KEY AUTO_INCREMENT,
	Name VARCHAR(100) NOT NULL,
	RollNumber VARCHAR(20) UNIQUE NOT NULL,
	DateOfBirth DATE,
	Gender VARCHAR(10),
	Address VARCHAR(255),
	PhoneNumber VARCHAR(15),
	ParentName VARCHAR(100),
	ParentPhoneNumber VARCHAR(15),
	ParentEmail VARCHAR(100),
	EnrollmentDate DATETIME,
	Class VARCHAR(20),
	MonthlyFee DECIMAL(10, 2),
	IsActive BOOLEAN DEFAULT TRUE,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ==========================================
-- ATTENDANCE TABLE
-- ==========================================
CREATE TABLE Attendance (
	AttendanceID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	AttendanceDate DATE NOT NULL,
	Status VARCHAR(20),
	Remarks VARCHAR(255),
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- ==========================================
-- CLASSFEES TABLE
-- ==========================================
CREATE TABLE ClassFees (
	FeeID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	Amount DECIMAL(10, 2),
	DueDate DATE,
	PaidDate DATE,
	Status VARCHAR(20),
	Description VARCHAR(255),
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- ==========================================
-- EXAMRESULTS TABLE
-- ==========================================
CREATE TABLE ExamResults (
	ResultID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	ExamName VARCHAR(100),
	Subject VARCHAR(50),
	MarksObtained DECIMAL(5, 2),
	TotalMarks DECIMAL(5, 2),
	Percentage DECIMAL(5, 2),
	Grade VARCHAR(5),
	ExamDate DATE,
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- ==========================================
-- NOTIFICATIONS TABLE
-- ==========================================
CREATE TABLE Notifications (
	NotificationID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	ParentEmail VARCHAR(100),
	ParentPhoneNumber VARCHAR(15),
	Message TEXT,
	NotificationType VARCHAR(50),
	SentDate DATETIME,
	IsSent BOOLEAN DEFAULT FALSE,
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID) ON DELETE CASCADE,
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID) ON DELETE CASCADE
);

-- ==========================================
-- CLASSES TABLE
-- ==========================================
CREATE TABLE Classes (
	ClassID INT PRIMARY KEY AUTO_INCREMENT,
	ClassName VARCHAR(50) NOT NULL,
	Section VARCHAR(10),
	ClassTeacherID INT,
	ClassTeacherName VARCHAR(100),
	TotalStudents INT,
	Room VARCHAR(50),
	Schedule VARCHAR(255),
	CreatedDate DATETIME,
	IsActive BOOLEAN DEFAULT TRUE,
	FOREIGN KEY (ClassTeacherID) REFERENCES Teachers(TeacherID) ON DELETE SET NULL
);

-- ==========================================
-- INSERT SAMPLE DATA
-- ==========================================
INSERT INTO Teachers (Name, Email, Password, PhoneNumber, Subject, Class, JoiningDate, IsActive)
VALUES ('Mr. John Doe', 'john@example.com', 'password123', '1234567890', 'Mathematics', '10-A', NOW(), TRUE);
```

**How to run:**
- Copy all SQL above
- In MySQL Workbench: Paste into query editor → Execute
- Or in command line: Save as `schema.sql` → `mysql -u root -p EduNex < schema.sql`

---

## **PHASE 3: INSTALL MYSQL NUGET PACKAGE**

### **Step 3.1: Install MySQL.Data Package**

**In Visual Studio:**
1. **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. **Copy and paste:**
```powershell
Install-Package MySql.Data -Version 8.0.35
```

Or search in NuGet Package Manager GUI:
1. Right-click Project → Manage NuGet Packages
2. Search: `MySql.Data`
3. Click Install

---

## **PHASE 4: UPDATE DATABASEHELPER.CS**

### **Step 4.1: Add Using Statements**

Add these at the top of `DatabaseHelper.cs`:

```csharp
using MySql.Data.MySqlClient;
```

---

### **Step 4.2: Replace Connection String**

Update the connection string in DatabaseHelper.cs:

```csharp
// OLD (In-Memory)
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=;";

// NEW (MySQL)
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=YOUR_PASSWORD_HERE;";
```

Replace `YOUR_PASSWORD_HERE` with your MySQL root password.

---

### **Step 4.3: Remove In-Memory Lists**

**DELETE these lines from DatabaseHelper.cs:**

```csharp
// REMOVE THESE (they're replaced by database tables)
private static List<Models.Student> _students = new List<Models.Student>();
private static List<Models.Teacher> _teachers = new List<Models.Teacher>();
private static List<Models.Attendance> _attendances = new List<Models.Attendance>();
private static List<Models.ClassFee> _fees = new List<Models.ClassFee>();
private static List<Models.ExamResult> _results = new List<Models.ExamResult>();
private static List<Models.Notification> _notifications = new List<Models.Notification>();
private static List<Models.Class> _classes = new List<Models.Class>();
```

---

### **Step 4.4: Replace Methods with SQL Implementations**

Replace each method category with MySQL implementations:

#### **STUDENT METHODS - Replace these:**

```csharp
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
			MessageBox.Show("Student updated successfully", "Success");
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
			MessageBox.Show("Student deleted successfully", "Success");
		}
	}
	catch (Exception ex)
	{
		MessageBox.Show("Error deleting student: " + ex.Message);
	}
}
#endregion
```

---

#### **TEACHER METHODS - Similar pattern:**

```csharp
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
```

---

## **PHASE 5: COMPLETE CONVERSION TEMPLATE**

I've created a complete MySQL conversion template file for you to use:

**See: `DATABASEHELPER_MYSQL_COMPLETE.cs`** (I'll create this next)

---

## **PHASE 6: TEST CONNECTION**

### **Step 6.1: Add Connection Test Method**

Add this to DatabaseHelper.cs:

```csharp
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
```

### **Step 6.2: Call Test on App Startup**

Add to `Program.cs`:

```csharp
[STAThread]
static void Main()
{
	ApplicationConfiguration.Initialize();

	// Test database connection before starting app
	if (DatabaseHelper.TestDatabaseConnection())
	{
		Application.Run(new Form1());
	}
	else
	{
		MessageBox.Show("Cannot connect to database. Please check your connection settings.");
	}
}
```

---

## **PHASE 7: BUILD & TEST**

### **Step 7.1: Build Project**
- Press `Ctrl+Shift+B` or Build → Build Solution
- Fix any compilation errors

### **Step 7.2: Run Application**
- Press `F5` to run
- You should see connection test message
- Login and test features
- Data should persist after app closes

### **Step 7.3: Verify Data Persistence**
1. Add a student
2. Close application
3. Re-open application
4. Check if student still exists
5. ✅ If yes, migration successful!

---

## **PHASE 8: TROUBLESHOOTING**

### **Issue: "Unable to connect to any of the specified MySQL hosts"**
```
Solution:
1. Check MySQL service is running: Services → MySQL80
2. Verify connection string is correct
3. Check firewall isn't blocking port 3306
```

### **Issue: "Access denied for user 'root'@'localhost'"**
```
Solution:
1. Verify password is correct in connection string
2. Check user 'root' exists in MySQL
3. MySQL command to reset password:
   mysql -u root -p
   ALTER USER 'root'@'localhost' IDENTIFIED BY 'newpassword';
   FLUSH PRIVILEGES;
```

### **Issue: "Unknown database 'EduNex'"**
```
Solution:
1. Check database was created: SHOW DATABASES;
2. If not exists, run: CREATE DATABASE EduNex;
3. Verify database name in connection string matches
```

### **Issue: "No data showing in DataGridView"**
```
Solution:
1. Check database tables have data
2. Verify SQL query in GetAll method
3. Add error handling to see actual error message
```

---

## **MIGRATION CHECKLIST**

```
Phase 1: MySQL Setup
  ☐ MySQL Server installed
  ☐ MySQL Service running
  ☐ EduNex database created

Phase 2: Database Schema
  ☐ All 7 tables created
  ☐ Foreign keys configured
  ☐ Sample teacher inserted

Phase 3: NuGet Package
  ☐ MySql.Data installed
  ☐ No package conflicts

Phase 4: Code Update
  ☐ Using MySql.Data added
  ☐ Connection string updated
  ☐ In-memory Lists removed
  ☐ All methods converted to SQL

Phase 5: Testing
  ☐ Project builds successfully
  ☐ Connection test passes
  ☐ Can login with sample account
  ☐ Can add/view/update/delete data
  ☐ Data persists after app restart
```

---

## **NEXT STEPS**

1. ✅ Follow phases 1-7 above
2. ✅ Use the complete conversion template
3. ✅ Test thoroughly
4. ✅ Backup your database regularly
5. ✅ Consider implementing password hashing

**Your EduNex is now MySQL-powered! 🎉**
