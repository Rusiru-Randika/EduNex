# EduNex Database Configuration & Documentation

## 🗄️ **CURRENT DATABASE SETUP**

### **Current Implementation: In-Memory Storage**

```csharp
// From DatabaseHelper.cs
private static List<Models.Student> _students = new List<Models.Student>();
private static List<Models.Teacher> _teachers = new List<Models.Teacher>();
private static List<Models.Attendance> _attendances = new List<Models.Attendance>();
private static List<Models.ClassFee> _fees = new List<Models.ClassFee>();
private static List<Models.ExamResult> _results = new List<Models.ExamResult>();
private static List<Models.Notification> _notifications = new List<Models.Notification>();
private static List<Models.Class> _classes = new List<Models.Class>();
```

**Status:** ⏳ **In-Memory (Temporary)**
- Data stored in RAM using `List<T>` collections
- Data is cleared when application closes
- No persistent storage
- Perfect for testing and development

---

## 🎯 **PLANNED DATABASE: MySQL**

### **Connection String Configuration**

```csharp
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=;";
```

**Current Connection Details:**
- **Server:** localhost
- **Database Name:** EduNex
- **User ID:** root
- **Password:** (empty - needs to be configured)
- **Port:** 3306 (default MySQL port)

---

## 📊 **EXPECTED DATABASE SCHEMA**

### **Tables Required for MySQL Integration**

#### **1. Teachers Table**
```sql
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
```

#### **2. Students Table**
```sql
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
```

#### **3. Attendance Table**
```sql
CREATE TABLE Attendance (
	AttendanceID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	AttendanceDate DATE NOT NULL,
	Status VARCHAR(20), -- Present, Absent, Late, Leave
	Remarks VARCHAR(255),
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
);
```

#### **4. ClassFees Table**
```sql
CREATE TABLE ClassFees (
	FeeID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	Amount DECIMAL(10, 2),
	DueDate DATE,
	PaidDate DATE,
	Status VARCHAR(20), -- Pending, Paid, Overdue
	Description VARCHAR(255),
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
);
```

#### **5. ExamResults Table**
```sql
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
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
);
```

#### **6. Notifications Table**
```sql
CREATE TABLE Notifications (
	NotificationID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT NOT NULL,
	StudentName VARCHAR(100),
	ParentEmail VARCHAR(100),
	ParentPhoneNumber VARCHAR(15),
	Message TEXT,
	NotificationType VARCHAR(50), -- Attendance, Fee, Exam, General
	SentDate DATETIME,
	IsSent BOOLEAN DEFAULT FALSE,
	TeacherID INT,
	CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID)
);
```

#### **7. Classes Table**
```sql
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
	FOREIGN KEY (ClassTeacherID) REFERENCES Teachers(TeacherID)
);
```

---

## 🔄 **HOW TO MIGRATE FROM IN-MEMORY TO MySQL**

### **Step 1: Install MySQL**
- Download: https://dev.mysql.com/downloads/mysql/
- Install MySQL Server
- Note: User ID (root) and Password

### **Step 2: Create Database**
```sql
CREATE DATABASE EduNex;
USE EduNex;
```

### **Step 3: Run SQL Scripts**
Execute all CREATE TABLE statements above

### **Step 4: Update Connection String**
```csharp
// In DatabaseHelper.cs
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=yourPassword;";
```

### **Step 5: Install MySQL NuGet Package**
```
Package Manager Console:
Install-Package MySql.Data
```

### **Step 6: Update DatabaseHelper Methods**
Replace in-memory List operations with SQL queries

**Example:**
```csharp
// Current (In-Memory)
public static List<Models.Student> GetAllStudents()
{
	return _students.ToList();
}

// Convert to (MySQL)
public static List<Models.Student> GetAllStudents()
{
	List<Models.Student> students = new List<Models.Student>();
	using (MySqlConnection conn = new MySqlConnection(_connectionString))
	{
		conn.Open();
		string query = "SELECT * FROM Students WHERE IsActive = TRUE";
		MySqlCommand cmd = new MySqlCommand(query, conn);
		MySqlDataReader reader = cmd.ExecuteReader();

		while (reader.Read())
		{
			students.Add(new Models.Student
			{
				StudentID = (int)reader["StudentID"],
				Name = reader["Name"].ToString(),
				// ... map other fields
			});
		}
	}
	return students;
}
```

---

## 📋 **DATA ENTITIES & RELATIONSHIPS**

### **Entity Diagram**

```
┌──────────────┐
│  Teachers    │◄─────┐
└──────────────┘      │
	   ▲              │
	   │              │ (One-to-Many)
	   │              │
┌──────┴──────┐  ┌────┴──────┐
│ Students    │  │ Classes   │
└─────────────┘  └───────────┘
	   │ │
	   │ └─── Attendance
	   │ └─── ExamResults
	   │ └─── ClassFees
	   └───── Notifications
```

