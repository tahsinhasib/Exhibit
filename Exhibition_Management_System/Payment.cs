using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace Exhibition_Management_System
{
    public partial class Payment : Form
    {
        public string venid;
        public string venname;
        public string evname;
        public string username;
        public string fees;
        public string date;
        public string time;

        public string FEES;
        public string VAT;
        public string TOTAL;
        public Payment(string s1,
                       string s2,
                       string s3,
                       string s4,
                       string s5,
                       string s6,
                       string s7)
        {
            venid = s1;
            venname = s2;
            evname = s3;
            username = s4;
            fees = s5;
            date = s6;
            time = s7;

           
            InitializeComponent();

            textBox4.Text = fees;
            textBox5.Text = "500";

            FEES = textBox4.Text;
            VAT = textBox5.Text;

            /*
             * For converting the string type amount to integer type for
             * addition operation. Later the integer result is converted
             * to string and displayed in the TextBox6
            */
            if (int.TryParse(FEES, out int num1) && int.TryParse(VAT, out int num2))
            {
                int result = num1 + num2;
                TOTAL = result.ToString();
                textBox6.Text = TOTAL.ToString();
            }


            SqlConnection con = null;

            try
            {
                con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                con.Open();

                string query1 = "SELECT Email FROM UserDataTable WHERE Username = '" + username + "'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                DataSet ds1 = new DataSet();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(ds1);
                DataTable dt1 = ds1.Tables[0];
                string Email = dt1.Rows[0]["Email"].ToString();
                textBox1.Text = Email;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            } 

        }

        private void Payment_Load(object sender, EventArgs e)
        {

            panel2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 40, 40));
            panel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width, panel3.Height, 40, 40));
            panel4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel4.Width, panel4.Height, 40, 40));

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
        }

        //for rounding the edges of panels and buttons
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" &&
               textBox2.Text != "" &&
               textBox3.Text != "" &&
               textBox4.Text != "" &&
               textBox5.Text != "" &&
               textBox6.Text != "" &&
               textBox7.Text != "" &&
               (radioButton1.Checked ||
                radioButton2.Checked ||
                radioButton3.Checked ||
                radioButton4.Checked))
            {
                SqlConnection con = null;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();
                    string query = "INSERT INTO RegisteredEventsDataTable (VenueID, VenueName, Event, Username, Fees, Date, Time) VALUES ('" + venid + "','" + venname + "','" + evname + "','" + username + "','" + fees + "','" + date + "','" + time + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                    MessageBox.Show("Event planned!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Hide();


                //for storing into PaymentTable
                SqlConnection con2 = null;
                try
                {
                    con2 = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con2.Open();
                    string query2 = "INSERT INTO PaymentTable (Username, Email, Venue, Event, Date, Amount, Card, Holder) VALUES ('" + username + "','" + textBox1.Text + "','" + venname + "','" + evname + "','" + textBox7.Text + "','" + textBox6.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con2.Close();
                    MessageBox.Show("Thank you for your payment!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Fields are empty! or Invalid input", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox7.Text = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");
            textBox7.SelectionStart = textBox7.Text.Length;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
