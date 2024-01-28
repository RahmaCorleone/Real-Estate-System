using real_state_app1;
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
using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Tls;
using real_state_app1.form;
namespace SaleForm
{
   
    public partial class sale : Form
    {
        private OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;");
       public OracleConnection connectionString = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;");

        Class1 func = new Class1();
        public sale()
        {
            InitializeComponent();
        }
        public void populateDatagridview(DataGridView dgv, string query)
        {
            try
            {
                string connectionString = "Data Source=localhost;User Id=hr;Password=1234;";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dgv.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewClient.DataSource = Class1.GetAllOwner("property_client");
            dataGridView2.DataSource = Class1.Getproperties("the_property");
            dataGridView1.DataSource = Class1.Getsale("property_sale");
            dataGridViewClient.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewClient_Click(object sender, EventArgs e)
        {
            textBoxclient.Text= dataGridViewClient.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            ID.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            price.Text= dataGridView2.CurrentRow.Cells[2].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                try
                {
                    connection.Open();
                    string SaleId = ID.Text;
                    string propertyId = textBox3.Text;
                    string clientId = textBoxclient.Text;
                    string sellingPrice = price.Text;
                    DateTime saleDate = dateTimePicker1.Value;

                    if (!string.IsNullOrWhiteSpace(propertyId) && !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(sellingPrice))
                    {
                        // Assuming SALE_ID is generated automatically by the database
                        string insertQuery = "INSERT INTO property_sale (SALE_ID,PROPERTY_ID, CLIENT_ID, SELLING_PRICE, SALE_DATA) " +
                                             "VALUES (:SaleId,:propertyId, :clientId, :sellingPrice, :saleDate)";

                        using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.Add(":SaleId", OracleDbType.Int32).Value = int.Parse(SaleId);
                            insertCommand.Parameters.Add(":propertyId", OracleDbType.Int32).Value = int.Parse(propertyId);
                            insertCommand.Parameters.Add(":clientId", OracleDbType.Int32).Value = int.Parse(clientId);
                            insertCommand.Parameters.Add(":sellingPrice", OracleDbType.Varchar2).Value = sellingPrice;
                            insertCommand.Parameters.Add(":saleDate", OracleDbType.Date).Value = saleDate;

                            insertCommand.ExecuteNonQuery();

                            MessageBox.Show("Data inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Property ID, Client ID, and Selling Price cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ID.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString(); 
            textBoxclient.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            price.Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Value= Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string SaleId = ID.Text;
                string propertyId = textBox3.Text;
                string clientId = textBoxclient.Text;
                string sellingPrice = price.Text;
                DateTime saleDate = dateTimePicker1.Value;

                if (!string.IsNullOrWhiteSpace(SaleId) && !string.IsNullOrWhiteSpace(propertyId) && !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(sellingPrice))
                {
                    // Assuming SALE_ID is generated automatically by the database
                    string updateQuery = "UPDATE property_sale " +
                                         "SET PROPERTY_ID = :propertyId, CLIENT_ID = :clientId, SELLING_PRICE = :sellingPrice, SALE_DATA = :saleDate " +
                                         "WHERE SALE_ID = :SaleId";

                    using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.Add(":propertyId", OracleDbType.Int32).Value = int.Parse(propertyId);
                        updateCommand.Parameters.Add(":clientId", OracleDbType.Int32).Value = int.Parse(clientId);
                        updateCommand.Parameters.Add(":sellingPrice", OracleDbType.Varchar2).Value = sellingPrice;
                        updateCommand.Parameters.Add(":saleDate", OracleDbType.Date).Value = saleDate;
                        updateCommand.Parameters.Add(":SaleId", OracleDbType.Int32).Value = int.Parse(SaleId);

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Sale ID, Property ID, Client ID, and Selling Price cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Class1.Getsale("property_sale");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string SaleId = ID.Text;

                if (!string.IsNullOrWhiteSpace(SaleId))
                {
                    // Assuming SALE_ID is generated automatically by the database
                    string deleteQuery = "DELETE FROM property_sale WHERE SALE_ID = :SaleId";

                    using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.Add(":SaleId", OracleDbType.Int32).Value = int.Parse(SaleId);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Sale ID not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sale ID cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string query = "select property_id,property_owner_id,property_price,property_address from the_property where property_id  in(select property_id from property_sale)";
            populateDatagridview(dataGridView2, query);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string query = "select property_id,property_owner_id,property_price,property_address from the_property where property_id not in(select property_id from property_sale)";
            populateDatagridview(dataGridView2, query);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "select property_id,property_owner_id,property_price,property_address from the_property ";
            populateDatagridview(dataGridView2, query);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClientForm form = new ClientForm();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridViewClient.DataSource = Class1.GetAllOwner("property_client");
        }
    }
    }
