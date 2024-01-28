using Oracle.ManagedDataAccess.Client;
using PropertiesList;
using real_state_app1.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace real_state_app1.form
{
    public partial class PropertyOwnerForm : Form
    {
        private OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"); // replace with your actual connection string

        public PropertyOwnerForm()
        {
            InitializeComponent();
        }

        private void dataGridViewOwner_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void LoadDatagridviewOwners()
        {
            try
            {
                dataGridViewOwner.DataSource = property_type.GetAllOwner("property_owner");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void displayOwnersCount()
        {
            string Ownercount = property_type.GetAllOwner("property_owner").Rows.Count.ToString();
            labelowners.Text = Ownercount + "Owner(s)";

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
                    string insertQuery = "INSERT INTO property_owner (owner_id,owner_firstname, owner_last_name,owner_phone,owner_email,owner_address) VALUES (:id,:fname,:lname, :phone,:Email,:Address)";
                    using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        insertCommand.Parameters.Add(":fname", OracleDbType.Varchar2).Value = fname;
                        insertCommand.Parameters.Add(":lname", OracleDbType.Varchar2).Value = lname;
                        insertCommand.Parameters.Add(":phone", OracleDbType.Int32).Value = phone;
                        insertCommand.Parameters.Add(":Email", OracleDbType.Varchar2).Value = Email;
                        insertCommand.Parameters.Add(":Address", OracleDbType.Varchar2).Value = Address;




                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("owner_Data inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatagridviewOwners();
                        displayOwnersCount();
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

        private void PropertyOwnerForm_Load(object sender, EventArgs e)
        {
            LoadDatagridviewOwners();
            displayOwnersCount();
        }

        private void dataGridViewOwner_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridViewOwner.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridViewOwner.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridViewOwner.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridViewOwner.CurrentRow.Cells[3].Value.ToString();
            textBoxEmail.Text = dataGridViewOwner.CurrentRow.Cells[4].Value.ToString();
            textBoxAddress.Text = dataGridViewOwner.CurrentRow.Cells[5].Value.ToString();
        }

        private void labelowners_Click(object sender, EventArgs e)
        {

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
                    string updateQuery = "UPDATE property_owner SET owner_firstname = :fname, owner_last_name = :lname, owner_phone = :phone, owner_email = :Email, owner_address = :Address WHERE owner_id = :id";
                    using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.Add(":fname", OracleDbType.Varchar2).Value = fname;
                        updateCommand.Parameters.Add(":lname", OracleDbType.Varchar2).Value = lname;
                        updateCommand.Parameters.Add(":phone", OracleDbType.Int32).Value = phone;
                        updateCommand.Parameters.Add(":Email", OracleDbType.Varchar2).Value = Email;
                        updateCommand.Parameters.Add(":Address", OracleDbType.Varchar2).Value = Address;
                        updateCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Owner data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatagridviewOwners();
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
                    string deleteQuery = "DELETE FROM property_owner WHERE owner_id = :id";
                    using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        deleteCommand.ExecuteNonQuery();

                        MessageBox.Show("Owner data removed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatagridviewOwners();
                        displayOwnersCount();
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

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

    }
}

