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
    public partial class HostandUserData : Form
    {
        public HostandUserData()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HostandUserData_Load(object sender, EventArgs e)
        {
            GetostandUserDataRecord();
        }

        private void GetostandUserDataRecord()
        {
            SqlCommand cmd = new SqlCommand("select * from UserDataTable", con);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HUDInsert a = new HUDInsert();
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HUDRemove b = new HUDRemove();
            b.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HUDUpdate c = new HUDUpdate();
            c.ShowDialog();
        }
    }
}
