# EduNex MySQL - Manual Setup Instructions

Since MySQL CLI tools are not available in the PATH, please follow these manual setup steps:

## Option 1: Using MySQL Workbench (Recommended - GUI)

### Step 1: Open MySQL Workbench
1. Launch MySQL Workbench
2. Connect to your local MySQL Server (localhost:3306 with root user)

### Step 2: Create Database
1. Click **File** → **Open SQL Script**
2. Or paste the following SQL into a new query tab

### Step 3: Execute SQL Script
Copy and paste **ALL** the following SQL into MySQL Workbench query editor, then click **Execute All**:

```sql
-- Create Database
CREATE DATABASE IF NOT EXISTS EduNex;
USE EduNex;

-- Teachers Table
CREATE TABLE IF NOT EXISTS Teachers (
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

-- Students Table
CREATE TABLE IF NOT EXISTS Students (
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

-- Attendance Table
CREATE TABLE IF NOT EXISTS Attendance (
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

-- ClassFees Table
CREATE TABLE IF NOT EXISTS ClassFees (
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

-- ExamResults Table
CREATE TABLE IF NOT EXISTS ExamResults (
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

-- Notifications Table
CREATE TABLE IF NOT EXISTS Notifications (
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

-- Classes Table
CREATE TABLE IF NOT EXISTS Classes (
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

-- Insert sample teacher data
INSERT INTO Teachers (Name, Email, Password, PhoneNumber, Subject, Class, JoiningDate, IsActive)
VALUES ('Mr. John Doe', 'john@example.com', 'password123', '1234567890', 'Mathematics', '10-A', NOW(), TRUE);

-- Verify tables were created
SELECT 'Database setup complete!' AS Status;
SHOW TABLES;
```

## Option 2: Using MySQL Command Line

1. Open PowerShell as Administrator
2. Find your MySQL installation directory (typically `C:\Program Files\MySQL\MySQL Server 8.0\bin`)
3. Run:
   ```powershell
   "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -p
   ```
4. Enter your MySQL root password
5. Paste the SQL script from Option 1 above

## Option 3: Using File Import

1. Save the SQL script from Option 1 to a file: `schema.sql`
2. Open PowerShell in the directory where you saved the file
3. Run:
   ```powershell
   "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe" -u root -p EduNex < schema.sql
   ```

---

## After Database Setup

### Step 1: Update Connection String

Edit **EduNex\DatabaseHelper.cs** and update:

```csharp
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=YOUR_PASSWORD_HERE;";
```

Replace `YOUR_PASSWORD_HERE` with your actual MySQL root password.

### Step 2: Build the Project

1. Open the solution in Visual Studio
2. Press **Ctrl+Shift+B** to build
3. Fix any errors if they occur

### Step 3: Run and Test

1. Press **F5** to run the application
2. You should see a "✓ Database connection successful!" message
3. Login with sample account:
   - **Email:** john@example.com
   - **Password:** password123

### Step 4: Verify Data Persistence

1. Add a new student or record
2. Close the application
3. Re-open the application
4. Check if the data still exists
5. ✅ If yes, your MySQL migration is successful!

---

## Troubleshooting

### "Cannot connect to MySQL"
- Check MySQL Server is running: Windows Services → MySQL80
- Verify password is correct in connection string
- Check firewall isn't blocking port 3306

### "Unknown database 'EduNex'"
- Verify you created the database with `CREATE DATABASE EduNex;`
- Check spelling matches exactly

### "No tables in database"
- Run the full SQL script from Option 1
- Verify all statements executed without errors

### "Access denied for user 'root'"
- Verify MySQL password in connection string matches your setup password
- Reset password in MySQL Workbench if needed

---

**Questions?** Refer to MYSQL_MIGRATION_GUIDE.md for more details.
