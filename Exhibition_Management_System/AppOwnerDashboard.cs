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
    public partial class AppOwnerDashboard : Form
    {
        public string temp;
        public AppOwnerDashboard(string passedfromlogin)
        {
            InitializeComponent();
            temp = passedfromlogin;
            label2.Text = temp;

            CountVenues();
            CountEvents();
            CountHosts();
            CountGuests();
            CountEarnings();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void CountVenues()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM VenueDataTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label7.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountEvents()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM EventDataTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label8.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountHosts()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM UserDataTable WHERE Account = '" + "Host" + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label9.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountGuests()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM UserDataTable WHERE Account = '" + "User" + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label10.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountEarnings()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(CONVERT(decimal(10,2), Amount)) AS TotalMoney FROM PaymentTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label11.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        public void RefreshLables()
        {
            CountVenues();
            CountEvents();
            CountHosts();
            CountGuests();
            CountEarnings();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminReview());
        }

        private void AppOwnerDashboard_Load(object sender, EventArgs e)
        {
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 40, 40));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 40, 40));
            button4.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button4.Width, button4.Height, 40, 40));
            button5.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button5.Width, button5.Height, 40, 40));
            button6.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button6.Width, button6.Height, 40, 40));

            panel3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel3.Width, panel3.Height, 40, 40));
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

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm= childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle= FormBorderStyle.None;
            childForm.Dock= DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag= childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminProfile(temp));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new HostandUserData());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminVenueData());
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new AdminPayment());
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            RefreshLables();
        }
    }
}
