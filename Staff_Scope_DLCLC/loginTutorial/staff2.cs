using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace loginTutorial
{
    public partial class staff2 : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        public staff2()
        {
            InitializeComponent();
        }

        private void staff2_Load(object sender, EventArgs e)
        {
            
            MaximizeBox = false;
            label1.Text = Class1.uname;
           
        }

        private void staff2_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is staff)
                {
                    frm.Show();
                }
            }
        }
        bool Check()
        {
            string pass;
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string sqlSelectQuery = "select * from tbluser where usr ='" + label1.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pass = dr["pwd"].ToString();
                if (pass == textBox1.Text)
                {
                    return true;
                }

            }
            return false;

        }

        private void loginadmin_Click(object sender, EventArgs e)
        {
            if (Check() == true)
            {
                if (txtpass.Text == txtpass1.Text)
                {
                    con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                    con.Open();
                    cmd = new SqlCommand("UPDATE tbluser set pwd=@a1 WHERE usr=@a2", con);
                    cmd.Parameters.Add("a1", txtpass.Text);
                    cmd.Parameters.Add("a2", Convert.ToInt32(label1.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Password Succesfully Changed");
                }
                else
                {
                    MessageBox.Show("Please Confirm New Password.");
                }
            }
            else 
            {
                MessageBox.Show("Please enter the old password.");
            }
                
        }
    }
}
