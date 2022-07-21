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
using System.Data;
using System.Net;
using System.Data.Sql;

namespace loginTutorial
{
    public partial class change : Form
    {
        public change()
        {
            InitializeComponent();
        }

        private void change_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Close();
          
        }

        private void loginadmin_Click(object sender, EventArgs e)
        {
            if (txt1.Text == txt2.Text)
            {
            
               SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("update tbladmin set password='" + txt1.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Password Succesfully Changed");
            }
            else
            {
                MessageBox.Show("Please Confirm New Password.");
            }
        }

        private void change_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is logrecords)
                {
                    frm.Show();
                }
            }
        }
    }
}
