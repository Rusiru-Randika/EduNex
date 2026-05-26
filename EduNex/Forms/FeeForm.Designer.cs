using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class FeeForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Label lblAmount;
        private TextBox txtAmount;
        private Label lblDueDate;
        private DateTimePicker dtpDueDate;
        private Label lblPaidDate;
        private DateTimePicker dtpPaidDate;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblDescription;
        private TextBox txtDescription;
        private Button btnAddFee;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnViewPending;
        private Button btnViewPaid;
        private Button btnViewOverdue;
        private Button btnRefresh;
        private DataGridView dgvFees;

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

            // Amount
            lblAmount = new Label { Text = "Amount:", Location = new Point(280, 20), AutoSize = true };
            txtAmount = new TextBox { Location = new Point(360, 20), Width = 120 };

            // Due Date
            lblDueDate = new Label { Text = "Due Date:", Location = new Point(520, 20), AutoSize = true };
            dtpDueDate = new DateTimePicker { Location = new Point(620, 20), Width = 150 };

            // Paid Date
            lblPaidDate = new Label { Text = "Paid Date:", Location = new Point(20, 60), AutoSize = true };
            dtpPaidDate = new DateTimePicker { Location = new Point(120, 60), Width = 150 };

            // Status
            lblStatus = new Label { Text = "Status:", Location = new Point(300, 60), AutoSize = true };
            cmbStatus = new ComboBox { Location = new Point(380, 60), Width = 150 };
            cmbStatus.Items.AddRange(new string[] { "Pending", "Paid", "Overdue" });

            // Description
            lblDescription = new Label { Text = "Description:", Location = new Point(570, 60), AutoSize = true };
            txtDescription = new TextBox { Location = new Point(680, 60), Width = 150 };

            // Buttons
            btnAddFee = new Button { Text = "Add Fee", Location = new Point(20, 110), Width = 100, Height = 40 };
            btnAddFee.Click += btnAddFee_Click;

            btnUpdate = new Button { Text = "Update", Location = new Point(130, 110), Width = 100, Height = 40 };
            btnUpdate.Click += btnUpdate_Click;

            btnDelete = new Button { Text = "Delete", Location = new Point(240, 110), Width = 100, Height = 40 };
            btnDelete.Click += btnDelete_Click;

            btnViewPending = new Button { Text = "Pending", Location = new Point(350, 110), Width = 90, Height = 40 };
            btnViewPending.Click += btnViewPending_Click;

            btnViewPaid = new Button { Text = "Paid", Location = new Point(450, 110), Width = 90, Height = 40 };
            btnViewPaid.Click += btnViewPaid_Click;

            btnViewOverdue = new Button { Text = "Overdue", Location = new Point(550, 110), Width = 90, Height = 40 };
            btnViewOverdue.Click += btnViewOverdue_Click;

            btnRefresh = new Button { Text = "Refresh", Location = new Point(650, 110), Width = 90, Height = 40 };
            btnRefresh.Click += btnRefresh_Click;

            // DataGridView
            dgvFees = new DataGridView { Location = new Point(20, 170), Width = 940, Height = 350 };
            dgvFees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvFees.SelectionChanged += dgvFees_SelectionChanged;

            // Form Settings
            ClientSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblStudentId);
            Controls.Add(txtStudentId);
            Controls.Add(lblAmount);
            Controls.Add(txtAmount);
            Controls.Add(lblDueDate);
            Controls.Add(dtpDueDate);
            Controls.Add(lblPaidDate);
            Controls.Add(dtpPaidDate);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(btnAddFee);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnViewPending);
            Controls.Add(btnViewPaid);
            Controls.Add(btnViewOverdue);
            Controls.Add(btnRefresh);
            Controls.Add(dgvFees);
            Load += FeeForm_Load;
        }
    }
}
