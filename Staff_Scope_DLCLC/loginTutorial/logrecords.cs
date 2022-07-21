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
using System.Configuration;


using System.Net;
using System.Data.Sql;


namespace loginTutorial
{
    public partial class logrecords : Form
    {

        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;

        public logrecords()
        {
            InitializeComponent();
           
        }
        

        private void loginadmin_Click(object sender, EventArgs e)
        {
            change ch = new change();
            this.Hide();
            ch.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registerForm rf = new registerForm();
            this.Hide();
            rf.ShowDialog();
        }

        private void logrecords_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form1)
                {
                    frm.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset rt = new reset();
            this.Hide();
            rt.ShowDialog();
        }

        private void logrecords_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            //SqlDataAdapter da = new SqlDataAdapter("SELECT Employee_ID,Firstname,Lastname,Date FROM tblLog", @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Elijah\source\repos\loginTutorial\loginTutorial\dbtest.mdf; Integrated Security = True");
            //DataSet ds = new DataSet();
            //da.Fill(ds, "tblLog");
            //dataGridView1.DataSource = ds.Tables["tblLog"].DefaultView; 
            con = new SqlConnection(cs);
            con.Open();
            adapt = new SqlDataAdapter("SELECT Employee_ID,Firstname,Lastname,Date FROM tblLog", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            staffsched ss = new staffsched();
            this.Hide();
            ss.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            leave lv = new leave();
            this.Hide();
            lv.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wholetable wt = new wholetable();
            this.Hide();
            wt.ShowDialog();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           test.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            fname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            lname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            date.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string sqlSelectQuery = "select * from tblLog where Date ='" + date.Text + "'and Employee_ID ='" + test.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label1.Text = dr["Designation"].ToString();
                label2.Text = dr["Time_In"].ToString();
                    label4.Text = dr["Log_in"].ToString();
                label6.Text = dr["Log_out"].ToString();
                label3.Text = dr["Time_Out"].ToString();
                label11.Text = dr["Morning_Break_Start"].ToString();
                label9.Text = dr["Morning_Break_End"].ToString();

                label10.Text = dr["Lunch_Break_Start"].ToString();
                label8.Text = dr["Lunch_Break_End"].ToString();

                label13.Text = dr["Afternoon_Break_Start"].ToString();
                label12.Text = dr["Afternoon_Break_End"].ToString();
            }



        }

        private void test_Click(object sender, EventArgs e)
        {

        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            con.Open();
            adapt = new SqlDataAdapter("select Employee_ID,Firstname,Lastname,Date from tblLog where Firstname like '" + searchbox.Text + "%' OR Employee_ID like '" + searchbox.Text + "%' OR Lastname like '" + searchbox.Text + "%' OR Date like '" + searchbox.Text + "%'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
