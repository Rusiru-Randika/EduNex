using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class NotificationForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStudentId;
        private TextBox txtStudentId;
        private Label lblMessage;
        private TextBox txtMessage;
        private Label lblNotificationType;
        private ComboBox cmbNotificationType;
        private Button btnCreateNotification;
        private Button btnSendNotification;
        private Button btnDelete;
        private Button btnRefresh;
        private DataGridView dgvNotifications;

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

            // Message
            lblMessage = new Label { Text = "Message:", Location = new Point(270, 20), AutoSize = true };
            txtMessage = new TextBox { Location = new Point(350, 20), Width = 350, Height = 60, Multiline = true };

            // Notification Type
            lblNotificationType = new Label { Text = "Type:", Location = new Point(20, 100), AutoSize = true };
            cmbNotificationType = new ComboBox { Location = new Point(120, 100), Width = 200 };
            cmbNotificationType.Items.AddRange(new string[] { "Attendance", "Fee", "Exam", "General" });

            // Buttons
            btnCreateNotification = new Button { Text = "Create Notification", Location = new Point(20, 150), Width = 150, Height = 40 };
            btnCreateNotification.Click += btnCreateNotification_Click;

            btnSendNotification = new Button { Text = "Send All Notifications", Location = new Point(180, 150), Width = 160, Height = 40 };
            btnSendNotification.Click += btnSendNotification_Click;

            btnDelete = new Button { Text = "Delete", Location = new Point(350, 150), Width = 100, Height = 40 };
            btnDelete.Click += btnDelete_Click;

            btnRefresh = new Button { Text = "Refresh", Location = new Point(460, 150), Width = 100, Height = 40 };
            btnRefresh.Click += btnRefresh_Click;

            // DataGridView
            dgvNotifications = new DataGridView { Location = new Point(20, 210), Width = 850, Height = 310 };
            dgvNotifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Form Settings
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblStudentId);
            Controls.Add(txtStudentId);
            Controls.Add(lblMessage);
            Controls.Add(txtMessage);
            Controls.Add(lblNotificationType);
            Controls.Add(cmbNotificationType);
            Controls.Add(btnCreateNotification);
            Controls.Add(btnSendNotification);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(dgvNotifications);
            Load += NotificationForm_Load;
        }
    }
}
