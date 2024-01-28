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

namespace real_state_app1.form
{
    public partial class OwnerListForm : Form
    {
        public OwnerListForm()
        {
            InitializeComponent();
        }

        private void OwnerListForm_Load(object sender, EventArgs e)
        {
            LoadDatagridviewOwners();
            ownerlist.ClearSelection();
        }

        private void ownerlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void LoadDatagridviewOwners()
        {
            try
            {
                ownerlist.DataSource = property_type.GetApartOwner("property_owner");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ownerlist_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
