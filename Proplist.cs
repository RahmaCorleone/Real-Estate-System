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
using MySql.Data.MySqlClient;
using real_state_app1.classes;

namespace PropertiesList
{

    public partial class Proplist : Form
    {

        public Proplist()
        {
            InitializeComponent();
        }

        private void PropertiesListForm_Load(object sender, EventArgs e)
        {
            //change the datagridview row height
            dataGridView1.RowTemplate.Height = 50;

            //populate the datagridview
            displayOwnersCount();

            try
            {
                dataGridView1.DataSource = property_type.GetAllOwner("the_property");

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        public void populateDataGridview()
        {
            dataGridView1.RowTemplate.Height = 40;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void displayOwnersCount()
        {
            string propertycount = property_type.GetAllOwner("the_property").Rows.Count.ToString();
            label1.Text = propertycount + "Types";

        }

    }
}
