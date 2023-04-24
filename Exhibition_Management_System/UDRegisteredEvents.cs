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
    public partial class UDRegisteredEvents : Form
    {
        public string pass2;
        public UDRegisteredEvents(string recievepass1)
        {
            InitializeComponent();
            //catches the constructor pass from UserDashboard to UDRegisteredEvents
            pass2 = recievepass1;           
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void UDRegisteredEvents_Load(object sender, EventArgs e)
        {
            GetRegisteredEventsData();
        }

        private void GetRegisteredEventsData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM RegisteredEventsDataTable WHERE Username = @Username", con);
            cmd.Parameters.AddWithValue("@Username", pass2);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }
    }
}
