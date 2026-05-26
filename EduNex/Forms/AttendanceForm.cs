using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EduNex
{
    public partial class AttendanceForm : Form
    {
        private int _teacherId = 0;

        public AttendanceForm(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            this.Text = "Attendance Tracking";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 600);
            LoadAttendanceData();
        }

        private void LoadAttendanceData()
        {
            var attendances = DatabaseHelper.GetAllAttendances();
            dgvAttendance.DataSource = attendances;
        }

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStudentId.Text) || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill all required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("Invalid Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = DatabaseHelper.GetStudentById(studentId);
            if (student == null)
            {
                MessageBox.Show("Student not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var attendance = new Models.Attendance
            {
                StudentID = studentId,
                StudentName = student.Name,
                AttendanceDate = dtpAttendanceDate.Value,
                Status = cmbStatus.SelectedItem.ToString(),
                Remarks = txtRemarks.Text,
                TeacherID = _teacherId
            };

            DatabaseHelper.AddAttendance(attendance);
            MessageBox.Show("Attendance recorded successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAttendanceData();
            ClearFields();
        }

        private void btnViewByDate_Click(object sender, EventArgs e)
        {
            var attendances = DatabaseHelper.GetAttendanceByDate(dtpAttendanceDate.Value);
            dgvAttendance.DataSource = attendances;
        }

        private void btnViewByStudent_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var attendances = DatabaseHelper.GetAttendanceByStudent(studentId);
            dgvAttendance.DataSource = attendances;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvAttendance.SelectedRows[0].Cells[0].Value.ToString(), out int attendanceId))
            {
                DatabaseHelper.DeleteAttendance(attendanceId);
                MessageBox.Show("Attendance record deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAttendanceData();
            }
        }

        private void ClearFields()
        {
            txtStudentId.Clear();
            cmbStatus.SelectedIndex = -1;
            txtRemarks.Clear();
            dtpAttendanceDate.Value = DateTime.Now;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAttendanceData();
        }
    }
}
