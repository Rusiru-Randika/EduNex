using System;
using System.Windows.Forms;

namespace EduNex
{
    public partial class NotificationForm : Form
    {
        private int _teacherId = 0;

        public NotificationForm(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            this.Text = "Notifications";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 600);
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            var notifications = DatabaseHelper.GetAllNotifications();
            dgvNotifications.DataSource = notifications;
        }

        private void btnCreateNotification_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = DatabaseHelper.GetStudentById(studentId);
            if (student == null)
            {
                MessageBox.Show("Student not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var notification = new Models.Notification
            {
                StudentID = studentId,
                StudentName = student.Name,
                ParentEmail = student.ParentEmail,
                ParentPhoneNumber = student.ParentPhoneNumber,
                Message = txtMessage.Text,
                NotificationType = cmbNotificationType.SelectedItem?.ToString() ?? "General",
                SentDate = DateTime.Now,
                IsSent = false,
                TeacherID = _teacherId
            };

            DatabaseHelper.AddNotification(notification);
            MessageBox.Show("Notification created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadNotifications();
            ClearFields();
        }

        private void btnSendNotification_Click(object sender, EventArgs e)
        {
            var unsentNotifications = DatabaseHelper.GetUnsentNotifications();
            if (unsentNotifications.Count == 0)
            {
                MessageBox.Show("No unsent notifications", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var notification in unsentNotifications)
            {
                DatabaseHelper.MarkNotificationAsSent(notification.NotificationID);
            }

            MessageBox.Show($"{unsentNotifications.Count} notifications sent successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadNotifications();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvNotifications.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a notification to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvNotifications.SelectedRows[0].Cells[0].Value.ToString(), out int notificationId))
            {
                DatabaseHelper.DeleteNotification(notificationId);
                MessageBox.Show("Notification deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNotifications();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadNotifications();
        }

        private void ClearFields()
        {
            txtStudentId.Clear();
            txtMessage.Clear();
            cmbNotificationType.SelectedIndex = -1;
        }
    }
}
