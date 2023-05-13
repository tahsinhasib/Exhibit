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
            // This method displays all the details of VenueDataTable
            GetVenueDataRecord();

            // For making the roundness of buttons
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
                richTextBox2.Text != "" &&
                lblavailable.Visible == true)
            {
                string id = richTextBox1.Text;
                string name = richTextBox2.Text;

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    // For inserting the venues in VenueDataTable
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
                    
                    // Dynamically updates the information in table
                    GetVenueDataRecord();

                    // Clears the textboxes for reuse
                    richTextBox1.Clear();
                    richTextBox2.Clear();

                    // This prevents the label to hide after inserting into the table
                    lblavailable.Visible = false;
                    lbltaken.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Fields are empty! or Invalid input", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This part is used for navigating the data table by clicking onto it
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

                    // This query is used for updating the VenueTableInformation
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
                    lblavailable.Visible = false;
                    lbltaken.Visible = false;
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
            lblavailable.Visible = false;
            lbltaken.Visible = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /*
                 * If atleast one venueID is present in the table then the system will
                 * restrict user to use the same ID for another venue
                 */
                con.Open();
                string query2 = "SELECT COUNT(*) FROM VenueDataTable WHERE VenueID = @VenueID";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@VenueID", richTextBox1.Text);

                int count = (int)cmd2.ExecuteScalar();

                if (count > 0)
                {
                    lbltaken.Visible = true;
                    lblavailable.Visible = false;
                }
                else
                {
                    lblavailable.Visible = true;
                    lbltaken.Visible = false;
                }
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
    }
}
