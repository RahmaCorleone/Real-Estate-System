using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlX.XDevAPI.Relational;
using Oracle.ManagedDataAccess.Client;

namespace real_state_app1.form
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Close();
        }
       

        // check if the username or password are empty
        public bool checkFields()
        {
            label10.Visible = false;
            label11.Visible = false;

            if (username.Text.Trim().Equals("")&& password.Text.Trim().Equals(""))
            {
                label10.Visible = true;
                label11.Visible = true;
                return false;
            }
            else if (username.Text.Trim().Equals(""))
             {
                label10.Visible = true;
                return false;
            }
            else if (password.Text.Trim().Equals(""))
            {
                label11.Visible = true;
                return false;
            }
            else
            {
                return true;
             }
            // Add a return statement for the case where both conditions are false
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

       private void button2_Click(object sender, EventArgs e)
        {if (checkFields())
            {
                string enteredUsername = username.Text.Trim();
                string enteredPassword = password.Text.Trim();

                // Establish connection string
                string connectionString = "Data Source=localhost;User Id=hr;Password=1234;";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    // Create a command with the query
                    string selectQuery = "SELECT * FROM user_Data WHERE user_name = :username AND user_password = :password";
                    using (OracleCommand command = new OracleCommand(selectQuery, connection))
                    {
                        // Add parameters
                        command.Parameters.Add(":username", OracleDbType.Varchar2).Value = enteredUsername;
                        command.Parameters.Add(":password", OracleDbType.Varchar2).Value = enteredPassword;

                        try
                        {
                            // Open the connection
                            connection.Open();

                            // Execute the query
                            using (OracleDataReader reader = command.ExecuteReader())
                            {
                                // Check if any rows were returned
                                if (reader.HasRows)
                                {
                                    // Login successful
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    // Login failed
                                    labelError.Visible = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        public void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_MouseEnter(object sender, EventArgs e)
        {
            labelError.Visible=false;
        }

        private void username_MouseEnter(object sender, EventArgs e)
        {
            labelError.Visible = false;
        }
    }

}
