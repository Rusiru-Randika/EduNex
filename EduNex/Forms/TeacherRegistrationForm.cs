using System;
using System.Windows.Forms;

namespace EduNex
{
    public partial class TeacherRegistrationForm : Form
    {
        public TeacherRegistrationForm()
        {
            InitializeComponent();
        }

        private void TeacherRegistrationForm_Load(object sender, EventArgs e)
        {
            this.Text = "Teacher Registration";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(600, 550);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || 
                string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please fill all required fields", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if email already exists
            var existingTeacher = DatabaseHelper.GetTeacherByEmail(txtEmail.Text);
            if (existingTeacher != null)
            {
                MessageBox.Show("Email already registered", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var teacher = new Models.Teacher
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Subject = cmbSubject.SelectedItem?.ToString() ?? "Not Assigned",
                Class = cmbClass.SelectedItem?.ToString() ?? "Not Assigned",
                JoiningDate = DateTime.Now,
                IsActive = true
            };

            DatabaseHelper.AddTeacher(teacher);
            MessageBox.Show("Teacher registered successfully!\n\nEmail: " + txtEmail.Text + "\nYou can now login with these credentials.", 
                "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearFields();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            txtPhoneNumber.Clear();
            cmbSubject.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
