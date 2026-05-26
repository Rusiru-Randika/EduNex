using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblWelcome;
        private Button btnStudentManagement;
        private Button btnAttendance;
        private Button btnFeeManagement;
        private Button btnExamResults;
        private Button btnReports;
        private Button btnNotifications;
        private Button btnClassManagement;
        private Button btnLogout;
        private Button btnExit;
        private Panel pnlMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // Welcome Label
            lblWelcome = new Label();
            lblWelcome.Text = "Welcome to EduNex - Smart Class Management System";
            lblWelcome.Font = new Font("Arial", 18, FontStyle.Bold);
            lblWelcome.Location = new Point(150, 20);
            lblWelcome.AutoSize = true;

            // Menu Panel
            pnlMenu = new Panel();
            pnlMenu.Location = new Point(50, 100);
            pnlMenu.Size = new Size(900, 450);

            // Student Management Button
            btnStudentManagement = new Button();
            btnStudentManagement.Text = "Student Management";
            btnStudentManagement.Location = new Point(50, 100);
            btnStudentManagement.Size = new Size(180, 60);
            btnStudentManagement.Click += btnStudentManagement_Click;

            // Attendance Button
            btnAttendance = new Button();
            btnAttendance.Text = "Attendance Tracking";
            btnAttendance.Location = new Point(280, 100);
            btnAttendance.Size = new Size(180, 60);
            btnAttendance.Click += btnAttendance_Click;

            // Fee Management Button
            btnFeeManagement = new Button();
            btnFeeManagement.Text = "Fee Management";
            btnFeeManagement.Location = new Point(510, 100);
            btnFeeManagement.Size = new Size(180, 60);
            btnFeeManagement.Click += btnFeeManagement_Click;

            // Exam Results Button
            btnExamResults = new Button();
            btnExamResults.Text = "Exam Results";
            btnExamResults.Location = new Point(740, 100);
            btnExamResults.Size = new Size(180, 60);
            btnExamResults.Click += btnExamResults_Click;

            // Reports Button
            btnReports = new Button();
            btnReports.Text = "Reports";
            btnReports.Location = new Point(50, 200);
            btnReports.Size = new Size(180, 60);
            btnReports.Click += btnReports_Click;

            // Notifications Button
            btnNotifications = new Button();
            btnNotifications.Text = "Notifications";
            btnNotifications.Location = new Point(280, 200);
            btnNotifications.Size = new Size(180, 60);
            btnNotifications.Click += btnNotifications_Click;

            // Class Management Button
            btnClassManagement = new Button();
            btnClassManagement.Text = "Class Management";
            btnClassManagement.Location = new Point(510, 200);
            btnClassManagement.Size = new Size(180, 60);
            btnClassManagement.Click += btnClassManagement_Click;

            // Logout Button
            btnLogout = new Button();
            btnLogout.Text = "Logout";
            btnLogout.Location = new Point(50, 300);
            btnLogout.Size = new Size(180, 60);
            btnLogout.Click += btnLogout_Click;

            // Exit Button
            btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.Location = new Point(280, 300);
            btnExit.Size = new Size(180, 60);
            btnExit.Click += btnExit_Click;

            // Add controls
            pnlMenu.Controls.Add(btnStudentManagement);
            pnlMenu.Controls.Add(btnAttendance);
            pnlMenu.Controls.Add(btnFeeManagement);
            pnlMenu.Controls.Add(btnExamResults);
            pnlMenu.Controls.Add(btnReports);
            pnlMenu.Controls.Add(btnNotifications);
            pnlMenu.Controls.Add(btnClassManagement);
            pnlMenu.Controls.Add(btnLogout);
            pnlMenu.Controls.Add(btnExit);

            // Form Settings
            ClientSize = new Size(1200, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblWelcome);
            Controls.Add(pnlMenu);
            Load += MainForm_Load;
        }
    }
}
