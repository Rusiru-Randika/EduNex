using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class ClassManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblClassName;
        private TextBox txtClassName;
        private Label lblSection;
        private TextBox txtSection;
        private Label lblClassTeacher;
        private ComboBox cmbClassTeacher;
        private Label lblRoom;
        private TextBox txtRoom;
        private Label lblSchedule;
        private TextBox txtSchedule;
        private Label lblTotalStudents;
        private TextBox txtTotalStudents;
        private Button btnAddClass;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnClear;
        private DataGridView dgvClasses;

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

            // Class Name
            lblClassName = new Label { Text = "Class Name:", Location = new Point(20, 20), AutoSize = true };
            txtClassName = new TextBox { Location = new Point(130, 20), Width = 150 };

            // Section
            lblSection = new Label { Text = "Section:", Location = new Point(310, 20), AutoSize = true };
            txtSection = new TextBox { Location = new Point(400, 20), Width = 100 };

            // Class Teacher
            lblClassTeacher = new Label { Text = "Class Teacher:", Location = new Point(530, 20), AutoSize = true };
            cmbClassTeacher = new ComboBox { Location = new Point(660, 20), Width = 200 };

            // Room
            lblRoom = new Label { Text = "Room:", Location = new Point(20, 60), AutoSize = true };
            txtRoom = new TextBox { Location = new Point(130, 60), Width = 150 };

            // Schedule
            lblSchedule = new Label { Text = "Schedule:", Location = new Point(310, 60), AutoSize = true };
            txtSchedule = new TextBox { Location = new Point(400, 60), Width = 460, Height = 40, Multiline = true };

            // Total Students
            lblTotalStudents = new Label { Text = "Total Students:", Location = new Point(20, 120), AutoSize = true };
            txtTotalStudents = new TextBox { Location = new Point(130, 120), Width = 150 };

            // Buttons
            btnAddClass = new Button { Text = "Add Class", Location = new Point(20, 170), Width = 100, Height = 40 };
            btnAddClass.Click += btnAddClass_Click;

            btnUpdate = new Button { Text = "Update", Location = new Point(130, 170), Width = 100, Height = 40 };
            btnUpdate.Click += btnUpdate_Click;

            btnDelete = new Button { Text = "Delete", Location = new Point(240, 170), Width = 100, Height = 40 };
            btnDelete.Click += btnDelete_Click;

            btnRefresh = new Button { Text = "Refresh", Location = new Point(350, 170), Width = 100, Height = 40 };
            btnRefresh.Click += btnRefresh_Click;

            btnClear = new Button { Text = "Clear", Location = new Point(460, 170), Width = 100, Height = 40 };
            btnClear.Click += btnClear_Click;

            // DataGridView
            dgvClasses = new DataGridView { Location = new Point(20, 230), Width = 940, Height = 350 };
            dgvClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvClasses.SelectionChanged += dgvClasses_SelectionChanged;

            // Form Settings
            ClientSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblClassName);
            Controls.Add(txtClassName);
            Controls.Add(lblSection);
            Controls.Add(txtSection);
            Controls.Add(lblClassTeacher);
            Controls.Add(cmbClassTeacher);
            Controls.Add(lblRoom);
            Controls.Add(txtRoom);
            Controls.Add(lblSchedule);
            Controls.Add(txtSchedule);
            Controls.Add(lblTotalStudents);
            Controls.Add(txtTotalStudents);
            Controls.Add(btnAddClass);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(btnClear);
            Controls.Add(dgvClasses);
            Load += ClassManagementForm_Load;
        }
    }
}
