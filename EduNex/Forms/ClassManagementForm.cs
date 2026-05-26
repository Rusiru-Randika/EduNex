using System;
using System.Windows.Forms;

namespace EduNex
{
    public partial class ClassManagementForm : Form
    {
        private int _teacherId = 0;

        public ClassManagementForm(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
        }

        private void ClassManagementForm_Load(object sender, EventArgs e)
        {
            this.Text = "Class Management";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 650);
            LoadClassData();
            PopulateTeacherDropdown();
        }

        private void LoadClassData()
        {
            var classes = DatabaseHelper.GetAllClasses();
            dgvClasses.DataSource = classes;
        }

        private void PopulateTeacherDropdown()
        {
            var teachers = DatabaseHelper.GetAllTeachers();
            cmbClassTeacher.DataSource = teachers;
            cmbClassTeacher.DisplayMember = "Name";
            cmbClassTeacher.ValueMember = "TeacherID";
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClassName.Text) || string.IsNullOrEmpty(txtSection.Text))
            {
                MessageBox.Show("Please fill required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if class already exists
            var existingClass = DatabaseHelper.GetClassByName(txtClassName.Text);
            if (existingClass != null)
            {
                MessageBox.Show("Class already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var classObj = new Models.Class
            {
                ClassName = txtClassName.Text,
                Section = txtSection.Text,
                ClassTeacherID = (int)cmbClassTeacher.SelectedValue,
                ClassTeacherName = cmbClassTeacher.SelectedText,
                TotalStudents = int.TryParse(txtTotalStudents.Text, out int total) ? total : 0,
                Room = txtRoom.Text,
                Schedule = txtSchedule.Text,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            DatabaseHelper.AddClass(classObj);
            MessageBox.Show("Class added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadClassData();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a class to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvClasses.SelectedRows[0].Cells[0].Value.ToString(), out int classId))
            {
                var classObj = DatabaseHelper.GetClassById(classId);
                if (classObj != null)
                {
                    classObj.ClassName = txtClassName.Text;
                    classObj.Section = txtSection.Text;
                    classObj.ClassTeacherID = (int)cmbClassTeacher.SelectedValue;
                    classObj.ClassTeacherName = cmbClassTeacher.SelectedText;
                    classObj.TotalStudents = int.TryParse(txtTotalStudents.Text, out int total) ? total : 0;
                    classObj.Room = txtRoom.Text;
                    classObj.Schedule = txtSchedule.Text;

                    DatabaseHelper.UpdateClass(classObj);
                    MessageBox.Show("Class updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClassData();
                    ClearFields();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a class to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvClasses.SelectedRows[0].Cells[0].Value.ToString(), out int classId))
            {
                DatabaseHelper.DeleteClass(classId);
                MessageBox.Show("Class deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadClassData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadClassData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvClasses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count > 0)
            {
                var row = dgvClasses.SelectedRows[0];
                txtClassName.Text = row.Cells[1].Value?.ToString() ?? "";
                txtSection.Text = row.Cells[2].Value?.ToString() ?? "";
                txtRoom.Text = row.Cells[6].Value?.ToString() ?? "";
                txtSchedule.Text = row.Cells[7].Value?.ToString() ?? "";
                txtTotalStudents.Text = row.Cells[5].Value?.ToString() ?? "0";
            }
        }

        private void ClearFields()
        {
            txtClassName.Clear();
            txtSection.Clear();
            txtRoom.Clear();
            txtSchedule.Clear();
            txtTotalStudents.Clear();
            cmbClassTeacher.SelectedIndex = -1;
        }
    }
}
