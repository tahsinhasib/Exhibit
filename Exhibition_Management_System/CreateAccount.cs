using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exhibition_Management_System
{
    public partial class CreateAccount : Form
    {
        Boolean ischange = false;
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            if(email.Contains("@") && email.Contains("."))
            {
                lblEmailRight.Visible= true;
                lblEmailCross.Visible = false;
            }
            else
            {
                lblEmailCross.Visible = true;
                lblEmailRight.Visible = false;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(textBox4.Text != textBox2.Text)
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string number = textBox5.Text;
            if(number.StartsWith("01") && (number.Length == 11))
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Login login = new Login();
            login.ShowDialog();
        }

        private void lblRight_Click(object sender, EventArgs e)
        {

        }
    }
}
