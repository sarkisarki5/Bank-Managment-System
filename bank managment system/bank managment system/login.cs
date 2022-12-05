using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace bank_managment_system
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\92303\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ACgendercomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Usernametb.Text = "";
            Passwordtb.Text = "";
            Rolecb.SelectedIndex = -1;
            Rolecb.Text = "Role";
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            if(Rolecb.SelectedIndex == -1)
            {
                MessageBox.Show("Select the Role");
            }
            else if(Rolecb.SelectedIndex== 0)
            {
                if(Usernametb.Text =="" || Passwordtb.Text == "" )
                {
                    MessageBox.Show("Enter Both Admin And Admin Password");
                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Admintbl where Adname='" + Usernametb.Text + "' and Adpass='" + Passwordtb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if(dt.Rows[0][0].ToString() == "1")
                    {
                        Agent Obj = new Agent();
                        Obj.Show();
                        this.Hide();
                        Con.Close();
                    }else
                    {
                        MessageBox.Show("Wrong Admin Name or Password");
                        Usernametb.Text = "";
                        Passwordtb.Text = "";
                    }
                    Con.Close();
                }
            }
            else
            {
                if (Usernametb.Text == "" || Passwordtb.Text == "")
                {
                    MessageBox.Show("Enter Both User Name And Password");
                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Agenttbl where AName='" + Usernametb.Text + "' and APass='" + Passwordtb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MainMenu Obj = new MainMenu();
                        Obj.Show();
                        this.Hide();
                        Con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong User Name or Password");
                        Usernametb.Text = "";
                        Passwordtb.Text = "";
                    }
                    Con.Close();
                }
            }
        }

        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {
            Form2 Obj = new Form2();
            Obj.Show();
            this.Hide();
        }
    }
}
