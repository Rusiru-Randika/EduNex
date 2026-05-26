using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EduNex
{
    public partial class ReportForm : Form
    {
        private int _teacherId = 0;

        public ReportForm(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            this.Text = "Reports";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 650);
            LoadAttendanceReport();
        }

        private void btnAttendanceReport_Click(object sender, EventArgs e)
        {
            LoadAttendanceReport();
        }

        private void LoadAttendanceReport()
        {
            var attendances = DatabaseHelper.GetAllAttendances();

            var report = attendances.GroupBy(a => a.StudentName)
                .Select(g => new
                {
                    StudentName = g.Key,
                    Present = g.Count(a => a.Status == "Present"),
                    Absent = g.Count(a => a.Status == "Absent"),
                    Late = g.Count(a => a.Status == "Late"),
                    Leave = g.Count(a => a.Status == "Leave"),
                    Total = g.Count(),
                    Percentage = (g.Count(a => a.Status == "Present") * 100m) / g.Count()
                }).ToList();

            dgvReport.DataSource = report;
            lblReportTitle.Text = "Attendance Report";
        }

        private void btnFeeReport_Click(object sender, EventArgs e)
        {
            LoadFeeReport();
        }

        private void LoadFeeReport()
        {
            var fees = DatabaseHelper.GetAllFees();

            var report = fees.GroupBy(f => f.StudentName)
                .Select(g => new
                {
                    StudentName = g.Key,
                    TotalFees = g.Sum(f => f.Amount),
                    Paid = g.Where(f => f.Status == "Paid").Sum(f => f.Amount),
                    Pending = g.Where(f => f.Status == "Pending").Sum(f => f.Amount),
                    Overdue = g.Where(f => f.Status == "Overdue").Sum(f => f.Amount)
                }).ToList();

            dgvReport.DataSource = report;
            lblReportTitle.Text = "Fee Report";
        }

        private void btnExamReport_Click(object sender, EventArgs e)
        {
            LoadExamReport();
        }

        private void LoadExamReport()
        {
            var results = DatabaseHelper.GetAllExamResults();

            var report = results.GroupBy(r => r.StudentName)
                .Select(g => new
                {
                    StudentName = g.Key,
                    AveragePercentage = Math.Round(g.Average(r => r.Percentage), 2),
                    HighestMarks = g.Max(r => r.MarksObtained),
                    LowestMarks = g.Min(r => r.MarksObtained),
                    ExamCount = g.Count(),
                    GradeCount = g.Count()
                }).ToList();

            dgvReport.DataSource = report;
            lblReportTitle.Text = "Exam Performance Report";
        }

        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            LoadSummaryReport();
        }

        private void LoadSummaryReport()
        {
            var students = DatabaseHelper.GetAllStudents();
            var attendances = DatabaseHelper.GetAllAttendances();
            var fees = DatabaseHelper.GetAllFees();
            var results = DatabaseHelper.GetAllExamResults();

            var report = students.Select(s => new
            {
                StudentName = s.Name,
                RollNumber = s.RollNumber,
                Class = s.Class,
                AttendanceCount = attendances.Count(a => a.StudentID == s.StudentID),
                PendingFees = fees.Where(f => f.StudentID == s.StudentID && f.Status == "Pending").Sum(f => f.Amount),
                ExamAverage = results.Where(r => r.StudentID == s.StudentID).Any() ? 
                    Math.Round(results.Where(r => r.StudentID == s.StudentID).Average(r => r.Percentage), 2) : 0
            }).ToList();

            dgvReport.DataSource = report;
            lblReportTitle.Text = "Summary Report";
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files|*.csv";
            saveFileDialog.Title = "Export Report to CSV";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        // Write headers
                        for (int i = 0; i < dgvReport.Columns.Count; i++)
                        {
                            writer.Write(dgvReport.Columns[i].HeaderText);
                            if (i < dgvReport.Columns.Count - 1)
                                writer.Write(",");
                        }
                        writer.WriteLine();

                        // Write data
                        foreach (DataGridViewRow row in dgvReport.Rows)
                        {
                            for (int i = 0; i < dgvReport.Columns.Count; i++)
                            {
                                writer.Write(row.Cells[i].Value);
                                if (i < dgvReport.Columns.Count - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();
                        }
                    }

                    MessageBox.Show("Report exported successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality - integrate with your printer settings", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
