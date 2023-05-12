using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Exhibition_Management_System
{
    public partial class ForgetPassword : Form
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        //Setting up SQL connection
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void ForgetPassword_Load(object sender, EventArgs e)
        {
            panel2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 40, 40));
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query2 = "SELECT COUNT(*) FROM UserDataTable WHERE Username = @username";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@username", textBox1.Text);

                int count = (int)cmd2.ExecuteScalar();

                if (count > 0)
                {
                    accfound.Visible = true;
                    accnotfound.Visible = false;
                }
                else
                {
                    accnotfound.Visible = true;
                    accfound.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(accfound.Visible == true &&
                lblmatched.Visible == true &&
                textBox1.Text != "" &&
                textBox2.Text != "" &&
                textBox3.Text != "")
            {
                string update = textBox1.Text;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "UPDATE UserDataTable SET Password = @Password WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Password", textBox3.Text);
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
                    MessageBox.Show("Password updated!", "Updated!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    this.Hide();
                }
            }
            else
            {
                //Anything other than fields are empty, can be a mix of empty and filled
                MessageBox.Show("Invalid Credentials!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox2.Text)
            {
                lblnotmatched.Visible = true;
                lblmatched.Visible = false;
            }
            else
            {
                lblnotmatched.Visible = false;
                lblmatched.Visible = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // This line of code enables the user to show password and hide password
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }
    }
}
