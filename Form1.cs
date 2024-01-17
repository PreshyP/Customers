using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
namespace ProductsApp
{
    public partial class Form1 : Form
    {


        // Declare the SqlConnection at the class level


        SqlConnection con = new SqlConnection("Data Source=NASHE;Initial Catalog=ProductsDb;Integrated Security=True;");


        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Your event handling code here
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Using the class-level SqlConnection
                using (SqlConnection con = new SqlConnection("Data Source=NASHE;Initial Catalog=ProductsDb;Integrated Security=True;")
) ;
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO dbo.ItemsProduc (ProductID, ItemName, Design, Color, InsetDate, UpdateDate) VALUES (@ProductID, @ItemName, @Design, @Color, GETDATE(), GETDATE())", con);

                    command.Parameters.AddWithValue("@ProductID", int.Parse(textBox1.Text));
                    command.Parameters.AddWithValue("@ItemName", textBox2.Text);
                    command.Parameters.AddWithValue("@Design", textBox3.Text);
                    command.Parameters.AddWithValue("@Color", comboBox1.Text);
                    command.Parameters.AddWithValue("@InsetDate", DateTime.Now); // Assuming your column is of type DateTime
                    command.Parameters.AddWithValue("@UpdateDate", DateTime.Now);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully inserted.");
                    con.Close();

                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid integer for Product ID.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                BindData();
            }
        }

        // Rest of your code...
    

    private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE ItemsProduc SET ItemName = '" + textBox2.Text + "', Design = '" + textBox3.Text + "', Color = '" + comboBox1.Text + "', Updatedate = '" + DateTime.Parse(dateTimePicker1.Text) + "' WHERE ProductID = '" + int.Parse(textBox1.Text) + "'", con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Updated.");
            BindData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("Are you sure to delete?", "Delete Records", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM ItemsProduc WHERE ProductID = '" + int.Parse(textBox1.Text) + "'", con);
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted.");
                    BindData();
                }
                else
                {
                    MessageBox.Show("Put Product ID");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ItemsProduc where ProductID= '" + int.Parse(textBox1.Text) + " '", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BindData()
        {
            try
            {
                con.Open(); // Open the connection

                SqlCommand command = new SqlCommand("select * from ItemsProduc", con);
                SqlDataAdapter sd = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection in the finally block to ensure it's closed even if an exception occurs
                con.Close();
            }
        }
    }
}