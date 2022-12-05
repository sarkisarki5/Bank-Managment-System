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

namespace bank_managment_system
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Adminnewpass.Text = "";
            ThemecomboBox.SelectedIndex = -1;

        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            if (ThemecomboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select A Theme");
            }
            else if (ThemecomboBox.SelectedIndex == 0)
            {
                panel1.BackColor = Color.Gold;
            }
            else if (ThemecomboBox.SelectedIndex == 1)
            {
                panel1.BackColor = Color.Crimson;
            }
            else
            {
                panel1.BackColor = Color.Green;
            }
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\92303\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (Adminnewpass.Text == "")
            {
                MessageBox.Show("Enter New Password");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update Admintbl set Adpass=@AP where AdId=@Akey", Con);
                    cmd.Parameters.AddWithValue("@AP", Adminnewpass.Text);
                    cmd.Parameters.AddWithValue("@AKey", 1);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password Updated!!!");
                    Con.Close();
                    Adminnewpass.Text = "";

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
