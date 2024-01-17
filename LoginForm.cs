using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NASHE;Initial Catalog=ProductsDb;Integrated Security=True;TrustServerCertificate=True;");





        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validate credentials and perform authentication
            if (AuthenticateUser(username, password))
            {
                // Successful login
                Form1 productForm = new Form1();
                productForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=NASHE;Initial Catalog=ProductsDb;Integrated Security=True;TrustServerCertificate=True;"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT PasswordHash FROM Users WHERE Username = @Username", connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedHash = reader["PasswordHash"].ToString();

                        if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                        {
                            return true; // Authentication successful
                        }
                        else
                        {
                            MessageBox.Show("Password does not match");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username not found");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return false; // Authentication failed
        }

    }
}



