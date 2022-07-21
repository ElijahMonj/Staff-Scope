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

using System.Net;
using System.Data.Sql;
namespace loginTutorial
{
    public partial class reset : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        public reset()
        {
            InitializeComponent();
        }
        dbcontrol sql = new dbcontrol();

        private void reset_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is logrecords)
                {
                    frm.Show();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void reset_Load(object sender, EventArgs e)
        {
            txtid.MaxLength = 8;
            loginadmin.Enabled = false;
            MaximizeBox = false;
        }
        bool Login()
        {

            sql.Param("@usr", txtid.Text);
            sql.query("select count(*) from tbluser where usr=@usr");
            if ((int)sql.dt.Rows[0][0] == 1)
            {
                return true;
                
            }
            MessageBox.Show("Invalid ID", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Login() == true)
            {
                label1.Text = txtid.Text;
                loginadmin.Enabled = true;
            }

        }

        private void loginadmin_Click(object sender, EventArgs e)
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
                loginadmin.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please Confirm New Password.");
            }
        }
    }
}
