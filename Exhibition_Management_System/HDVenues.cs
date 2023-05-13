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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Exhibition_Management_System
{
    public partial class HDVenues : Form
    {
        public HDVenues()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");

        private void HDVenues_Load(object sender, EventArgs e)
        {
            GetVenuesRecord();

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

        private void GetVenuesRecord()
        {
            SqlCommand cmd = new SqlCommand("select * from VEnueDataTable", con);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
        }


        string connectionString = "Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True";
        private DataTable SearchData(string searchText)
        {
            // Create a new SQL connection and command.
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                // Set the SQL command text to retrieve data from a table called "MyTable".
                command.CommandText = "SELECT * FROM VenueDataTable WHERE VenueName LIKE @searchText";

                // Add the search parameter to the command.
                command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                // Set the command's connection.
                command.Connection = connection;

                // Create a new data adapter and fill a DataTable with the results of the query.
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text;
            DataTable results = SearchData(searchText);
            dataGridView1.DataSource = results;
        }
    }
}
