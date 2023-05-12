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
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 40, 40));

        }

        /*A method signature for an external method call to the Windows GDI (Graphics Device Interface) 
         * library function "CreateRoundRectRgn" The CreateRoundRectRgn method is used to create a region 
         * with rounded corners, which can be used to define the shape of a window, a button, or any other 
         * control in a graphical user interface. The parameters to this function specify the coordinates 
         * of a rectangle and the size of the ellipse that defines the curvature of the corners.
         * 
         * The DllImport attribute is used to indicate that this method is implemented in an external 
         * library (Gdi32.dll in this case) and that the method signature should be marshaled to and from 
         * unmanaged code. The EntryPoint parameter specifies the name of the function in the external 
         * library that corresponds to this method.
         * 
         * The IntPtr return type indicates that the method returns a handle to the region that is created, 
         * which can be used in other GDI functions to manipulate the region.
         */

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


        private void button1_Click_1(object sender, EventArgs e)
        {
            String u_name;
            String u_pass;
            String ACC;

            try
            {
                String query = "SELECT * FROM UserDataTable WHERE Username = '" + textBox1.Text + "' AND Password = '" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                String query2 = "SELECT Account FROM UserDataTable WHERE Username = '" + textBox1.Text + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
                DataTable dtable2 = new DataTable();
                sda.Fill(dtable2);
                ACC = dtable2.Rows[0]["Account"].ToString();                //this one determines wether to launch user or host dashboard

                if (textBox1.Text != "" || textBox2.Text != "")
                {
                    if (dtable.Rows.Count > 0)
                    {
                        u_name = textBox1.Text;
                        u_pass = textBox2.Text;

                        MessageBox.Show("Login Successful ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (ACC.Equals("User"))
                        {
                            this.Hide();
                            UserDashboard udb = new UserDashboard(u_name);
                            udb.ShowDialog();
                        }
                        else if (ACC.Equals("Host"))
                        {
                            this.Hide();
                            HostDashboard hdb = new HostDashboard(u_name);
                            hdb.ShowDialog();
                        }
                        else
                        {
                            this.Hide();
                            AppOwnerDashboard apd = new AppOwnerDashboard(u_name);
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
                MessageBox.Show("Invalid Credentials!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
            finally
            {
                con.Close();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
