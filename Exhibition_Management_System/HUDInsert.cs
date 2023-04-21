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
    public partial class HUDInsert : Form
    {
        Boolean ischange = false;
        public HUDInsert()
        {
            InitializeComponent();
        }

        private void HUDInsert_Load(object sender, EventArgs e)
        {
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != textBox2.Text)
            {
                lblPassCross.Visible = true;
                lblPassRight.Visible = false;
            }
            else
            {
                lblPassCross.Visible = false;
                lblPassRight.Visible = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" &&
                textBox2.Text != "" &&
                lblPassRight.Visible == true &&
                lblEmailRight.Visible == true &&
                richTextBox1.Text != "" &&
                lblPhnRight.Visible == true &&
                comboBox1.SelectedItem != null &&
                comboBox2.SelectedItem != null)
            {
                string uname = textBox1.Text;
                string password = textBox2.Text;
                string email = textBox3.Text;
                string add = richTextBox1.Text;
                string phone = textBox5.Text; ;
                string acc = comboBox1.SelectedItem.ToString();
                string gen = comboBox2.SelectedItem.ToString();

                SqlConnection con = null;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "insert into UserDataTable (Username, Password, Phone, Email, Gender, Account, Address) VALUES ('" + uname + "','" + password + "','" + phone + "','" + email + "','" + gen + "','" + acc + "','" + add + "')";
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
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("Fields are empty! or Invalid input", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}
