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
    public partial class staff : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        public staff()
        {
            InitializeComponent();
        }

        private void staff_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label38.Text = DateTime.Now.ToShortTimeString();
            MaximizeBox = false;
            label30.Text = DateTime.Now.ToShortDateString();
            

            label1.Text=Class1.uname;
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string sqlSelectQuery = "select * from tbluser where usr ='" + label1.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label35.Text = dr["fname"].ToString();
                
                label9.Text = dr["lname"].ToString();
                label24.Text = dr["fname"].ToString();

                label25.Text = dr["lname"].ToString();
                label8.Text = dr["usr"].ToString();
                label3.Text = dr["designation"].ToString();
                label4.Text = dr["schedin"].ToString();
                label42.Text = dr["schedout"].ToString();

            }
           

            
            if (Check() == true)
            {
                button13.Enabled = false;
                button12.Enabled = true;
            } else if (Check() == false)
            {
                button12.Enabled = false;
                button13.Enabled = true;
            }
            button7.Enabled = false;
            button8.Enabled = false;
            button5.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = false;

            button_mornstart();

        }
      
        void refresh()
        {
            if (Check() == true)
            {
                button13.Enabled = false;
                button12.Enabled = true;
            }
            else if (Check() == false)
            {
                button12.Enabled = false;
                button13.Enabled = true;
            }
        }

        bool Check()
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string sqlSelectQuery = "select * from tblLog where Employee_ID ='" + label1.Text + "' and Date='" + label30.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label41.Text = dr["Date"].ToString();
                
                return true;
            }
            return false;

        }

        private void staff_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show(
                 "Are you sure you want to log out?",
               "Log Out",
                
                MessageBoxButtons.YesNo);

            e.Cancel = (window == DialogResult.No);


          

        }

        private void button13_Click(object sender, EventArgs e)
        {
            

            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();

                cmd = new SqlCommand("INSERT INTO [tblLog] (Employee_ID,Lastname,Firstname,Designation,Time_In,Date) "
                    + "VALUES(@emid,@lastname,@firstname,@designation,@timein,@date)", con);

                cmd.Parameters.Add("@emid", label1.Text);
                cmd.Parameters.Add("@lastname", label9.Text);
                cmd.Parameters.Add("@firstname", label35.Text);
                cmd.Parameters.Add("@designation", label3.Text);
                cmd.Parameters.Add("@timein", label38.Text);
                cmd.Parameters.Add("@date", label30.Text);




                int i = cmd.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    MessageBox.Show("Log Saved");
                }
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            refresh();
            button_mornstart();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sum();
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Time_Out=@mstart, Log_in=@in, Log_out=@out WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mstart", label38.Text);
                
                cmd.Parameters.Add("@in", label23.Text);
                cmd.Parameters.Add("@out", label21.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data saved");

                button_mornstart();
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            


        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Morning_Break_Start=@mstart WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mstart", label38.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button_mornstart();
                MessageBox.Show("Data saved");
                
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        void button_mornstart()
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string sqlSelectQuery = "select * from tblLog where Employee_ID ='" + label1.Text + "'and Date ='" + label30.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label28.Text = dr["Morning_Break_Start"].ToString();
                label7.Text = dr["Morning_Break_End"].ToString();
                label16.Text = dr["Lunch_Break_Start"].ToString();
                label14.Text = dr["Lunch_Break_End"].ToString();
                label20.Text = dr["Afternoon_Break_Start"].ToString();
                label18.Text = dr["Afternoon_Break_End"].ToString();
                label21.Text = dr["Log_out"].ToString();
                label23.Text = dr["Log_in"].ToString();
                timein.Text = dr["Time_In"].ToString();
                timeout.Text = dr["Time_Out"].ToString();

            }
            if (timeout.Text != "")
            {
                button7.Enabled = false;
                button8.Enabled = false;
                button5.Enabled = false;
                button4.Enabled = false;
                button6.Enabled = false;
                button3.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;

            }
            else
            {
                if (label28.Text == "" && label41.Text != "")
                {
                    button7.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;

                }
                if (label28.Text != "" && label7.Text == "")
                {
                    button7.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button8.Enabled = true;
                }
                if (label28.Text != "" && label7.Text != "")
                {
                    button8.Enabled = false;

                }
                if (label7.Text != "")
                {
                    button5.Enabled = true;
                    button6.Enabled = true;
                }
                if (label16.Text != "" && label14.Text == "")
                {
                    button7.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button8.Enabled = false;
                    button4.Enabled = true;
                    button3.Enabled = false;
                }
                if (label14.Text != "")
                {
                    button7.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = true;
                    button8.Enabled = false;
                    button4.Enabled = false;
                    button3.Enabled = false;
                }
                if (label20.Text != "" && label18.Text == "")
                {
                    button7.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button8.Enabled = false;
                    button4.Enabled = false;
                    button3.Enabled = true;
                }
                if (label20.Text != "" && label18.Text != "")
                {
                    button7.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button8.Enabled = false;
                    button4.Enabled = false;
                    button3.Enabled = false;
                }
            }
            


            

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Morning_Break_End=@mend WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mend", label38.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button_mornstart();
                MessageBox.Show("Data saved");
               
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Lunch_Break_Start=@mend WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mend", label38.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button_mornstart();
                MessageBox.Show("Data saved");

            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Lunch_Break_End=@mend WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mend", label38.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button_mornstart();
                MessageBox.Show("Data saved");
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Afternoon_Break_Start=@mend WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mend", label38.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button_mornstart();
                MessageBox.Show("Data saved");
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the log?", "Confirm Save Log", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("UPDATE tblLog set Afternoon_Break_End=@mend WHERE Employee_ID=@eid AND Date=@dt", con);
                cmd.Parameters.Add("@mend", label38.Text);
                cmd.Parameters.Add("@eid", label1.Text);
                cmd.Parameters.Add("@dt", label30.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button_mornstart();
                MessageBox.Show("Data saved");
            }

            else
            {
                MessageBox.Show("Log Not Saved", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
        }


        private void btnchange_Click(object sender, EventArgs e)
        {
            staff2 a = new staff2();
            this.Hide();
            a.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label38.Text = DateTime.Now.ToShortTimeString();
            timer1.Start();
        }

         void sum()
        {
            String source = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(source);
            con.Open();
            string sqlSelectQuery = "select * from tblLog where Employee_ID ='" + label1.Text + "'and Date ='" + label30.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlSelectQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                timein.Text = dr["Time_In"].ToString();

                timeout.Text = dr["Time_Out"].ToString();

                timeout.Text = DateTime.Now.ToShortTimeString();
            }
            try
            {
                DateTime date1 = Convert.ToDateTime(timein.Text);
                DateTime date2 = Convert.ToDateTime(label4.Text);


                int login = DateTime.Compare(date1, date2);


                if (login < 0)
                {

                    label23.Text = "Early";
                }

                else if (login == 0)
                {
                    label23.Text = "On Time";
                }

                else
                {
                    label23.Text = "Late";
                }


                DateTime date3 = Convert.ToDateTime(timeout.Text);
                DateTime date4 = Convert.ToDateTime(label42.Text);
                int logout = DateTime.Compare(date3, date4);

                if (logout < 0)
                {

                    label21.Text = "Undertime";
                }

                else if (logout == 0)
                {

                    label21.Text = "On Time";
                }

                else
                {

                    label21.Text = "Overtime";
                }
            }
            catch (Exception e)
            {
               

                MessageBox.Show("You still do not have specified schedule for your account! Please refer to the admin to set a schedule for you.", "Warning!",
     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string leave;
    
            if (MessageBox.Show("Do you want to propose this date for leave?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("INSERT into tblLeave (EmployeeID,Date) "
                        + "VALUES(@eid,@date)", con);
                cmd.Parameters.Add("eid", label1.Text);
                cmd.Parameters.Add("date", dateTimePicker1.Value.ToLongDateString());

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Selected date successfully proposed.");
            }

            else
            {
                MessageBox.Show("Leave not proposed.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void staff_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form1)
                {
                    frm.Show();
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
