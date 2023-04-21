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

namespace Exhibition_Management_System
{
    public partial class HUDUpdate : Form
    {
        public Boolean ischange = false;
        public HUDUpdate()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
        private void HUDUpdate_Load(object sender, EventArgs e)
        {
            GetRecord();

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

        private void GetRecord()
        {
            SqlCommand cmd = new SqlCommand("select Username from UserDataTable", con);

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" &&
               lblEmailRight.Visible == true &&
               textBox4.Text != "" &&
               lblPhnRight.Visible == true &&
               textBox7.Text != "")
            {
                string update = textBox1.Text;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "UPDATE UserDataTable SET Email = @Email, Address = @Address, Phone = @Phone, Gender = @Gender WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Phone", textBox5.Text);
                        cmd.Parameters.AddWithValue("@Gender", textBox7.Text);
                        cmd.Parameters.AddWithValue("@Username", update);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    MessageBox.Show("User updated!", "Updated!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    GetRecord();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Please select to delete information!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            if (email.Contains("@") && email.Contains("."))
            {
                lblEmailRight.Visible = true;
                lblEmailCross.Visible = false;
            }
            else
            {
                lblEmailCross.Visible = true;
                lblEmailRight.Visible = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string number = textBox5.Text;
            if (number.StartsWith("01") && (number.Length == 11))
            {
                lblPhnRight.Visible = true;
                lblPhnCross.Visible = false;
            }
            else
            {
                lblPhnRight.Visible = false;
                lblPhnCross.Visible = true;
            }
        }
    }
}
