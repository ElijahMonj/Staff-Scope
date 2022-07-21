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
using loginTutorial.Properties;

namespace loginTutorial
{
    public partial class leave : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public leave()
        {
            InitializeComponent();
            DisplayData();
        }

        private void leave_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;

        }
       

            //Insert Data  

            //Display Data in DataGridView  
            private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tblLeave", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            label2.Text = "";
           
            ID = 0;
        }
      
        
        private void leave_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is logrecords)
                {
                    frm.Show();
                }
            }
        }

        private void btn_Delete_Click_1(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete tblLeave where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void btn_Update_Click_1(object sender, EventArgs e)
        {

            if (box1.Text != "" && label2.Text !="")
            {
                cmd = new SqlCommand("update tblLeave set Tag=@date where ID=@id", con);
                con.Open();
                cmd.Parameters.Add("@id", ID);
                cmd.Parameters.Add("@date", box1.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record and a tag to update.");
            }
        }

        private void dataGridView1_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            label2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void box1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // label31.Text = box1.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
