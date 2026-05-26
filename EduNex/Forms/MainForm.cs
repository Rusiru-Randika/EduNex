using System;
using System.Windows.Forms;

namespace EduNex
{
    public partial class MainForm : Form
    {
        private int _teacherId = 0;
        private string _teacherName = "";

        public MainForm(int teacherId, string teacherName)
        {
            InitializeComponent();
            _teacherId = teacherId;
            _teacherName = teacherName;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = $"EduNex - Main Dashboard - Welcome {_teacherName}";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnStudentManagement_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm(_teacherId);
            studentForm.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            AttendanceForm attendanceForm = new AttendanceForm(_teacherId);
            attendanceForm.Show();
        }

        private void btnFeeManagement_Click(object sender, EventArgs e)
        {
            FeeForm feeForm = new FeeForm(_teacherId);
            feeForm.Show();
        }

        private void btnExamResults_Click(object sender, EventArgs e)
        {
            ExamForm examForm = new ExamForm(_teacherId);
            examForm.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm(_teacherId);
            reportForm.Show();
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            NotificationForm notificationForm = new NotificationForm(_teacherId);
            notificationForm.Show();
        }

        private void btnClassManagement_Click(object sender, EventArgs e)
        {
            ClassManagementForm classForm = new ClassManagementForm(_teacherId);
            classForm.Show();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
