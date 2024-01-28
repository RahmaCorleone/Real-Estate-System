using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace real_state_app1.form
{
    public partial class PropertyTypesForm : Form
    {

        private OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;"); // replace with your actual connection string

        public PropertyTypesForm()
        {
            InitializeComponent();
        }
        classes.property_type pType = new classes.property_type();
        private void PropertyTypesForm_Load(object sender, EventArgs e)
        {
            LoadListboxData();
            displayTypesCount();
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string name = textBoxName.Text;
                string description = textBoxDescription.Text;
                string id = textBoxID.Text;

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
                {
                    string insertQuery = "INSERT INTO property_type (property_id,property_name, property_description) VALUES (:id,:name, :description)";
                    using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        insertCommand.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                        insertCommand.Parameters.Add(":description", OracleDbType.Varchar2).Value = description;

                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("Data inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadListboxData();
                        displayTypesCount();
                    }
                }
                else
                {
                    MessageBox.Show("Name and Description cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void listBoxTypes_Click_1(object sender, EventArgs e)
        {
            DataRow dr = pType.GetAllType().Rows[listBoxTypes.SelectedIndex];
            textBoxID.Text = dr.ItemArray[0].ToString();
            textBoxName.Text = dr.ItemArray[1].ToString();
            textBoxDescription.Text = dr.ItemArray[2].ToString();
        }

        private void listBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();

                string name = textBoxName.Text;
                string description = textBoxDescription.Text;
                string id = textBoxID.Text;

                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
                {
                    string updateQuery = "UPDATE property_type SET property_name = :name, property_description = :description WHERE property_id = :id";
                    using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                        updateCommand.Parameters.Add(":description", OracleDbType.Varchar2).Value = description;
                        updateCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadListboxData();
                            displayTypesCount();
                        }
                        else
                        {
                            MessageBox.Show("No records found with the specified ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Name and Description cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void LoadListboxData()
        {
            listBoxTypes.DataSource = pType.GetAllType();
            listBoxTypes.DisplayMember = "property_name";
            listBoxTypes.ValueMember = "property_id";
            //remove selection from listbox
            listBoxTypes.SelectedItem = null;
        }
        public void displayTypesCount()
        {
            labelCount.Text = listBoxTypes.Items.Count + "Type(s)";
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string id = textBoxID.Text;

                if (!string.IsNullOrWhiteSpace(id))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM property_type WHERE property_id = :id";
                        using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadListboxData();
                                displayTypesCount();
                                textBoxID.Text = "";
                                textBoxName.Text = "";
                                textBoxDescription.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("No records found with the specified ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
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

    }
}


