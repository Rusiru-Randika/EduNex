using System.Windows.Forms;

namespace EduNex
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Test database connection before starting app
            if (DatabaseHelper.TestDatabaseConnection())
            {
                // Connection successful, launch the login form
                Application.Run(new Form1());
            }
            else
            {
                // Connection failed, show error and exit
                MessageBox.Show("Cannot connect to database. Please check your connection settings in DatabaseHelper.cs\n\nUpdate the connection string with your MySQL password.", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
