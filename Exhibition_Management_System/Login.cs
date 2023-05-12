using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Exhibition_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

       

        //Setting up SQL connection
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
            // Works as a button that takes to title page
            this.Dispose();
            Title title = new Title();
            title.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Sets the value for roundness
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 40, 40));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 40, 40));

            panel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width, panel3.Height, 40, 40));



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
         * 
         * Responsible for making the outlines rounded for buttons and panels.
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
            String u_name;      // Stores the username provided in the textbox
            String u_pass;      // Stores the user password provided in the textbox
            String ACC;         // The type which takes a user to different dashboard

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

                // If the textboxes are not empty
                if (textBox1.Text != "" || textBox2.Text != "")
                {
                    /* The conditional expression dtable.Rows.Count > 0 evaluates to true if there 
                     * is at least one row in the dtable, and false if there are no rows.
                     */
                    if (dtable.Rows.Count > 0)
                    {
                        u_name = textBox1.Text;
                        u_pass = textBox2.Text;

                        MessageBox.Show("Login Successful ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (ACC.Equals("User"))
                        {
                            //Takes the person to User Dashboard
                            this.Hide();
                            UserDashboard udb = new UserDashboard(u_name);
                            udb.ShowDialog();
                        }
                        else if (ACC.Equals("Host"))
                        {
                            //Takes the person to Host Dashboard
                            this.Hide();
                            HostDashboard hdb = new HostDashboard(u_name);
                            hdb.ShowDialog();
                        }
                        else
                        {
                            //Takes the person to Admin Dashboard
                            this.Hide();
                            AppOwnerDashboard apd = new AppOwnerDashboard(u_name);
                            apd.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                        
                        //Clears the textbox and places the cursor on textbox1 for reuse
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                }
                else
                {
                    //Displays the message if fields are empty, any one of them
                    MessageBox.Show("Fields are empty!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                //Anything other than fields are empty, can be a mix of empty and filled
                MessageBox.Show("Invalid Credentials!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
            }
            finally
            {
                con.Close();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // This line of code enables the user to show password and hide password
            if(checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ForgetPassword fgp = new ForgetPassword();
            fgp.ShowDialog();
        }
    }
}
