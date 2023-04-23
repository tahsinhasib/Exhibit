using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exhibition_Management_System
{
    public partial class UserDashboard : Form
    {
        public UserDashboard(string passedfromlogin)
        {
            InitializeComponent();
            string labelname = passedfromlogin;
            label2.Text= labelname;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button3.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button5.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
            button6.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));

            panelChildForm.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panelChildForm.Width, panelChildForm.Height, 40, 40));
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
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pass1 = label2.Text;
            openChildForm(new FindEvent(pass1));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pass1 = label2.Text;
            openChildForm(new UDRegisteredEvents(pass1));
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
