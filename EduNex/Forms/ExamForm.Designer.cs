using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class ExamForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Label lblExamName;
        private TextBox txtExamName;
        private Label lblSubject;
        private TextBox txtSubject;
        private Label lblMarksObtained;
        private TextBox txtMarksObtained;
        private Label lblTotalMarks;
        private TextBox txtTotalMarks;
        private Label lblExamDate;
        private DateTimePicker dtpExamDate;
        private Button btnAddResult;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnViewByStudent;
        private Button btnViewByExam;
        private Button btnRefresh;
        private DataGridView dgvResults;

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
            lblStudentId = new Label { Text = "Student ID:", Location = new Point(20, 20), AutoSize = true };
            txtStudentId = new TextBox { Location = new Point(120, 20), Width = 120 };

            // Exam Name
            lblExamName = new Label { Text = "Exam Name:", Location = new Point(280, 20), AutoSize = true };
            txtExamName = new TextBox { Location = new Point(380, 20), Width = 150 };

            // Subject
            lblSubject = new Label { Text = "Subject:", Location = new Point(570, 20), AutoSize = true };
            txtSubject = new TextBox { Location = new Point(650, 20), Width = 150 };

            // Marks Obtained
            lblMarksObtained = new Label { Text = "Marks Obtained:", Location = new Point(20, 60), AutoSize = true };
            txtMarksObtained = new TextBox { Location = new Point(150, 60), Width = 120 };

            // Total Marks
            lblTotalMarks = new Label { Text = "Total Marks:", Location = new Point(300, 60), AutoSize = true };
            txtTotalMarks = new TextBox { Location = new Point(410, 60), Width = 120 };

            // Exam Date
            lblExamDate = new Label { Text = "Exam Date:", Location = new Point(570, 60), AutoSize = true };
            dtpExamDate = new DateTimePicker { Location = new Point(680, 60), Width = 150 };

            // Buttons
            btnAddResult = new Button { Text = "Add Result", Location = new Point(20, 110), Width = 100, Height = 40 };
            btnAddResult.Click += btnAddResult_Click;

            btnUpdate = new Button { Text = "Update", Location = new Point(130, 110), Width = 100, Height = 40 };
            btnUpdate.Click += btnUpdate_Click;

            btnDelete = new Button { Text = "Delete", Location = new Point(240, 110), Width = 100, Height = 40 };
            btnDelete.Click += btnDelete_Click;

            btnViewByStudent = new Button { Text = "By Student", Location = new Point(350, 110), Width = 100, Height = 40 };
            btnViewByStudent.Click += btnViewByStudent_Click;

            btnViewByExam = new Button { Text = "By Exam", Location = new Point(460, 110), Width = 100, Height = 40 };
            btnViewByExam.Click += btnViewByExam_Click;

            btnRefresh = new Button { Text = "Refresh", Location = new Point(570, 110), Width = 100, Height = 40 };
            btnRefresh.Click += btnRefresh_Click;

            // DataGridView
            dgvResults = new DataGridView { Location = new Point(20, 170), Width = 940, Height = 400 };
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResults.SelectionChanged += dgvResults_SelectionChanged;

            // Form Settings
            ClientSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblStudentId);
            Controls.Add(txtStudentId);
            Controls.Add(lblExamName);
            Controls.Add(txtExamName);
            Controls.Add(lblSubject);
            Controls.Add(txtSubject);
            Controls.Add(lblMarksObtained);
            Controls.Add(txtMarksObtained);
            Controls.Add(lblTotalMarks);
            Controls.Add(txtTotalMarks);
            Controls.Add(lblExamDate);
            Controls.Add(dtpExamDate);
            Controls.Add(btnAddResult);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnViewByStudent);
            Controls.Add(btnViewByExam);
            Controls.Add(btnRefresh);
            Controls.Add(dgvResults);
            Load += ExamForm_Load;
        }
    }
}
