using System;
using System.Windows.Forms;

namespace EduNex
{
    public partial class ExamForm : Form
    {
        private int _teacherId = 0;

        public ExamForm(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            this.Text = "Exam Results Management";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 650);
            LoadExamData();
        }

        private void LoadExamData()
        {
            var results = DatabaseHelper.GetAllExamResults();
            dgvResults.DataSource = results;
        }

        private void btnAddResult_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtMarksObtained.Text, out decimal marks) || !decimal.TryParse(txtTotalMarks.Text, out decimal total))
            {
                MessageBox.Show("Please enter valid marks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = DatabaseHelper.GetStudentById(studentId);
            if (student == null)
            {
                MessageBox.Show("Student not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal percentage = (marks / total) * 100;
            string grade = CalculateGrade(percentage);

            var result = new Models.ExamResult
            {
                StudentID = studentId,
                StudentName = student.Name,
                ExamName = txtExamName.Text,
                Subject = txtSubject.Text,
                MarksObtained = marks,
                TotalMarks = total,
                Percentage = percentage,
                Grade = grade,
                ExamDate = dtpExamDate.Value,
                TeacherID = _teacherId
            };

            DatabaseHelper.AddExamResult(result);
            MessageBox.Show("Exam result added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadExamData();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a result to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvResults.SelectedRows[0].Cells[0].Value.ToString(), out int resultId))
            {
                var result = DatabaseHelper.GetAllExamResults().Find(r => r.ResultID == resultId);
                if (result != null && decimal.TryParse(txtMarksObtained.Text, out decimal marks) && decimal.TryParse(txtTotalMarks.Text, out decimal total))
                {
                    result.MarksObtained = marks;
                    result.TotalMarks = total;
                    result.Percentage = (marks / total) * 100;
                    result.Grade = CalculateGrade(result.Percentage);
                    DatabaseHelper.UpdateExamResult(result);
                    MessageBox.Show("Exam result updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadExamData();
                    ClearFields();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a result to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvResults.SelectedRows[0].Cells[0].Value.ToString(), out int resultId))
            {
                DatabaseHelper.DeleteExamResult(resultId);
                MessageBox.Show("Exam result deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadExamData();
            }
        }

        private void btnViewByStudent_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var results = DatabaseHelper.GetResultsByStudent(studentId);
            dgvResults.DataSource = results;
        }

        private void btnViewByExam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExamName.Text))
            {
                MessageBox.Show("Please enter exam name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var results = DatabaseHelper.GetResultsByExam(txtExamName.Text);
            dgvResults.DataSource = results;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadExamData();
        }

        private string CalculateGrade(decimal percentage)
        {
            if (percentage >= 90) return "A+";
            if (percentage >= 80) return "A";
            if (percentage >= 70) return "B";
            if (percentage >= 60) return "C";
            if (percentage >= 50) return "D";
            return "F";
        }

        private void ClearFields()
        {
            txtStudentId.Clear();
            txtExamName.Clear();
            txtSubject.Clear();
            txtMarksObtained.Clear();
            txtTotalMarks.Clear();
            dtpExamDate.Value = DateTime.Now;
        }

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count > 0)
            {
                var row = dgvResults.SelectedRows[0];
                txtStudentId.Text = row.Cells[1].Value?.ToString() ?? "";
                txtExamName.Text = row.Cells[3].Value?.ToString() ?? "";
                txtSubject.Text = row.Cells[4].Value?.ToString() ?? "";
                txtMarksObtained.Text = row.Cells[5].Value?.ToString() ?? "0";
                txtTotalMarks.Text = row.Cells[6].Value?.ToString() ?? "0";
            }
        }
    }
}
