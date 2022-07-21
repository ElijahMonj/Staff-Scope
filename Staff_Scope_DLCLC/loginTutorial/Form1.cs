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

using System.Net;
using System.Data.Sql;
using loginTutorial.Properties;

namespace loginTutorial
{
    public partial class Form1 : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
            

        }
        
        dbcontrol sql = new dbcontrol();

        int counter;
        bool Login()
        {

            sql.Param("@usr", txtuser.Text);
            sql.Param("@pwd", txtpass.Text);
            sql.query("select count(*) from tbluser where usr=@usr and pwd=@pwd");
            if ((int)sql.dt.Rows[0][0]==1)
            {
                counter = 0;
                return true;
                
            }
            if (counter < 3)
            {
                MessageBox.Show("Invalid Username or Password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                counter++;
            } else if ( counter >=3) 
            {
                MessageBox.Show("Please Contact the Admin for Password Reset", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");

            string query = "Select * from tbladmin Where username ='" + txtuser.Text.Trim() + "' and password = '" + txtpass.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if (dtbl.Rows.Count == 1)
            {
                logrecords objlogrecords = new logrecords();
                this.Hide();
                objlogrecords.ShowDialog();
            }
            else
            {
                if (Login() == true)
                {

                    //
                    
                    //
                    Class1.uname = txtuser.Text;
                    
                    staff a = new staff();
                    this.Hide();
                    a.Show();
                }
            }

          
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start(); MaximizeBox = false;
            label6.Text = DateTime.Now.ToLongTimeString();
            label7.Text = DateTime.Now.ToLongDateString();

            CheckFirstRun();
            
        }

        private static void CheckFirstRun()
        {
            if (Settings.Default.FirstRun)
            {
                MessageBox.Show(
                    "Welcome to Staff Scope!");
                Settings.Default.FirstRun = false;
                Settings.Default.Save();

                SqlCommand cmd;
                SqlConnection con;

                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();

                cmd = new SqlCommand("INSERT INTO [tbladmin] (username,password)"
                    + "VALUES('admin','admin')", con);





                int i = cmd.ExecuteNonQuery();

                con.Close();

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }



       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void adminusername_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }


      

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtuser_OnValueChanged(object sender, EventArgs e)
        {

        }

       
    }
}
