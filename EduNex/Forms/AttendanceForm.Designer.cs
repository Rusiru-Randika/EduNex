using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class AttendanceForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Label lblDate;
        private DateTimePicker dtpAttendanceDate;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblRemarks;
        private TextBox txtRemarks;
        private Button btnAddAttendance;
        private Button btnViewByDate;
        private Button btnViewByStudent;
        private Button btnDelete;
        private Button btnRefresh;
        private DataGridView dgvAttendance;

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

            // Student ID
            lblStudentId = new Label();
            lblStudentId.Text = "Student ID:";
            lblStudentId.Location = new Point(20, 20);
            lblStudentId.AutoSize = true;

            txtStudentId = new TextBox();
            txtStudentId.Location = new Point(120, 20);
            txtStudentId.Width = 100;

            // Date
            lblDate = new Label();
            lblDate.Text = "Date:";
            lblDate.Location = new Point(250, 20);
            lblDate.AutoSize = true;

            dtpAttendanceDate = new DateTimePicker();
            dtpAttendanceDate.Location = new Point(350, 20);
            dtpAttendanceDate.Width = 150;

            // Status
            lblStatus = new Label();
            lblStatus.Text = "Status:";
            lblStatus.Location = new Point(20, 60);
            lblStatus.AutoSize = true;

            cmbStatus = new ComboBox();
            cmbStatus.Location = new Point(120, 60);
            cmbStatus.Width = 150;
            cmbStatus.Items.AddRange(new string[] { "Present", "Absent", "Late", "Leave" });

            // Remarks
            lblRemarks = new Label();
            lblRemarks.Text = "Remarks:";
            lblRemarks.Location = new Point(300, 60);
            lblRemarks.AutoSize = true;

            txtRemarks = new TextBox();
            txtRemarks.Location = new Point(400, 60);
            txtRemarks.Width = 200;

            // Buttons
            btnAddAttendance = new Button();
            btnAddAttendance.Text = "Add Attendance";
            btnAddAttendance.Location = new Point(20, 110);
            btnAddAttendance.Width = 120;
            btnAddAttendance.Click += btnAddAttendance_Click;

            btnViewByDate = new Button();
            btnViewByDate.Text = "View by Date";
            btnViewByDate.Location = new Point(150, 110);
            btnViewByDate.Width = 120;
            btnViewByDate.Click += btnViewByDate_Click;

            btnViewByStudent = new Button();
            btnViewByStudent.Text = "View by Student";
            btnViewByStudent.Location = new Point(280, 110);
            btnViewByStudent.Width = 120;
            btnViewByStudent.Click += btnViewByStudent_Click;

            btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Location = new Point(410, 110);
            btnDelete.Width = 100;
            btnDelete.Click += btnDelete_Click;

            btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(520, 110);
            btnRefresh.Width = 100;
            btnRefresh.Click += btnRefresh_Click;

            // DataGridView
            dgvAttendance = new DataGridView();
            dgvAttendance.Location = new Point(20, 160);
            dgvAttendance.Width = 800;
            dgvAttendance.Height = 350;
            dgvAttendance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Form Settings
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblStudentId);
            Controls.Add(txtStudentId);
            Controls.Add(lblDate);
            Controls.Add(dtpAttendanceDate);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(lblRemarks);
            Controls.Add(txtRemarks);
            Controls.Add(btnAddAttendance);
            Controls.Add(btnViewByDate);
            Controls.Add(btnViewByStudent);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(dgvAttendance);
            Load += AttendanceForm_Load;
        }
    }
}
