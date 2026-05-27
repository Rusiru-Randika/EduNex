# EduNex - Smart Class Management System

> A comprehensive Windows Forms application for managing school classes, students, teachers, attendance, fees, exams, and notifications.

![.NET](https://img.shields.io/badge/.NET-10.0-blue)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-blue)
![Database](https://img.shields.io/badge/Database-MySQL-orange)

## 📋 Quick Start

### Prerequisites
- Windows 10+
- .NET 10.0+
- MySQL 8.0+ or XAMPP
- Visual Studio 2022+

### Installation (5 Minutes)

1. **Clone Repository**
   ```bash
   git clone https://github.com/Rusiru-Randika/EduNex.git
   cd EduNex
   ```

2. **Start MySQL**
   ```powershell
   # If using XAMPP
   & "C:\xampp\mysql_start.bat"

   # Or start MySQL daemon
   & "C:\xampp\mysql\bin\mysqld.exe" --port=3306
   ```

3. **Create Database**
   ```powershell
   & "C:\xampp\mysql\bin\mysql.exe" -u root < "EduNex\schema.sql"
   ```

4. **Open in Visual Studio**
   - File → Open → `EduNex.slnx`

5. **Run Application**
   - Press **F5** to start
   - Login: `john@example.com` / `password123`

---

## ✨ Features

### 📚 Student Management
- Add, update, delete students
- Track enrollment and personal details
- Store parent contact information
- View student profiles

### 👨‍🏫 Teacher Management
- Teacher registration and authentication
- Subject and class assignment
- Profile management

### 📅 Attendance Tracking
- Mark daily attendance
- Generate attendance reports
- Filter by date and class

### 💰 Fee Management
- Track monthly fees
- Record payments
- View fee reports
- Payment status tracking

### 📊 Exam Results
- Record exam scores
- Auto-calculate percentages
- Grade assignment (A/B/C/D)
- Generate exam reports

### 📋 Class Management
- Create and manage classes
- Assign class teachers
- Track schedules

### 🔔 Notifications
- Send parent notifications
- Track notification status
- Communication history

### 📈 Reports & Analytics
- Student performance reports
- Attendance analytics
- Fee collection summary
- Exam statistics

---

## 🗄️ Database Schema

### 7 Tables
- **Teachers** - Teacher accounts and profiles
- **Students** - Student information
- **Attendance** - Attendance records
- **ClassFees** - Fee tracking
- **ExamResults** - Exam scores and grades
- **Notifications** - Parent communications
- **Classes** - Class information

All tables include timestamps and are optimized with proper indexes and foreign keys.

---

## 🔧 Configuration

### Database Connection String

Edit `DatabaseHelper.cs` (line 13):

```csharp
// For XAMPP (default - no password)
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=;";

// For MySQL Server (with password)
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=your_password;";
```

---

## 📁 Project Structure

```
EduNex/
├── Models/                    # Data models (7 classes)
├── Forms/                     # UI Forms (10+ forms)
├── DatabaseHelper.cs          # Data access layer
├── Program.cs                 # Entry point
├── schema.sql                 # Database schema
└── Documentation/
	├── MYSQL_MIGRATION_GUIDE.md
	├── MANUAL_SETUP_INSTRUCTIONS.md
	└── SETUP_COMPLETE.md
```

---

## 🚀 Usage

### Login
1. Enter teacher email
2. Enter password
3. Click Login

### Add Student
1. Go to Students section
2. Click Add Student
3. Fill details (Name, Roll Number, Class, etc.)
4. Save

### Mark Attendance
1. Go to Attendance
2. Select date and class
3. Mark attendance (Present/Absent/Leave)
4. Save

### Record Fees
1. Go to Fees
2. Select student
3. Enter amount and due date
4. Mark as paid
5. Save

### Record Exams
1. Go to Exams
2. Enter exam details
3. Record marks
4. Auto-calculates: Percentage & Grade
5. Save

### View Reports
1. Go to Reports
2. Select report type
3. Apply filters
4. View report

---

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| Connection failed | Check MySQL is running, verify password in DatabaseHelper.cs |
| Unknown database | Run schema.sql file to create database |
| MySQL not running | `& "C:\xampp\mysql\bin\mysqld.exe" --port=3306` |
| Duplicate email error | Register with different email or clear database |
| Build errors | Run `Build → Clean Solution` then `Build → Rebuild Solution` |

---

## 📚 Documentation

- **[MySQL Migration Guide](MYSQL_MIGRATION_GUIDE.md)** - Complete setup instructions
- **[Manual Setup](MANUAL_SETUP_INSTRUCTIONS.md)** - Step-by-step guide
- **[Setup Status](SETUP_COMPLETE.md)** - Verification checklist

---

## 🔐 Sample Account

**Email:** john@example.com  
**Password:** password123

Or register a new teacher account.

---

## 📦 Technology Stack

- **.NET Framework:** 10.0
- **UI:** Windows Forms
- **Database:** MySQL 8.0 / MariaDB 10.4+
- **ORM:** MySql.Data connector
- **IDE:** Visual Studio 2022+

---

## 🤝 Contributing

1. Fork repository
2. Create feature branch: `git checkout -b feature/YourFeature`
3. Commit changes: `git commit -m 'Add feature'`
4. Push: `git push origin feature/YourFeature`
5. Create Pull Request

---

## 📝 License

MIT License - Free to use and modify

---

## 👤 Author

**Rusiru Randika**  
GitHub: [@Rusiru-Randika](https://github.com/Rusiru-Randika)  
Repository: [EduNex](https://github.com/Rusiru-Randika/EduNex)

---

## 📞 Support

- 🐛 **Issues:** [GitHub Issues](https://github.com/Rusiru-Randika/EduNex/issues)
- 💬 **Discussions:** [GitHub Discussions](https://github.com/Rusiru-Randika/EduNex/discussions)

---

## ✅ Status

**Production Ready** ✓

Last Updated: May 27, 2026
