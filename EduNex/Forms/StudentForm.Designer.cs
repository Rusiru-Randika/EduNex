using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class StudentForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblName;
        private TextBox txtName;
        private Label lblRollNumber;
        private TextBox txtRollNumber;
        private Label lblDateOfBirth;
        private DateTimePicker dtpDateOfBirth;
        private Label lblGender;
        private ComboBox cmbGender;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblPhoneNumber;
        private TextBox txtPhoneNumber;
        private Label lblParentName;
        private TextBox txtParentName;
        private Label lblParentPhone;
        private TextBox txtParentPhoneNumber;
        private Label lblParentEmail;
        private TextBox txtParentEmail;
        private Label lblClass;
        private ComboBox cmbClass;
        private Label lblMonthlyFee;
        private TextBox txtMonthlyFee;
        private Button btnAddStudent;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvStudents;

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
            lblName = new Label { Text = "Name:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(120, 20), Width = 200 };

            // Roll Number
            lblRollNumber = new Label { Text = "Roll Number:", Location = new Point(360, 20), AutoSize = true };
            txtRollNumber = new TextBox { Location = new Point(480, 20), Width = 150 };

            // Date of Birth
            lblDateOfBirth = new Label { Text = "DOB:", Location = new Point(670, 20), AutoSize = true };
            dtpDateOfBirth = new DateTimePicker { Location = new Point(720, 20), Width = 150 };

            // Gender
            lblGender = new Label { Text = "Gender:", Location = new Point(20, 60), AutoSize = true };
            cmbGender = new ComboBox { Location = new Point(120, 60), Width = 200 };
            cmbGender.Items.AddRange(new string[] { "Male", "Female", "Other" });

            // Address
            lblAddress = new Label { Text = "Address:", Location = new Point(360, 60), AutoSize = true };
            txtAddress = new TextBox { Location = new Point(480, 60), Width = 390, Height = 40, Multiline = true };

            // Phone Number
            lblPhoneNumber = new Label { Text = "Phone:", Location = new Point(20, 110), AutoSize = true };
            txtPhoneNumber = new TextBox { Location = new Point(120, 110), Width = 200 };

            // Parent Name
            lblParentName = new Label { Text = "Parent Name:", Location = new Point(360, 110), AutoSize = true };
            txtParentName = new TextBox { Location = new Point(480, 110), Width = 150 };

            // Parent Phone
            lblParentPhone = new Label { Text = "Parent Phone:", Location = new Point(670, 110), AutoSize = true };
            txtParentPhoneNumber = new TextBox { Location = new Point(800, 110), Width = 150 };

            // Parent Email
            lblParentEmail = new Label { Text = "Parent Email:", Location = new Point(20, 150), AutoSize = true };
            txtParentEmail = new TextBox { Location = new Point(120, 150), Width = 200 };

            // Class
            lblClass = new Label { Text = "Class:", Location = new Point(360, 150), AutoSize = true };
            cmbClass = new ComboBox { Location = new Point(480, 150), Width = 150 };
            cmbClass.Items.AddRange(new string[] { "6-A", "7-A", "8-A", "9-A", "10-A", "10-B", "11-A", "12-A" });

            // Monthly Fee
            lblMonthlyFee = new Label { Text = "Monthly Fee:", Location = new Point(670, 150), AutoSize = true };
            txtMonthlyFee = new TextBox { Location = new Point(800, 150), Width = 150 };

            // Buttons
            btnAddStudent = new Button { Text = "Add Student", Location = new Point(20, 200), Width = 120, Height = 40 };
            btnAddStudent.Click += btnAddStudent_Click;

            btnUpdate = new Button { Text = "Update", Location = new Point(150, 200), Width = 100, Height = 40 };
            btnUpdate.Click += btnUpdate_Click;

            btnDelete = new Button { Text = "Delete", Location = new Point(260, 200), Width = 100, Height = 40 };
            btnDelete.Click += btnDelete_Click;

            btnClear = new Button { Text = "Clear", Location = new Point(370, 200), Width = 100, Height = 40 };
            btnClear.Click += btnClear_Click;

            // DataGridView
            dgvStudents = new DataGridView { Location = new Point(20, 270), Width = 930, Height = 350 };
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;

            // Form Settings
            ClientSize = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblRollNumber);
            Controls.Add(txtRollNumber);
            Controls.Add(lblDateOfBirth);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(lblGender);
            Controls.Add(cmbGender);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblPhoneNumber);
            Controls.Add(txtPhoneNumber);
            Controls.Add(lblParentName);
            Controls.Add(txtParentName);
            Controls.Add(lblParentPhone);
            Controls.Add(txtParentPhoneNumber);
            Controls.Add(lblParentEmail);
            Controls.Add(txtParentEmail);
            Controls.Add(lblClass);
            Controls.Add(cmbClass);
            Controls.Add(lblMonthlyFee);
            Controls.Add(txtMonthlyFee);
            Controls.Add(btnAddStudent);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dgvStudents);
            Load += StudentForm_Load;
        }
    }
}
