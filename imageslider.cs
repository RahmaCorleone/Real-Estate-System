using real_state_app1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagesSliderForm
{
    public partial class imageslider : System.Windows.Forms.Form
    {
        public imageslider()
        {
            InitializeComponent();
        }
        int position = 0;   
        private void imageslider_Load(object sender, EventArgs e)
        {

        }
        DataTable images;
        public void showImage(int propertyId)
        {

            Class1 propertyImage = new Class1();
            images = propertyImage.getPropertyImages(propertyId);
            labelCount.Text= Convert.ToString(images.Rows.Count);
            pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])images.Rows[0]["image"])); 
        }

        public void displayImage(int index)
        {
            labelTotal.Text = Convert.ToString(images.Rows.Count);
            pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])images.Rows[0]["image"]));
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            position += 1;
            labelCount.Text = Convert.ToString(position); 
            displayImage(position); 

        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            position -= 1;
            labelCount.Text = Convert.ToString(position);
            displayImage(position);
        }
    }
}
