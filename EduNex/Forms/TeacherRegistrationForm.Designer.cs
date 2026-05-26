using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class TeacherRegistrationForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblName;
        private TextBox txtName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblPhoneNumber;
        private TextBox txtPhoneNumber;
        private Label lblSubject;
        private ComboBox cmbSubject;
        private Label lblClass;
        private ComboBox cmbClass;
        private Button btnRegister;
        private Button btnClear;
        private Button btnCancel;

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

            // Name
            lblName = new Label { Text = "Full Name *:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(120, 20), Width = 200 };

            // Email
            lblEmail = new Label { Text = "Email *:", Location = new Point(20, 60), AutoSize = true };
            txtEmail = new TextBox { Location = new Point(120, 60), Width = 200 };

            // Password
            lblPassword = new Label { Text = "Password *:", Location = new Point(20, 100), AutoSize = true };
            txtPassword = new TextBox { Location = new Point(120, 100), Width = 200, PasswordChar = '*' };

            // Confirm Password
            lblConfirmPassword = new Label { Text = "Confirm Password *:", Location = new Point(20, 140), AutoSize = true };
            txtConfirmPassword = new TextBox { Location = new Point(120, 140), Width = 200, PasswordChar = '*' };

            // Phone Number
            lblPhoneNumber = new Label { Text = "Phone Number *:", Location = new Point(20, 180), AutoSize = true };
            txtPhoneNumber = new TextBox { Location = new Point(120, 180), Width = 200 };

            // Subject
            lblSubject = new Label { Text = "Subject:", Location = new Point(20, 220), AutoSize = true };
            cmbSubject = new ComboBox { Location = new Point(120, 220), Width = 200 };
            cmbSubject.Items.AddRange(new string[] { "Mathematics", "Science", "English", "History", "Geography", "Computer Science", "Physical Education" });

            // Class
            lblClass = new Label { Text = "Class:", Location = new Point(20, 260), AutoSize = true };
            cmbClass = new ComboBox { Location = new Point(120, 260), Width = 200 };
            cmbClass.Items.AddRange(new string[] { "6-A", "7-A", "8-A", "9-A", "10-A", "10-B", "11-A", "12-A" });

            // Buttons
            btnRegister = new Button { Text = "Register", Location = new Point(50, 320), Width = 100, Height = 40 };
            btnRegister.Click += btnRegister_Click;

            btnClear = new Button { Text = "Clear", Location = new Point(160, 320), Width = 100, Height = 40 };
            btnClear.Click += btnClear_Click;

            btnCancel = new Button { Text = "Cancel", Location = new Point(270, 320), Width = 100, Height = 40 };
            btnCancel.Click += btnCancel_Click;

            // Form Settings
            ClientSize = new Size(600, 450);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblSubject);
            Controls.Add(cmbSubject);
            Controls.Add(lblClass);
            Controls.Add(cmbClass);
            Controls.Add(btnRegister);
            Controls.Add(btnClear);
            Controls.Add(btnCancel);
            Load += TeacherRegistrationForm_Load;
        }
    }
}
