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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Exhibition_Management_System
{
    public partial class AdminVenueData : Form
    {
        public AdminVenueData()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
        public string VenID;
        private void AdminVenueData_Load(object sender, EventArgs e)
        {
            GetVenueDataRecord();

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 40, 40));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 40, 40));
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

        private void GetVenueDataRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM VenueDataTable", con);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text != "" &&
                richTextBox2.Text != "")
            {
                string id = richTextBox1.Text;
                string name = richTextBox2.Text;

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "INSERT INTO VenueDataTable (VenueID, VenueName) VALUES ('" + id + "','" + name + "')";
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
                    MessageBox.Show("Succesfully registered!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetVenueDataRecord();
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                }
            }
            else
            {
                MessageBox.Show("Fields are empty! or Invalid input", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            VenID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            richTextBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            richTextBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text != "" &&
                richTextBox2.Text != "") 
            {
                string venid = richTextBox1.Text;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "UPDATE VenueDataTable SET VenueName = @VenueName WHERE VenueID = '" + venid + "'";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@VenueName", richTextBox2.Text);
                        cmd.Parameters.AddWithValue("@VenueID", venid);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("Venue updated!", "Updated!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    GetVenueDataRecord();
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please select to update information!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" &&
                richTextBox2.Text != "")
            {
                string venid = richTextBox1.Text;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "DELETE FROM VenueDataTable WHERE VenueID = '" + venid + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Venue deleted!", "Deleted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    GetVenueDataRecord();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select to delete information!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            GetVenueDataRecord();
        }
    }
}
