using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZstdSharp.Unsafe;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Management;
using System.Security.Cryptography.X509Certificates;
using SaleForm;
using PropertyImagesForm;
using PropertiesList;
using ImagesSliderForm;

namespace real_state_app1.form
{
    public partial class TheProbertyForm : Form
    {
        private OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;");
        public TheProbertyForm()
        {
            InitializeComponent();
        }
        classes.property_type property=new classes.property_type();
        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //select owner
        private void button1_Click(object sender, EventArgs e)
        {
            OwnerListForm ownerf = new OwnerListForm();
            ownerf.ShowDialog();
            owner.Text = ownerf.ownerlist.CurrentRow.Cells[0].Value.ToString();
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void TheProbertyForm_Load(object sender, EventArgs e)
        {
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bedroom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string id = textBoxID.Text;
                string type = textBoxType.Text; // Assuming you have a ComboBox for property type
                string squareFeet = Size.Text;
                string ownerID = owner.Text; // Assuming you have a TextBox for owner ID
                string price = price1.Text;
                string address = textBoxAddress.Text;
                string bedrooms = bedroom.Text;
                string bathrooms = bathroom.Text;
                string age = age1.Text;
                string balcony = textBox1.Text;
                string backyard = textBox2.Text;
                string pool = textBox3.Text;
                string garage = textBox4.Text;
                string fireplace = textBox5.Text;
                string commentS = comments.Text;

                // Check if required fields are not empty
                if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(squareFeet) &&
                    !string.IsNullOrWhiteSpace(ownerID) && !string.IsNullOrWhiteSpace(price) &&
                    !string.IsNullOrWhiteSpace(address))
                {
                    string insertQuery = "INSERT INTO the_property (property_id, property_type, property_square_feet, " +
                    "property_owner_id, property_price, property_address, property_bedrooms, " +
                    "property_bathrooms, property_age, balcony, backyard, pool, garage, fireplace, property_comment) " +
                    "VALUES (:id, :type, :squareFeet, :ownerID, :price, :address, :bedrooms, :bathrooms, :age, " +
                    ":balcony, :backyard, :pool, :garage, :fireplace, :commentS)";
                    using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.Add(":id", OracleDbType.Int32).Value = int.Parse(id) ;
                        insertCommand.Parameters.Add(":type", OracleDbType.Int32).Value = int.Parse(type);
                        insertCommand.Parameters.Add(":squareFeet", OracleDbType.Int32).Value = int.Parse(squareFeet);
                        insertCommand.Parameters.Add(":ownerID", OracleDbType.Int32).Value = int.Parse(ownerID);
                        insertCommand.Parameters.Add(":price", OracleDbType.Int32).Value = int.Parse(price);
                        insertCommand.Parameters.Add(":address", OracleDbType.Varchar2).Value = address;
                        insertCommand.Parameters.Add(":bedrooms", OracleDbType.Int32).Value = int.Parse(bedrooms);
                        insertCommand.Parameters.Add(":bathrooms", OracleDbType.Int32).Value = int.Parse(bathrooms);
                        insertCommand.Parameters.Add(":age", OracleDbType.Int32).Value = int.Parse(age);
                        insertCommand.Parameters.Add(":balcony", OracleDbType.Varchar2).Value = balcony;
                        insertCommand.Parameters.Add(":backyard", OracleDbType.Varchar2).Value = backyard;
                        insertCommand.Parameters.Add(":pool", OracleDbType.Varchar2).Value = pool;
                        insertCommand.Parameters.Add(":garage", OracleDbType.Varchar2).Value = garage;
                        insertCommand.Parameters.Add(":fireplace", OracleDbType.Varchar2).Value = fireplace;
                        insertCommand.Parameters.Add(":commentS", OracleDbType.Varchar2).Value = commentS;

                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("Property data inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Optionally, you can refresh your DataGridView or perform other actions here
                    }
                }
                else
                {
                    MessageBox.Show("Type, Square Feet, Owner ID, Price, and Address cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }
        //search
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string sql = "SELECT * FROM the_property WHERE property_id = :searchId";

                using (OracleCommand cmd = new OracleCommand(sql, connection))
                {
                    // Assuming textBoxID is a TextBox where you enter the property ID
                    cmd.Parameters.Add(":searchId", OracleDbType.Int32).Value = int.Parse(textBoxID.Text);

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];

                        // Assuming these are the TextBox controls where you want to display the results
                        textBoxType.Text = row["PROPERTY_TYPE"].ToString();
                        Size.Text = row["PROPERTY_SQUARE_FEET"].ToString();
                        owner.Text = row["PROPERTY_OWNER_ID"].ToString();
                        price1.Text = row["PROPERTY_PRICE"].ToString();
                        textBoxAddress.Text = row["PROPERTY_ADDRESS"].ToString();
                        bedroom.Text = row["PROPERTY_BEDROOMS"].ToString();
                        bathroom.Text = row["PROPERTY_BATHROOMS"].ToString();
                        age1.Text = row["PROPERTY_AGE"].ToString();
                        textBox1.Text = row["BALCONY"].ToString();
                        textBox2.Text = row["BACKYARD"].ToString();
                        textBox3.Text = row["POOL"].ToString();
                        textBox4.Text = row["GARAGE"].ToString();
                        textBox5.Text = row["FIREPLACE"].ToString();
                        comments.Text = row["PROPERTY_COMMENT"].ToString();
                        // Add more textboxes for other columns as needed
                    }
                    else
                    {
                        MessageBox.Show("Property with the specified ID not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clearFields();
                    }
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
        public void clearFields()
        {
            textBoxType.Text = "";
            Size.Text = "";
            owner.Text = "";
            price1.Text = "";
            textBoxAddress.Text = "";
            bedroom.Text = "";
            bathroom.Text = "";
            age1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comments.Text = "";


        }
        //show all properties
        private void button5_Click(object sender, EventArgs e)
        {
            Proplist proplistf= new Proplist();
            proplistf.ShowDialog();
        }
        //show images
        private void button3_Click(object sender, EventArgs e)
        {
            image propimageF = new image();
            propimageF.Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string id = textBoxID.Text;
                string type = textBoxType.Text; // Assuming you have a ComboBox for property type
                string squareFeet = Size.Text;
                string ownerID = owner.Text; // Assuming you have a TextBox for owner ID
                string price = price1.Text;
                string address = textBoxAddress.Text;
                string bedrooms = bedroom.Text;
                string bathrooms = bathroom.Text;
                string age = age1.Text;
                string balcony = textBox1.Text;
                string backyard = textBox2.Text;
                string pool = textBox3.Text;
                string garage = textBox4.Text;
                string fireplace = textBox5.Text;
                string commentS = comments.Text;

                // Check if required fields are not empty
                if (!string.IsNullOrWhiteSpace(id) &&
                    !string.IsNullOrWhiteSpace(type) &&
                    !string.IsNullOrWhiteSpace(squareFeet) &&
                    !string.IsNullOrWhiteSpace(ownerID) &&
                    !string.IsNullOrWhiteSpace(price) &&
                    !string.IsNullOrWhiteSpace(address))
                {
                    string updateQuery = "UPDATE the_property SET " +
                        "property_type = :type, " +
                        "property_square_feet = :squareFeet, " +
                        "property_owner_id = :ownerID, " +
                        "property_price = :price, " +
                        "property_address = :address, " +
                        "property_bedrooms = :bedrooms, " +
                        "property_bathrooms = :bathrooms, " +
                        "property_age = :age, " +
                        "balcony = :balcony, " +
                        "backyard = :backyard, " +
                        "pool = :pool, " +
                        "garage = :garage, " +
                        "fireplace = :fireplace, " +
                        "property_comment = :commentS " +
                        "WHERE property_id = :id";

                    using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.Add(":type", OracleDbType.Int32).Value = int.Parse(type);
                        updateCommand.Parameters.Add(":squareFeet", OracleDbType.Int32).Value = int.Parse(squareFeet);
                        updateCommand.Parameters.Add(":ownerID", OracleDbType.Int32).Value = int.Parse(ownerID);
                        updateCommand.Parameters.Add(":price", OracleDbType.Int32).Value = int.Parse(price);
                        updateCommand.Parameters.Add(":address", OracleDbType.Varchar2).Value = address;
                        updateCommand.Parameters.Add(":bedrooms", OracleDbType.Int32).Value = int.Parse(bedrooms);
                        updateCommand.Parameters.Add(":bathrooms", OracleDbType.Int32).Value = int.Parse(bathrooms);
                        updateCommand.Parameters.Add(":age", OracleDbType.Int32).Value = int.Parse(age);
                        updateCommand.Parameters.Add(":balcony", OracleDbType.Varchar2).Value = balcony;
                        updateCommand.Parameters.Add(":backyard", OracleDbType.Varchar2).Value = backyard;
                        updateCommand.Parameters.Add(":pool", OracleDbType.Varchar2).Value = pool;
                        updateCommand.Parameters.Add(":garage", OracleDbType.Varchar2).Value = garage;
                        updateCommand.Parameters.Add(":fireplace", OracleDbType.Varchar2).Value = fireplace;
                        updateCommand.Parameters.Add(":commentS", OracleDbType.Varchar2).Value = commentS;
                        updateCommand.Parameters.Add(":id", OracleDbType.Int32).Value = int.Parse(id);

                        updateCommand.ExecuteNonQuery();

                        MessageBox.Show("Property data updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Optionally, you can refresh your DataGridView or perform other actions here
                    }
                }
                else
                {
                    MessageBox.Show("ID, Type, Square Feet, Owner ID, Price, and Address cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string id = textBoxID.Text;

                // Check if the ID is not empty
                if (!string.IsNullOrWhiteSpace(id))
                {
                    // Ask for confirmation before deletion
                    DialogResult result = MessageBox.Show("Are you sure you want to delete the property with ID " + id + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM the_property WHERE property_id = :id";

                        using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.Add(":id", OracleDbType.Int32).Value = int.Parse(id);

                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Property data deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Optionally, you can refresh your DataGridView or perform other actions here
                            }
                            else
                            {
                                MessageBox.Show("Property with the specified ID not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        // User clicked "No" in the confirmation dialog
                        MessageBox.Show("Deletion canceled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid ID to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

