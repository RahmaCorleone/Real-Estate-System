using PropertyImagesForm;
using SaleForm;
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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            labelAppName.Text = "Real\r\nEstate\r\nManger";
            panel1.Height = this.Height;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TheProbertyForm propertyF = new TheProbertyForm();
            propertyF.Show();

        }

        private void propertytypes_Click(object sender, EventArgs e)
        {
            PropertyTypesForm propTypeF= new PropertyTypesForm();
            propTypeF.Show();
        }

        private void owner_Click(object sender, EventArgs e)
        {
            PropertyOwnerForm propOwnerF = new PropertyOwnerForm();
            propOwnerF.Show();
        }

        private void client_Click(object sender, EventArgs e)
        {
            ClientForm propClientF = new ClientForm();
            propClientF.Show();
        }

        private void propertyimges_Click(object sender, EventArgs e)
        {
            image propimageF = new image();
            propimageF.Show();
        }

        private void sale_Click(object sender, EventArgs e)
        {
           sale salef = new sale();
            salef.Show();
        }
    }
}
