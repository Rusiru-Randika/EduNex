namespace EduNex
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnClear;
        private Button btnRegister;
        private Label lblTitle;

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

            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "EduNex - Smart Class Management System";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(150, 30);

            // Email Label
            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(200, 100);
            lblEmail.AutoSize = true;

            // Email TextBox
            txtEmail = new TextBox();
            txtEmail.Location = new Point(200, 125);
            txtEmail.Width = 250;
            txtEmail.Height = 30;

            // Password Label
            lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(200, 170);
            lblPassword.AutoSize = true;

            // Password TextBox
            txtPassword = new TextBox();
            txtPassword.Location = new Point(200, 195);
            txtPassword.Width = 250;
            txtPassword.Height = 30;
            txtPassword.PasswordChar = '*';

            // Login Button
            btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Location = new Point(200, 260);
            btnLogin.Width = 110;
            btnLogin.Height = 40;
            btnLogin.Click += btnLogin_Click;

            // Clear Button
            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Location = new Point(340, 260);
            btnClear.Width = 110;
            btnClear.Height = 40;
            btnClear.Click += btnClear_Click;

            // Register Button
            btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new Point(200, 320);
            btnRegister.Width = 250;
            btnRegister.Height = 40;
            btnRegister.Click += btnRegister_Click;

            // Form Settings
            ClientSize = new Size(650, 450);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Add(lblTitle);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnClear);
            Controls.Add(btnRegister);
            Load += Form1_Load;
        }
    }
}

