using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Hotel_Management_System
{
    public partial class LoginForm : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\suzan\Desktop\visual project\login.accdb;Persist Security Info=False;";

        public LoginForm()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            // Load data from the database - if needed for other purposes
        }

        private bool AuthenticateUser(string enteredUsername, string enteredPassword)
        {
            bool isValidUser = false;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Username, Password FROM tbl_admn WHERE Username = @Username AND Password = @Password";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", enteredUsername);
                        command.Parameters.AddWithValue("@Password", enteredPassword);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Valid username and password
                                isValidUser = true;
                            }
                        }
                    }
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return isValidUser;
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Orange;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button_Login_Click_1(object sender, EventArgs e)
        {
            if (TextBox_username.Text.Trim().Equals("") || TextBox_password.Text == "")
            {
                MessageBox.Show("Enter your username and Password", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string enteredUsername = TextBox_username.Text;
                string enteredPassword = TextBox_password.Text;

                if (AuthenticateUser(enteredUsername, enteredPassword))
                {
                    // Valid username and password
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Your username and/or password is incorrect", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
