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
    public partial class Agent : Form
    {
        public Agent()
        {
            InitializeComponent();
            DisplayAgents();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\92303\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAgents()
        {
            Con.Open();
            string Query = "select * from Agenttbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet1();
            sda.Fill(ds);
            AgentDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Reset()
        {
            ANametb.Text = "";
            APasswordtb.Text = "";
            AAddresstext.Text = "";
            
Aphonetext.Text = "";
            
        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
            if (ANametb.Text == "" || APasswordtb.Text == "" || AAddresstext.Text == ""  || Aphonetext.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                { 
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Agenttbl(AName,APass,APhone,Aaddress) values(@An,@APA,@APn,@Aa)", Con);
                    cmd.Parameters.AddWithValue("@An", ANametb.Text);
                    cmd.Parameters.AddWithValue("@APA", APasswordtb.Text);
                    cmd.Parameters.AddWithValue("@Aa", AAddresstext.Text);
                    cmd.Parameters.AddWithValue("@APn", Aphonetext.Text);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Added!!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Account");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Agenttbl where AId=@Akey", Con);
                    cmd.Parameters.AddWithValue("@Akey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Deleted!!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void AgentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ANametb.Text = AgentDGV.SelectedRows[0].Cells[1].Value.ToString();
            APasswordtb.Text = AgentDGV.SelectedRows[0].Cells[2].Value.ToString();
            Aphonetext.Text = AgentDGV.SelectedRows[0].Cells[3].Value.ToString();
            AAddresstext.Text = AgentDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (ANametb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AgentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (ANametb.Text == "" || APasswordtb.Text == "" || AAddresstext.Text == "" || Aphonetext.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update Agenttbl set AName=@An,APass=@APA,APhone=@APn,Aaddress=@Aa where AId=@Akey", Con);
                    cmd.Parameters.AddWithValue("@An", ANametb.Text);
                    cmd.Parameters.AddWithValue("@APA", APasswordtb.Text);
                    cmd.Parameters.AddWithValue("@APn", AAddresstext.Text);
                    cmd.Parameters.AddWithValue("@Aa", Aphonetext.Text);
                    cmd.Parameters.AddWithValue("@AKey",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Updated!!!");
                    Con.Close();
                    Reset();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Setting Obj = new Setting();
            Obj.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Setting Obj = new Setting();
            Obj.Show();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            login Obj = new login();
            Obj.Show();
            this.Hide();
        }

      
    }
}