### **Relationships**

1. **Teachers → Classes** (1:1)
   - One teacher manages one class (currently)

2. **Teachers → Attendance** (1:N)
   - One teacher marks attendance for many records

3. **Teachers → ClassFees** (1:N)
   - One teacher manages many fee records

4. **Teachers → ExamResults** (1:N)
   - One teacher records many exam results

5. **Teachers → Notifications** (1:N)
   - One teacher sends many notifications

6. **Students → Attendance** (1:N)
   - One student has many attendance records

7. **Students → ClassFees** (1:N)
   - One student has many fee records

8. **Students → ExamResults** (1:N)
   - One student has many exam results

9. **Students → Notifications** (1:N)
   - One student receives many notifications

---

## 💾 **DATABASE HELPER CONFIGURATION**

### **Connection String Methods**

```csharp
// Get current connection string
public static string GetConnectionString()
{
	return _connectionString;
}

// Update connection string
public static void SetConnectionString(string connString)
{
	_connectionString = connString;
}
```

### **Current Methods (In-Memory)**

| Entity | Methods | Count |
|--------|---------|-------|
| Student | Add, GetAll, GetById, Update, Delete | 5 |
| Teacher | Add, GetAll, GetById, GetByEmail, Update, Delete | 6 |
| Attendance | Add, GetByDate, GetByStudent, GetAll, Update, Delete | 6 |
| ClassFee | Add, GetAll, GetByStudent, GetByStatus, Update, Delete | 6 |
| ExamResult | Add, GetAll, GetByStudent, GetByExam, Update, Delete | 6 |
| Notification | Add, GetAll, GetUnsent, MarkSent, Delete | 5 |
| Class | Add, GetAll, GetById, GetByName, Update, Delete | 6 |

---

## 🔐 **DATABASE SECURITY RECOMMENDATIONS**

### **For Production:**
1. **Use Strong Passwords** - Don't leave root password empty
2. **Create Database User** - Don't use root for app
   ```sql
   CREATE USER 'eduapp'@'localhost' IDENTIFIED BY 'SecurePassword123';
   GRANT ALL PRIVILEGES ON EduNex.* TO 'eduapp'@'localhost';
   FLUSH PRIVILEGES;
   ```

3. **Hash Passwords** - Implement password hashing for teachers
4. **Enable SSL** - Secure database connection
5. **Backup Regularly** - Automate database backups

### **Updated Connection String Example:**
```csharp
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=eduapp;Password=SecurePassword123;SSL Mode=Required;";
```

---

## 🧪 **TESTING CONNECTION**

### **Test MySQL Connection:**
```csharp
public static void TestConnection()
{
	try
	{
		using (MySqlConnection conn = new MySqlConnection(_connectionString))
		{
			conn.Open();
			MessageBox.Show("Database connection successful!", "Success");
			conn.Close();
		}
	}
	catch (Exception ex)
	{
		MessageBox.Show("Connection failed: " + ex.Message, "Error");
	}
}
```

---

## 📈 **DATA PERSISTENCE COMPARISON**

| Feature | Current (In-Memory) | After MySQL Integration |
|---------|-------------------|------------------------|
| **Persistence** | ❌ Lost on close | ✅ Permanent |
| **Multiple Users** | ❌ Single app only | ✅ Multi-user support |
| **Scalability** | ❌ Limited | ✅ Unlimited records |
| **Backup** | ❌ Manual | ✅ Automated |
| **Queries** | Simple LINQ | Complex SQL |
| **Speed** | Very fast (RAM) | Fast (optimized) |
| **Cost** | ✅ Free | ✅ Free (MySQL) |

---

## 🚀 **NEXT STEPS FOR DATABASE INTEGRATION**

1. ✅ Install MySQL Community Server
2. ✅ Create EduNex database
3. ✅ Run SQL schema scripts
4. ✅ Install MySql.Data NuGet package
5. ✅ Update DatabaseHelper.cs with SQL methods
6. ✅ Update connection string with credentials
7. ✅ Test all CRUD operations
8. ✅ Deploy to production

---

## 📞 **MYSQL RESOURCES**

- **MySQL Download:** https://dev.mysql.com/downloads/
- **MySQL Documentation:** https://dev.mysql.com/doc/
- **MySQL Workbench:** https://dev.mysql.com/products/workbench/
- **C# MySQL Driver:** https://www.nuget.org/packages/MySql.Data/

---

## ✅ **CURRENT STATUS**

```
Database Type: In-Memory (C# Lists)
Status: ✅ Fully Functional (Temporary)
Persistence: ❌ Not persistent
Ready for Integration: ✅ YES
Connection String: ✅ Configured
Next Phase: MySQL Integration
```

**Your application is ready to integrate MySQL whenever you're ready!** 🎯
