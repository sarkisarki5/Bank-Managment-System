using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bank_managment_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

       
        int startP = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 5;
            if (startP < 101) {
                Myprogress.Value = startP;
                if (Myprogress.Value == 100)
                {
                    Myprogress.Value = 0;
                    timer1.Stop();
                    login obj = new login();
                    obj.Show();
                    this.Hide();

                }
            }
        }

       

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            timer1.Start();
        }

        private void Myprogress_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
