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

SELECT 'Database and tables created successfully!' AS Status;
