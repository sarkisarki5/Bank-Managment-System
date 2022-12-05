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
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\92303\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30");

        int Balance;
        private void checkBalance()
        {
            Con.Open();
            string query = "select * from Accounttbl where  Acnum=" + checkbaltb.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                balancelbl.Text = "RS " + dr["ACbal"].ToString();
                Balance = Convert.ToInt32(dr["ACbal"].ToString());
            }
               

            Con.Close();
        }
        private void CheckAvailableBal()
        {
          //  Con.Open();
            string query = "select * from Accounttbl where  Acnum=" + Fromtb.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Fromlabel.Text = "RS " + dr["ACbal"].ToString();
                Balance = Convert.ToInt32(dr["ACbal"].ToString());
            }


         //   Con.Close();
        }
        private void GetNewBalance()
        {
            Con.Open();
            string query = "select * from Accounttbl where  Acnum=" + checkbaltb.Text + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter SDA = new SqlDataAdapter(cmd);
            SDA.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               balancelbl.Text = "RS " + dr["ACbal"].ToString();
                Balance = Convert.ToInt32(dr["ACbal"].ToString());
            }
            Con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkbalbtn_Click(object sender, EventArgs e)
        {
            if(checkbaltb.Text == "")
            {
                MessageBox.Show("Enter Account Number");
            }
            else
            {
                checkBalance();
                if(balancelbl.Text == "Your Balance")
                {
                    MessageBox.Show("Account Not Found");
                    checkbaltb.Text = "";
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Deposit()
        {
           
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Transactiontbl(TName,TDate,TAmt,TACNum) values(@TN,@TD,@TA,@TAC)", Con);
                    cmd.Parameters.AddWithValue("@TN", "Deposit");
                    cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@TA", Depositamout.Text);
                    cmd.Parameters.AddWithValue("@TAC", DepositAcctb.Text);

                cmd.ExecuteNonQuery();
                    MessageBox.Show("Money Deposit!!!");
                    Con.Close();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        private void Withdraw()
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into Transactiontbl(TName,TDate,TAmt,TACNum) values(@TN,@TD,@TA,@TAC)", Con);
                cmd.Parameters.AddWithValue("@TN", "Withdraw");
                cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TA", Wdamount.Text);
                cmd.Parameters.AddWithValue("@TAC", Wdaccnum.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Money WithDraw!!!");
                Con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void subtractbal()
        {
            GetNewBalance();
            int newbal = Balance - Convert.ToInt32(Transactionamttb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update AccountTbl set ACbal=@AB where Acnum=@AcKey", Con);
                cmd.Parameters.AddWithValue("@AB", newbal);
                cmd.Parameters.AddWithValue("@AcKey", Fromtb.Text);
                cmd.ExecuteNonQuery();
                Con.Close();
              
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Addbal()
        {
            GetNewBalance();
            int newbal = Balance + Convert.ToInt32(Transactionamttb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update AccountTbl set ACbal=@AB where Acnum=@AcKey", Con);
                cmd.Parameters.AddWithValue("@AB", newbal);
                cmd.Parameters.AddWithValue("@AcKey", Totb.Text);
                cmd.ExecuteNonQuery();
                Con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void depositbtn_Click(object sender, EventArgs e)
        {
            if (Depositamout.Text =="" || DepositAcctb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Deposit();
                GetNewBalance();
                int newbal = Balance + Convert.ToInt32(Depositamout.Text);
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update AccountTbl set ACbal=@AB where Acnum=@AcKey", Con);
                    cmd.Parameters.AddWithValue("@AB", newbal);
                    cmd.Parameters.AddWithValue("@AcKey", DepositAcctb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Money Deposit!!!");
                    Con.Close();
                    Depositamout.Text = "";
                    DepositAcctb.Text = "";
                    balancelbl.Text = "Your Balance";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        
        }

        private void wdbtn_Click(object sender, EventArgs e)
        {
            if (Wdamount.Text == "" || Wdaccnum.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Withdraw();
                GetNewBalance();
                if (Balance < Convert.ToInt32(Wdamount.Text))
                {
                    MessageBox.Show("Insufisiant Balance");
                }
                else
                {
                    int newbal = Balance - Convert.ToInt32(Wdamount.Text);

                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("update AccountTbl set ACbal=@AB where Acnum=@AcKey", Con);
                        cmd.Parameters.AddWithValue("@AB", newbal);
                        cmd.Parameters.AddWithValue("@AcKey", Wdaccnum.Text);
                        cmd.ExecuteNonQuery();
                       
                        Con.Close();
                        Wdamount.Text = "";
                        Wdaccnum.Text = "";
                        balancelbl.Text = "Your Balance";
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            if (Fromtb.Text == "")
            {
                MessageBox.Show("Enter Source Account");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Accounttbl where Acnum='" + Fromtb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    CheckAvailableBal();
                    Con.Close();
                }
                else
                {
                    MessageBox.Show("Account Does Not Found");
                    Fromtb.Text = "";
                    
                }
                Con.Close();
            }
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            if (Totb.Text == "")
            {
                MessageBox.Show("Enter Destination Account");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Accounttbl where Acnum='" + Totb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    //        CheckAvailableBal();
                    MessageBox.Show("Account Found");
                    Con.Close();
                    if (Totb.Text == Fromtb.Text)
                    {
                        MessageBox.Show("Source And Destination Account Are Same ");
                        Totb.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Account Does Not Found");
                    Totb.Text = "";

                }
                Con.Close();
            }
        }

        private void Transfer()
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into Transfertbl(TrSrc,TrDest,TrAmt,TrDate) values(@TRS,@TRD,@TRA,@TDE)", Con);
                cmd.Parameters.AddWithValue("@TRS",Fromtb.Text);
                cmd.Parameters.AddWithValue("@TRD", Totb.Text);
                cmd.Parameters.AddWithValue("@TRA", Transactionamttb.Text);
                cmd.Parameters.AddWithValue("@TDE", DateTime.Now.Date);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Money Transfered!!!");
                Con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void Transferbtn_Click(object sender, EventArgs e)
        {
            if (Totb.Text == "" || Fromtb.Text == "" || Transactionamttb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }else if (Convert.ToInt16(Transactionamttb.Text) > Balance)
            {
                MessageBox.Show("Insufisiant Balance");
            }
            else
            {
                Transfer();
                subtractbal();
                Addbal();
                Fromtb.Text = "";
                Totb.Text = "";
                Transactionamttb.Text = "";
            }
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            checkbaltb.Text = "";
            balancelbl.Text = "";
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

        private void Transactions_Load(object sender, EventArgs e)
        {

        }
    }
}
