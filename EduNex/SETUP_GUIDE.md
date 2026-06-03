# EduNex - MySQL Workbench Connection Setup

## ⚠️ IMPORTANT: MySQL Server Not Installed

**Error:** `MySQL80` service not found

You need to **install MySQL Server first** before proceeding.

---

## 🚀 Step 1: Install MySQL Server

### Option A: Download MySQL Installer (Recommended)

1. Go to: https://dev.mysql.com/downloads/mysql/
2. Download **MySQL Server 8.0** (Windows installer)
3. Run the installer (`mysql-installer-web-community-x.x.x.msi`)
4. **Installation Setup:**
   - Choose: **Developer Default** (includes MySQL Server + Workbench)
   - Click **Next**
5. **MySQL Server Configuration:**
   - Port: `3306` (default)
   - Config Type: **Development Machine**
   - MySQL 8.0 X Protocol Port: `33060`
   - Click **Next**
6. **Accounts and Roles:**
   - Username: `root`
   - Set a strong password (remember this!)
   - Click **Next**
7. **Windows Service:**
   - Service Name: `MySQL80`
   - Startup Type: **Automatic**
   - Click **Next**
8. Click **Execute** to install
9. Click **Finish**

### Option B: Use Chocolatey (If you have it installed)

```powershell
# Run as Administrator
choco install mysql-server
```

### Option C: Use Windows Package Manager

```powershell
# Run as Administrator
winget install MySQL.MySQL-Server
```

---

## ✅ Verify MySQL Installation

After installation, open PowerShell and verify:

```powershell
Get-Service "MySQL80" | Select-Object Status
```

**Expected output:**
```
Status
------
Running
```

If stopped, start it:
```powershell
Start-Service "MySQL80"
```

---

## 🔧 Step 2: Verify MySQL Workbench is Installed

1. Check if **MySQL Workbench** is installed (usually installed with MySQL Server)
2. If not, download from: https://dev.mysql.com/downloads/workbench/

---

## 🔧 Step 3: Open MySQL Workbench

1. Launch **MySQL Workbench**
2. Look for **"Local instance MySQL80"** or create a new connection:
   - **Connection Name:** `Local MySQL`
   - **Hostname:** `localhost`
   - **Port:** `3306`
   - **Username:** `root`
   - **Password:** *(Enter your MySQL root password from installation)*
3. Click **Test Connection** to verify
4. Click **OK**

---

## 📊 Step 4: Create Database in Workbench

1. Click on your connection to connect
2. Click **File** → **New Query Tab** (or press **Ctrl+T**)
3. Copy and paste this SQL:

```sql
CREATE DATABASE IF NOT EXISTS EduNex;
USE EduNex;

-- Create Teachers table
CREATE TABLE Teachers (
	TeacherID INT PRIMARY KEY AUTO_INCREMENT,
	Name VARCHAR(100) NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Password VARCHAR(100) NOT NULL,
	PhoneNumber VARCHAR(15),
	Subject VARCHAR(50),
	Class VARCHAR(20),
	JoiningDate DATE,
	IsActive BOOLEAN DEFAULT TRUE
);

-- Create Students table
CREATE TABLE Students (
	StudentID INT PRIMARY KEY AUTO_INCREMENT,
	Name VARCHAR(100) NOT NULL,
	RollNumber VARCHAR(20) UNIQUE,
	DateOfBirth DATE,
	Gender VARCHAR(10),
	Address VARCHAR(200),
	PhoneNumber VARCHAR(15),
	ParentName VARCHAR(100),
	ParentPhoneNumber VARCHAR(15),
	ParentEmail VARCHAR(100),
	EnrollmentDate DATE,
	Class VARCHAR(20),
	MonthlyFee DECIMAL(10, 2),
	IsActive BOOLEAN DEFAULT TRUE
);

-- Create Attendance table
CREATE TABLE Attendance (
	AttendanceID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT,
	StudentName VARCHAR(100),
	AttendanceDate DATETIME,
	Status VARCHAR(20),
	Remarks VARCHAR(200),
	TeacherID INT,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);

-- Create ClassFees table
CREATE TABLE ClassFees (
	FeeID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT,
	StudentName VARCHAR(100),
	Amount DECIMAL(10, 2),
	DueDate DATE,
	PaidDate DATE,
	Status VARCHAR(20),
	Description VARCHAR(200),
	TeacherID INT,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);

-- Create ExamResults table
CREATE TABLE ExamResults (
	ResultID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT,
	StudentName VARCHAR(100),
	ExamName VARCHAR(100),
	Subject VARCHAR(50),
	MarksObtained DECIMAL(5, 2),
	TotalMarks DECIMAL(5, 2),
	Percentage DECIMAL(5, 2),
	Grade VARCHAR(2),
	ExamDate DATE,
	TeacherID INT,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);

-- Create Notifications table
CREATE TABLE Notifications (
	NotificationID INT PRIMARY KEY AUTO_INCREMENT,
	StudentID INT,
	StudentName VARCHAR(100),
	ParentEmail VARCHAR(100),
	ParentPhoneNumber VARCHAR(15),
	Message TEXT,
	NotificationType VARCHAR(50),
	SentDate DATETIME,
	IsSent BOOLEAN DEFAULT FALSE,
	TeacherID INT,
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);

-- Create Classes table
CREATE TABLE Classes (
	ClassID INT PRIMARY KEY AUTO_INCREMENT,
	ClassName VARCHAR(50) NOT NULL,
	Section VARCHAR(10),
	ClassTeacherID INT,
	ClassTeacherName VARCHAR(100),
	TotalStudents INT,
	Room VARCHAR(20),
	Schedule VARCHAR(100),
	CreatedDate DATETIME,
	IsActive BOOLEAN DEFAULT TRUE,
	FOREIGN KEY (ClassTeacherID) REFERENCES Teachers(TeacherID)
);

-- Insert sample teacher
INSERT INTO Teachers (Name, Email, Password, PhoneNumber, Subject, Class, JoiningDate, IsActive)
VALUES ('Mr. John Doe', 'john@example.com', 'password123', '0711234567', 'Mathematics', '10-A', NOW(), TRUE);
```

