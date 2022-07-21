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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace loginTutorial
{
    public partial class wholetable : Form
    {
        public wholetable()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt; int ID = 0;
        DataTable dt;
        private void wholetable_Load(object sender, EventArgs e)
        {
            DisplayData();
        }
        private void DisplayData()
        {
            con.Open();
            dt = new DataTable();
            adapt = new SqlDataAdapter("select * from tblLog", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete tblLog where LogID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ID = 0;
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void wholetable_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is logrecords)
                {
                    frm.Show();
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    xlWorkSheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 2, j + 1] = cell.Value;
                    }
                }

                string datenow = DateTime.Now.ToLongDateString();
                string name = "Log Record ";
                string lr = name + datenow;
                xlWorkBook.SaveAs(lr, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Log Record file has been generated, inside the machine's document folder.");
            }
            catch (Exception test)
            {
                MessageBox.Show("An error occured when creating an Excel file. The Microsoft Excel may not be found, the Staff Scope will create a PDF file instead.", "Notice");
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF (*.pdf)|*.pdf";
                    sfd.FileName = "Output.pdf";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                File.Delete(sfd.FileName);
                            }
                            catch (IOException xd)
                            {
                                fileError = true;
                                MessageBox.Show("It wasn't possible to write the data to the disk." + xd.Message);
                            }
                        }
                        if (!fileError)
                        {
                            try
                            {
                                PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                                pdfTable.DefaultCell.Padding = 3;
                                pdfTable.WidthPercentage = 100;
                                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                foreach (DataGridViewColumn column in dataGridView1.Columns)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                    pdfTable.AddCell(cell);
                                }

                                foreach (DataGridViewRow row in dataGridView1.Rows)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }

                                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                                {
                                    Document pdfDoc = new Document(PageSize._11X17.Rotate());
                                    PdfWriter.GetInstance(pdfDoc, stream);
                                    pdfDoc.Open();
                                    pdfDoc.Add(pdfTable);
                                    pdfDoc.Close();
                                    stream.Close();
                                }

                                MessageBox.Show("Data Exported Successfully.", "Info");
                            }
                            catch (Exception xd)
                            {
                                MessageBox.Show("Error :" + xd.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Record To Export.", "Info");
                }
            }
            


        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error occured exporting into Excel file. Creating PDF file instead. " + ex.ToString());
                
            }
            finally
            {
                GC.Collect();
            }
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
            con.Open();
            
            adapt = new SqlDataAdapter("select * from tblLog where Firstname like '" + searchbox.Text + "%' OR Employee_ID like '" + searchbox.Text + "%' OR Lastname like '" + searchbox.Text + "%' OR Date like '" + searchbox.Text + "%'", con);


           


            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string month = box1.Text;
            string week = box2.Text;

            
            

            if (month == "Jan")
            {
                Class1.datepicker = "1";
               
            }
            else if (month == "Feb")
            {
                Class1.datepicker = "2";
               
            }
            else if (month == "Mar")
            {
                Class1.datepicker = "3";
            }
            else if (month == "April")
            {
                Class1.datepicker = "4";
            }
            else if (month == "May")
            {
                Class1.datepicker = "5";
            }
            else if (month == "June")
            {
                Class1.datepicker = "6";
            }
            else if (month == "July")
            {
                Class1.datepicker = "7";
            }
            else if (month == "Aug")
            {
                Class1.datepicker = "8";
            }
            else if (month == "Sept")
            {
                Class1.datepicker = "9";
            }
            else if (month == "Oct")
            {
                Class1.datepicker = "10";
            }
            else if (month == "Nov")
            {
                Class1.datepicker = "11";
            }
            else if (month == "Dec")
            {
                Class1.datepicker = "12";
            }


            //string d0="",d1="",d2 = "", d3 = "", d4 = "", d5 = "", d6 = "", d7 = "", d8 = "", d9 = "", d10 = "", d11 = "", d12 = "", d13 = "", d14 = "", d15 = "";

            if (week == "First and second week")
            {
              
                string dd0 = "/0/";
                string dd1 = "/1/";
                string dd2 = "/2/";
                string dd3 = "/3/";
                string dd4 = "/4/";
                string dd5 = "/5/";
                string dd6 = "/6/";
                string dd7 = "/7/";
                string dd8 = "/8/";
                string dd9 = "/9/";
                string dd10 = "/10/";
                string dd11 = "/11/";
                string dd12 = "/12/";
                string dd13 = "/13/";
                string dd14 = "/14/";
                string dd15 = "/15/";

                
                string ddate0 = Class1.datepicker + dd0;
                string ddate1 = Class1.datepicker + dd1;
                string ddate2 = Class1.datepicker + dd2;
                string ddate3 = Class1.datepicker + dd3;
                string ddate4 = Class1.datepicker + dd4;
                string ddate5 = Class1.datepicker + dd5;
                string ddate6 = Class1.datepicker + dd6;
                string ddate7 = Class1.datepicker + dd7;
                string ddate8 = Class1.datepicker + dd8;
                string ddate9 = Class1.datepicker + dd9;
                string ddate10 = Class1.datepicker + dd10;
                string ddate11= Class1.datepicker + dd11;
                string ddate12 = Class1.datepicker + dd12;
                string ddate13 = Class1.datepicker + dd13;
                string ddate14 = Class1.datepicker + dd14;
                string ddate15 = Class1.datepicker + dd15;


                
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                adapt = new SqlDataAdapter("select * from tblLog where Date like '" + ddate0 + "%' OR Date like '" + ddate1 + "%' OR Date like '" + ddate2 + "%' OR Date like '" + ddate3 + "%' OR Date like '" + ddate4 + "%' OR Date like '" + ddate5 + "%' OR Date like '" + ddate6 + "%' OR Date like '" + ddate7 + "%' OR Date like '" + ddate8 + "%' OR Date like '" + ddate9 + "%' OR Date like '" + ddate10 + "%' OR Date like '" + ddate11 + "%' OR Date like '" + ddate12 + "%' OR Date like '" + ddate13 + "%' OR Date like '" + ddate14 + "%' OR Date like '" + ddate15 + "%'", con);

                //adapt = new SqlDataAdapter("select * from tblLog where Date like '" + ddate2 + "%'", con);

                dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }
            if (week == "Third and last week")
            {
                
                string d0 = "/16/";
                string d1 = "/17/";
                string d2 = "/18/";
                string d3 = "/19/";
                string d4 = "/20/";
                string d5 = "/21/";
                string d6 = "/22/";
                string d7 = "/23/";
                string d8 = "/24/";
                string d9 = "/25/";
                string d10 = "/26/";
                string d11 = "/27/";
                string d12 = "/28/";
                string d13 = "/29/";
                string d14 = "/30/";
                string d15 = "/31/";

                string date0 = Class1.datepicker + d0;
                string date1 = Class1.datepicker + d1;
                string date2 = Class1.datepicker + d2;
                string date3 = Class1.datepicker + d3;
                string date4 = Class1.datepicker + d4;
                string date5 = Class1.datepicker + d5;
                string date6 = Class1.datepicker + d6;
                string date7 = Class1.datepicker + d7;
                string date8 = Class1.datepicker + d8;
                string date9 = Class1.datepicker + d9;
                string date10 = Class1.datepicker + d10;
                string date11 = Class1.datepicker + d11;
                string date12 = Class1.datepicker + d12;
                string date13 = Class1.datepicker + d13;
                string date14 = Class1.datepicker + d14;
                string date15 = Class1.datepicker + d15;
                


                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dbtest.mdf;Integrated Security=True");
                con.Open();
                adapt = new SqlDataAdapter("select * from tblLog where Date like '" + date0 + "%' OR Date like '" + date1 + "%' OR Date like '" + date2 + "%' OR Date like '" + date3 + "%' OR Date like '" + date4 + "%' OR Date like '" + date5 + "%' OR Date like '" + date6 + "%' OR Date like '" + date7 + "%' OR Date like '" + date8 + "%' OR Date like '" + date9 + "%' OR Date like '" + date10 + "%' OR Date like '" + date11 + "%' OR Date like '" + date12 + "%' OR Date like '" + date13 + "%' OR Date like '" + date14 + "%' OR Date like '" + date15 + "%'", con);


                //adapt = new SqlDataAdapter("select * from tblLog where Date like '" + date0 + "%' OR Date like '" + date1 + "%'", con);


                dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            }

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
