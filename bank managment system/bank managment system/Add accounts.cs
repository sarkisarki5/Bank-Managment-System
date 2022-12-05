using System;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace bank_managment_system
{
    public partial class Add_accounts : Form
    {
        public Add_accounts()
        {
            InitializeComponent();
            DisplayAccount();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\92303\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayAccount()
        {
            Con.Open();
            string Query = "select * from Accounttbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet1();
            sda.Fill(ds);
            AccountDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Reset()
        {
            ACnametext.Text = "";
            ACphonetext.Text = "";
            ACaddresstext.Text = "";
            ACgendercomboBox.SelectedIndex = -1;
            ACoccupationtext.Text = "";
            ACeducationcomboBox.SelectedIndex = -1;
            ACincometext.Text = "";
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
            if (ACnametext.Text == "" || ACphonetext.Text == "" || ACaddresstext.Text == "" || ACgendercomboBox.SelectedIndex == -1 || ACoccupationtext.Text == "" || ACeducationcomboBox.SelectedIndex == -1 || ACincometext.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                   Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AccountTbl(ACname,ACphone,ACaddress,ACgen,ACoccupation,ACeduc,ACinc,ACbal) values(@AN,@AP,@AA,@AG,@AO,@AE,@AI,@AB)",Con);
                    cmd.Parameters.AddWithValue("@AN", ACnametext.Text);
                    cmd.Parameters.AddWithValue("@AP", ACphonetext.Text);
                    cmd.Parameters.AddWithValue("@AA", ACaddresstext.Text);
                    cmd.Parameters.AddWithValue("@AG", ACgendercomboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", ACoccupationtext.Text);
                    cmd.Parameters.AddWithValue("@AE", ACeducationcomboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", ACincometext.Text);
                    cmd.Parameters.AddWithValue("@AB", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created!!!");
                    Con.Close();
                    Reset();
                    DisplayAccount();
                } catch(Exception Ex)
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
            if (key ==0)
            {
                MessageBox.Show("Select the Account");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Accounttbl where ACNum=@Ackey", Con);
                    cmd.Parameters.AddWithValue("@Ackey", key );
                  
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Deleted!!!");
                    Con.Close();
                    Reset();
                    DisplayAccount();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
    }
        int key = 0;
        private void AccountDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ACnametext.Text = AccountDGV.SelectedRows[0].Cells[1].Value.ToString();
            ACphonetext.Text = AccountDGV.SelectedRows[0].Cells[2].Value.ToString();
            ACaddresstext.Text = AccountDGV.SelectedRows[0].Cells[3].Value.ToString();
            ACgendercomboBox.SelectedItem = AccountDGV.SelectedRows[0].Cells[4].Value.ToString();
            ACoccupationtext.Text = AccountDGV.SelectedRows[0].Cells[5].Value.ToString();
            ACeducationcomboBox.SelectedItem = AccountDGV.SelectedRows[0].Cells[6].Value.ToString();
            ACincometext.Text = AccountDGV.SelectedRows[0].Cells[7].Value.ToString();
            if(ACnametext.Text == "")
                {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AccountDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (ACnametext.Text == "" || ACphonetext.Text == "" || ACaddresstext.Text == "" || ACgendercomboBox.SelectedIndex == -1 || ACoccupationtext.Text == "" || ACeducationcomboBox.SelectedIndex == -1 || ACincometext.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update AccountTbl set ACname=@AN,ACphone=@AP,ACaddress=@AA,ACgen=@AG,ACoccupation=@AO,ACeduc=@AE,ACinc=@AI where Acnum=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AN", ACnametext.Text);
                    cmd.Parameters.AddWithValue("@AP", ACphonetext.Text);
                    cmd.Parameters.AddWithValue("@AA", ACaddresstext.Text);
                    cmd.Parameters.AddWithValue("@AG", ACgendercomboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", ACoccupationtext.Text);
                    cmd.Parameters.AddWithValue("@AE", ACeducationcomboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", ACincometext.Text);
                    cmd.Parameters.AddWithValue("@AcKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Updated!!!");
                    Con.Close();
                    Reset();
                    DisplayAccount();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

       // private void guna2PictureBox1_Click(object sender, EventArgs e)
        //{
          //  About Obj = new About();
          //  Obj.Show();
          //  this.Hide();
        //}
    }
    }
