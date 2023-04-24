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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Exhibition_Management_System
{
    public partial class UDRatings : Form
    {
        public string user;
        public UDRatings(string s1)
        {
            user = s1;
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            if ((radioButton1.Checked ||
               radioButton2.Checked ||
               radioButton3.Checked ||
               radioButton4.Checked) &&
               richTextBox1.Text != "" &&
               textBox1.Text != "" &&
               textBox1.Text != "")
            {
                string vname = textBox1.Text;
                string evname = textBox2.Text;
                string rate = "";

                if(radioButton1.Checked)
                {
                    rate = radioButton1.Text;
                }
                if(radioButton2.Checked)
                {
                    rate = radioButton2.Text;
                }
                if(radioButton3.Checked)
                {
                    rate = radioButton3.Text;
                }
                if(radioButton4.Checked)
                {
                    rate = radioButton4.Text;
                }

                string review = richTextBox1.Text;

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "INSERT INTO ReviewDataTable (Venue, Event, Username, Rating, Comment) VALUES ('" + vname + "','" + evname + "','" + user + "','" + rate + "','" + review + "')";
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
                    MessageBox.Show("Thanks for your review!", "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Fields are empty! or Invalid input", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void UDRatings_Load(object sender, EventArgs e)
        {
            GetRegisteredEventsRecord();

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
        }

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

        private void GetRegisteredEventsRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT VenueName, Event, Username, Date, Time FROM RegisteredEventsDataTable where Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", user);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }
    }
}
