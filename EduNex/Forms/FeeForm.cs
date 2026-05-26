using System;
using System.Windows.Forms;

namespace EduNex
{
    public partial class FeeForm : Form
    {
        private int _teacherId = 0;

        public FeeForm(int teacherId)
        {
            InitializeComponent();
            _teacherId = teacherId;
        }

        private void FeeForm_Load(object sender, EventArgs e)
        {
            this.Text = "Fee Management";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
            LoadFeeData();
        }

        private void LoadFeeData()
        {
            var fees = DatabaseHelper.GetAllFees();
            dgvFees.DataSource = fees;
        }

        private void btnAddFee_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtStudentId.Text, out int studentId))
            {
                MessageBox.Show("Please enter a valid Student ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = DatabaseHelper.GetStudentById(studentId);
            if (student == null)
            {
                MessageBox.Show("Student not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var fee = new Models.ClassFee
            {
                StudentID = studentId,
                StudentName = student.Name,
                Amount = amount,
                DueDate = dtpDueDate.Value,
                PaidDate = cmbStatus.SelectedItem?.ToString() == "Paid" ? dtpPaidDate.Value : DateTime.MinValue,
                Status = cmbStatus.SelectedItem?.ToString() ?? "Pending",
                Description = txtDescription.Text,
                TeacherID = _teacherId
            };

            DatabaseHelper.AddFee(fee);
            MessageBox.Show("Fee record added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadFeeData();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvFees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a fee record to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvFees.SelectedRows[0].Cells[0].Value.ToString(), out int feeId))
            {
                var fee = DatabaseHelper.GetAllFees().Find(f => f.FeeID == feeId);
                if (fee != null && decimal.TryParse(txtAmount.Text, out decimal amount))
                {
                    fee.Status = cmbStatus.SelectedItem?.ToString() ?? "Pending";
                    fee.PaidDate = cmbStatus.SelectedItem?.ToString() == "Paid" ? dtpPaidDate.Value : DateTime.MinValue;
                    DatabaseHelper.UpdateFee(fee);
                    MessageBox.Show("Fee record updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFeeData();
                    ClearFields();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a fee record to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(dgvFees.SelectedRows[0].Cells[0].Value.ToString(), out int feeId))
            {
                DatabaseHelper.DeleteFee(feeId);
                MessageBox.Show("Fee record deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFeeData();
            }
        }

        private void btnViewPending_Click(object sender, EventArgs e)
        {
            var pendingFees = DatabaseHelper.GetFeesByStatus("Pending");
            dgvFees.DataSource = pendingFees;
        }

        private void btnViewPaid_Click(object sender, EventArgs e)
        {
            var paidFees = DatabaseHelper.GetFeesByStatus("Paid");
            dgvFees.DataSource = paidFees;
        }

        private void btnViewOverdue_Click(object sender, EventArgs e)
        {
            var overdueFees = DatabaseHelper.GetFeesByStatus("Overdue");
            dgvFees.DataSource = overdueFees;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadFeeData();
        }

        private void ClearFields()
        {
            txtStudentId.Clear();
            txtAmount.Clear();
            txtDescription.Clear();
            cmbStatus.SelectedIndex = -1;
            dtpDueDate.Value = DateTime.Now;
        }

        private void dgvFees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFees.SelectedRows.Count > 0)
            {
                var row = dgvFees.SelectedRows[0];
                txtStudentId.Text = row.Cells[1].Value?.ToString() ?? "";
                txtAmount.Text = row.Cells[3].Value?.ToString() ?? "0";
                txtDescription.Text = row.Cells[7].Value?.ToString() ?? "";
                cmbStatus.SelectedItem = row.Cells[6].Value?.ToString() ?? "Pending";
            }
        }
    }
}
