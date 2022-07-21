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
namespace loginTutorial
{
    public partial class staffsched : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        dbcontrol sql = new dbcontrol();
        public staffsched()
        {
            InitializeComponent();
        }

        private void staffsched_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;

            button2.Enabled = false;
            timeinpicker.Format = DateTimePickerFormat.Time; 
            timeoutpicker.Format = DateTimePickerFormat.Time;

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
                button2.Enabled = true;
            }
        }

        private void staffsched_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is logrecords)
                {
                    frm.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
            con.Open();
            cmd = new SqlCommand("UPDATE tbluser set schedin=@in,schedout=@out WHERE usr=@id", con);
            cmd.Parameters.Add("in", timeinpicker.Value.ToShortTimeString());
            cmd.Parameters.Add("out", timeoutpicker.Value.ToShortTimeString());
            cmd.Parameters.Add("id", label1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Schedule Succesfully Changed");
        }
    }
}