4. Click the **Lightning Bolt** ⚡ icon to execute
5. You should see: ✅ **Query executed successfully**

---

## 🔐 Step 5: Update Connection String in Your App

Open **`DatabaseHelper.cs`** in Visual Studio and update **Line 14**:

**Find this:**
```csharp
private static string _connectionString = "Server=localhost;Database=EduNex;User Id=root;Password=YOUR_PASSWORD;";
```

**Replace with your actual password (from MySQL installation):**
```csharp
private static string _connectionString = "Server=localhost;Database=EduNex;User Id=root;Password=YourPassword123;";
```

> **Note:** Replace `YourPassword123` with the password you set during MySQL installation

---

## ✅ Step 6: Test Connection in Your App

1. In Visual Studio, press **F5** to run the app
2. You should see: ✅ **Database connection successful!**
3. If error → Double-check your password in `DatabaseHelper.cs`

---

## 📋 Verify in MySQL Workbench

1. In Workbench left panel, expand **`EduNex`** → **`Tables`**
2. You should see all **7 tables**:
   - ✓ Teachers
   - ✓ Students
   - ✓ Attendance
   - ✓ ClassFees
   - ✓ ExamResults
   - ✓ Notifications
   - ✓ Classes

3. View sample data:
```sql
SELECT * FROM EduNex.Teachers;
```

You should see **Mr. John Doe** ✓

---

## 🔑 Key Information

| Item | Value |
|------|-------|
| **Host** | localhost |
| **Port** | 3306 |
| **Database** | EduNex |
| **Username** | root |
| **Password** | *(Your choice)* |
| **Service** | MySQL80 |

---

## ❌ Troubleshooting

### "Connection refused"
- ✅ Start MySQL service: `Start-Service "MySQL80"`
- ✅ Verify it's running: `Get-Service "MySQL80"`

### "Access denied for user 'root'"
- ✅ Check password in `DatabaseHelper.cs`
- ✅ Verify password in Workbench connection

### "Unknown database 'EduNex'"
- ✅ Run the SQL creation script in Workbench
- ✅ Execute: `CREATE DATABASE EduNex;`

### "Cannot connect to MySQL server"
- ✅ MySQL Server must be installed
- ✅ MySQL Workbench must be installed
- ✅ MySQL Service (`MySQL80`) must be running

---

## 🎉 You're All Set!

Your EduNex app is now connected to MySQL via Workbench. You can:
- ✓ Manage data through the app
- ✓ View/edit data in MySQL Workbench
- ✓ Run SQL queries in Workbench
- ✓ Export/backup your database

**Happy coding!** 🚀
