using System;
using System.Windows.Forms;

namespace EduNex
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblReportTitle;
        private Button btnAttendanceReport;
        private Button btnFeeReport;
        private Button btnExamReport;
        private Button btnSummaryReport;
        private Button btnExportToCSV;
        private Button btnPrint;
        private DataGridView dgvReport;

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

            // Title
            lblReportTitle = new Label { Text = "Select Report Type", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };

            // Report Buttons
            btnAttendanceReport = new Button { Text = "Attendance Report", Location = new Point(20, 60), Width = 150, Height = 40 };
            btnAttendanceReport.Click += btnAttendanceReport_Click;

            btnFeeReport = new Button { Text = "Fee Report", Location = new Point(190, 60), Width = 150, Height = 40 };
            btnFeeReport.Click += btnFeeReport_Click;

            btnExamReport = new Button { Text = "Exam Report", Location = new Point(360, 60), Width = 150, Height = 40 };
            btnExamReport.Click += btnExamReport_Click;

            btnSummaryReport = new Button { Text = "Summary Report", Location = new Point(530, 60), Width = 150, Height = 40 };
            btnSummaryReport.Click += btnSummaryReport_Click;

            // Export & Print Buttons
            btnExportToCSV = new Button { Text = "Export to CSV", Location = new Point(20, 120), Width = 150, Height = 40 };
            btnExportToCSV.Click += btnExportToCSV_Click;

            btnPrint = new Button { Text = "Print", Location = new Point(190, 120), Width = 150, Height = 40 };
            btnPrint.Click += btnPrint_Click;

            // DataGridView
            dgvReport = new DataGridView { Location = new Point(20, 180), Width = 940, Height = 400 };
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Form Settings
            ClientSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblReportTitle);
            Controls.Add(btnAttendanceReport);
            Controls.Add(btnFeeReport);
            Controls.Add(btnExamReport);
            Controls.Add(btnSummaryReport);
            Controls.Add(btnExportToCSV);
            Controls.Add(btnPrint);
            Controls.Add(dgvReport);
            Load += ReportForm_Load;
        }
    }
}
