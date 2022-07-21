using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace loginTutorial
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
        }
        dbcontrol sql = new dbcontrol();
        void register()
        {

            sql.Param("@usr", txtuser.Text);
            sql.Param("@pwd", txtuser.Text);
            sql.Param("@fname", txtfname.Text);
            
            sql.Param("@lname", txtlname.Text);
            sql.Param("@age", txtage.Text);
            sql.query("insert into tbluser (usr,pwd,fname,lname,designation) values (@usr,@pwd,@fname,@lname,@age)");
            if (sql.Check4error(true))
            {
                return;
            }
            MessageBox.Show("Registered! The default password given is the Employee ID.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }


        private void registerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is logrecords)
                {
                    frm.Show();
                }
            }
        }

        private void btncanc_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnreg_Click(object sender, EventArgs e)
        {
            

        }

        private void registerForm_Load(object sender, EventArgs e)
        {
            txtuser.MaxLength = 8;
            MaximizeBox = false;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtuser_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || txtfname.Text == "" || txtlname.Text == "" || txtage.Text == "")
            {

                string message = "Please fill up all the required information of the employee.";
                string title = "Missing Fields";
                MessageBox.Show(message, title);


            }
            else
            {
                register();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtfname_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtlname_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
