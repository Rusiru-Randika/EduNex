namespace EduNex
{
    public partial class Form1 : Form
    {
        private int _loggedInTeacherId = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "EduNex - Smart Class Management System - Login";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initialize sample teacher data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Add sample teacher for testing
            var teacher = new Models.Teacher
            {
                TeacherID = 1,
                Name = "Mr. John Doe",
                Email = "john@example.com",
                Password = "password123",
                PhoneNumber = "1234567890",
                Subject = "Mathematics",
                Class = "10-A",
                JoiningDate = DateTime.Now.AddYears(-5),
                IsActive = true
            };
            DatabaseHelper.AddTeacher(teacher);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter email and password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var teacher = DatabaseHelper.GetTeacherByEmail(email);
            if (teacher != null && teacher.Password == password && teacher.IsActive)
            {
                _loggedInTeacherId = teacher.TeacherID;
                // Open main dashboard
                MainForm mainForm = new MainForm(_loggedInTeacherId, teacher.Name);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid email or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
            txtEmail.Focus();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            TeacherRegistrationForm registrationForm = new TeacherRegistrationForm();
            registrationForm.ShowDialog();
        }
    }
}

