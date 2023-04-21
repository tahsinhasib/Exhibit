using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exhibition_Management_System
{
    public partial class HUDRemove : Form
    {
        public HUDRemove()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                string remove = textBox1.Text;
                try
                {
                    con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                    con.Open();

                    string query = "DELETE FROM UserDataTable WHERE Username = '" + remove + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User removed!", "Removed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    GetRecord();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select to delete information!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void HUDRemove_Load(object sender, EventArgs e)
        {
            GetRecord();

            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 40, 40));
        }

        //This part of the code is for designing of the button
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

        private void GetRecord()
        {
            SqlCommand cmd = new SqlCommand("select * from UserDataTable", con);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }
    }
}
