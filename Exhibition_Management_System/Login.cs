using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exhibition_Management_System
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            CreateAccount createAccount= new CreateAccount();
            createAccount.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Title title = new Title();
            title.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String u_name;
            String u_pass;


            try
            {
                String query = "SELECT * FROM UserDataTable WHERE Username = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                String acctype = "SELECT [Account] FROM UserDataTable WHERE Username = '" + textBox1.Text + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter(query, con);
                DataTable dtable2 = new DataTable();
                sda2.Fill(dtable2);

                if (textBox1.Text != "" || textBox2.Text != "")
                {
                    if (dtable.Rows.Count > 0)
                    {
                        u_name = textBox1.Text;
                        u_pass = textBox2.Text;

                        MessageBox.Show("Login Successful ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        if("User" == acctype)
                        {
                            this.Hide();
                            UserDashboard udb = new UserDashboard();
                            udb.ShowDialog();
                        }
                        else
                        {
                            AppOwnerDashboard apd = new AppOwnerDashboard();
                            apd.ShowDialog();
                        }
                        
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                        textBox1.Clear();
                        textBox2.Clear();

                        textBox1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Fields are empty!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");

            }
            finally
            {
                con.Close();
            }

        }
    }
}
