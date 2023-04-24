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
    public partial class AdminPayment : Form
    {
        public AdminPayment()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void AdminPayment_Load(object sender, EventArgs e)
        {
            GetPaymentRecord();
        }

        private void GetPaymentRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PaymentTable", con);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }
    }
}
