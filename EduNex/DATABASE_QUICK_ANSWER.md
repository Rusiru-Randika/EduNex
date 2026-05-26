# EduNex Database - Quick Answer

## 📊 **WHAT DATABASE DOES THIS PROJECT USE?**

### **Current Status: ⏳ IN-MEMORY STORAGE**

```csharp
// From DatabaseHelper.cs (Line 11)
private static string _connectionString = 
	"Server=localhost;Database=EduNex;User Id=root;Password=;";

// Data is stored in C# Lists (NOT PERSISTENT)
private static List<Models.Student> _students = new List<Models.Student>();
private static List<Models.Teacher> _teachers = new List<Models.Teacher>();
private static List<Models.Attendance> _attendances = new List<Models.Attendance>();
private static List<Models.ClassFee> _fees = new List<Models.ClassFee>();
private static List<Models.ExamResult> _results = new List<Models.ExamResult>();
private static List<Models.Notification> _notifications = new List<Models.Notification>();
private static List<Models.Class> _classes = new List<Models.Class>();
```

---

## 🎯 **KEY POINTS**

| Aspect | Current | Planned |
|--------|---------|---------|
| **Type** | In-Memory Lists | MySQL Database |
| **Persistence** | ❌ NO (Lost on app close) | ✅ YES (Permanent) |
| **Multi-user** | ❌ NO (Single app) | ✅ YES (Network access) |
| **Data Loss** | ⚠️ YES (When app closes) | ✅ Safe |
| **Scalability** | Limited | Unlimited |

---

## 🗄️ **CONFIGURED FOR MYSQL**

The application is **already configured to use MySQL**, but it's currently using in-memory storage as a fallback:

```
Server:   localhost
Database: EduNex
User:     root
Password: (empty - needs setup)
```

---

## 📈 **7 ENTITIES READY FOR DATABASE**

1. **Teachers** - 6 fields
2. **Students** - 14 fields
3. **Attendance** - 6 fields
4. **ClassFees** - 8 fields
5. **ExamResults** - 9 fields
6. **Notifications** - 8 fields
7. **Classes** - 10 fields

---

## ✅ **CURRENT SITUATION**

```
✅ Application runs perfectly with in-memory data
✅ All features work (CRUD operations)
✅ MySQL connection string is configured
✅ Ready for database migration anytime
❌ Data does NOT persist when app closes
```

---

## 🚀 **TO ENABLE MYSQL PERSISTENCE**

1. **Install MySQL Server** (free from mysql.com)
2. **Create Database:**
   ```sql
   CREATE DATABASE EduNex;
   ```
3. **Install NuGet Package:**
   ```
   Install-Package MySql.Data
   ```
4. **Update DatabaseHelper.cs** - Replace Lists with SQL queries
5. **Update Connection String** with your credentials

---

## 💡 **BEST PRACTICE**

```
NOW (Development):        Use in-memory (fast, no setup needed)
LATER (Production):       Switch to MySQL (persistent, multi-user)
```

---

## 📝 **SUMMARY**

**Question:** What database does this project use?

**Answer:** 
- **Currently:** In-Memory C# Lists (temporary)
- **Planned:** MySQL Database (configured)
- **Status:** Ready for production database migration

The application is fully functional but data is cleared when you close the app. To make it persistent, follow the MySQL integration steps in `DATABASE_DOCUMENTATION.md`.

---

**See `DATABASE_DOCUMENTATION.md` for complete SQL schema and migration guide!**
