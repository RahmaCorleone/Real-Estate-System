using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using real_state_app1.classes;

namespace real_state_app1.form
{
    public partial class ClientForm : Form
    {
        private OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;");
        public ClientForm()
        {
            InitializeComponent();
        }
        public void LoadDatagridviewClients()
        {
            try
            {
                dataGridViewClient.DataSource = property_type.GetAllOwner("property_client");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void displayClientsCount()
        {
            string Clientcount = property_type.GetAllOwner("property_client").Rows.Count.ToString();
            labelclient.Text = Clientcount + "Client(s)";

        }
        private void ClientForm_Load(object sender, EventArgs e)
        {
            LoadDatagridviewClients();
            displayClientsCount();

        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string id = textBoxID.Text;
                string fname = textBoxFirstName.Text;
                string lname = textBoxLastName.Text;
                string phone = textBoxPhone.Text;
                string Email = textBoxEmail.Text;
                string Address = textBoxAddress.Text;



                if (!string.IsNullOrWhiteSpace(fname) && !string.IsNullOrWhiteSpace(lname) && !string.IsNullOrWhiteSpace(phone))
                {
                    string insertQuery = "INSERT INTO property_client (client_id,client_firstname, client_last_name,client_phone,client_email,client_address) VALUES (:id,:fname,:lname, :phone,:Email,:Address)";
                    using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        insertCommand.Parameters.Add(":fname", OracleDbType.Varchar2).Value = fname;
                        insertCommand.Parameters.Add(":lname", OracleDbType.Varchar2).Value = lname;
                        insertCommand.Parameters.Add(":phone", OracleDbType.Int32).Value = phone;
                        insertCommand.Parameters.Add(":Email", OracleDbType.Varchar2).Value = Email;
                        insertCommand.Parameters.Add(":Address", OracleDbType.Varchar2).Value = Address;




                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("client_Data inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatagridviewClients();
                        displayClientsCount();
                    }
                }
                else
                {
                    MessageBox.Show("first name, last name and phone cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string id = textBoxID.Text;
                string fname = textBoxFirstName.Text;
                string lname = textBoxLastName.Text;
                string phone = textBoxPhone.Text;
                string Email = textBoxEmail.Text;
                string Address = textBoxAddress.Text;

                if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(fname) && !string.IsNullOrWhiteSpace(lname) && !string.IsNullOrWhiteSpace(phone))
                {
                    string updateQuery = "UPDATE property_client SET client_firstname = :fname, client_last_name = :lname, client_phone = :phone, client_email = :Email, client_address = :Address WHERE client_id = :id";
                    using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.Add(":fname", OracleDbType.Varchar2).Value = fname;
                        updateCommand.Parameters.Add(":lname", OracleDbType.Varchar2).Value = lname;
                        updateCommand.Parameters.Add(":phone", OracleDbType.Int32).Value = phone;
                        updateCommand.Parameters.Add(":Email", OracleDbType.Varchar2).Value = Email;
                        updateCommand.Parameters.Add(":Address", OracleDbType.Varchar2).Value = Address;
                        updateCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("client data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatagridviewClients();

                    }
                }
                else
                {
                    MessageBox.Show("ID, first name, last name, and phone cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string id = textBoxID.Text;

                if (!string.IsNullOrWhiteSpace(id))
                {
                    string deleteQuery = "DELETE FROM property_client WHERE client_id = :id";
                    using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        deleteCommand.ExecuteNonQuery();

                        MessageBox.Show("client data removed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatagridviewClients();
                        displayClientsCount();
                    }
                }
                else
                {
                    MessageBox.Show("ID cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }



        private void dataGridViewOwner_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridViewClient.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridViewClient.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridViewClient.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridViewClient.CurrentRow.Cells[3].Value.ToString();
            textBoxEmail.Text = dataGridViewClient.CurrentRow.Cells[4].Value.ToString();
            textBoxAddress.Text = dataGridViewClient.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
