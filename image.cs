using ImagesSliderForm;
using Oracle.ManagedDataAccess.Client;
using real_state_app1;
using real_state_app1.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropertyImagesForm
{


    public partial class image : Form
    {

        private OracleConnection connection = new OracleConnection("Data Source=localhost;User Id=hr;Password=1234;");
        Class1 pType = new Class1();
        public image()
        {
            InitializeComponent();
            LoadListboxData();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please select an image before inserting.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Assuming your DataGridView has a column named "property_id"
                int propertyId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["property_id"].Value);

                // Convert the image to bytes
                MemoryStream pic = new MemoryStream();
                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                byte[] imageBytes = pic.ToArray();

                // Insert the image into the property_image table
                string insertQuery = "INSERT INTO property_images (image_id, property_id, image) VALUES (image_id_seq.NEXTVAL, :propertyId, :imageData)";
                using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.Add(":propertyId", OracleDbType.Int32).Value = propertyId;
                    insertCommand.Parameters.Add(":imageData", OracleDbType.Blob).Value = imageBytes;

                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Image inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            listBoxType.DataSource = pType.GetAllType();
            listBoxType.DisplayMember = "property_name";
            listBoxType.ValueMember = "property_id";
            //remove selection from listbox
            listBoxType.SelectedItem = null;
            listBoxImagesId.SelectedItem = null;
            dataGridView1.ClearSelection();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Change font size for column headers
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10);

            // Change font size for row headers
            dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10);
            // Assuming dataGridView1 is the name of your DataGridView
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10); // Adjust the font and size as needed

            try
            {
                dataGridView1.DataSource = property_type.Getimageproperty("the_property");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif ";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image=Image.FromFile(opf.FileName);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                imageslider sliderf = new imageslider();
                int propertyId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                sliderf.showImage(propertyId);
                sliderf.ShowDialog();
                sliderf.Show();
            }
            catch 
            {
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        MessageBox.Show("This proprty doesnt have any images to display", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        MessageBox.Show("You need to select the property first", "select property", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row before deleting.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Assuming your DataGridView has a column named "image_id"
                int imageId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["image_id"].Value);

                // Display a confirmation message
                DialogResult result = MessageBox.Show("Are you sure you want to delete this image?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the image from the property_image table
                    string deleteQuery = "DELETE FROM property_images WHERE image_id = :imageId";
                    using (OracleCommand deleteCommand = new OracleCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.Add(":imageId", OracleDbType.Int32).Value = imageId;

                        connection.Open();
                        deleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Image deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void listBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Class1 dtype = new Class1();


            //OracleCommand command = new OracleCommand("SELECT property_id,property_type,property_age FROM the_property where property_type=:tp");
            //int typeId = Convert.ToInt32(listBoxType.SelectedValue);
            //command.Parameters.Add(":tp", OracleDbType.Int32).Value = typeId;
            //dataGridView1.DataSource= dtype.GetData(command);
        }

        private void listBoxImagesId_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Class1 func = new Class1();
            OracleCommand command = new OracleCommand("SELECT * FROM property_images where property_id=:id");
            int propertyId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            command.Parameters.Add(":id", OracleDbType.Int32).Value = propertyId;
            listBoxImagesId.DataSource = func.GetData(command);
            listBoxImagesId.DisplayMember = "property_id";
            listBoxImagesId.ValueMember = "property_id";

        }

        private void listBoxImagesId_Click(object sender, EventArgs e)
        {
            int picId=Convert.ToInt32(listBoxImagesId.SelectedValue);
        }
    }
}
