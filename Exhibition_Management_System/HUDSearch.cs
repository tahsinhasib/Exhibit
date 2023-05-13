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
    public partial class HUDSearch : Form
    {
        public HUDSearch()
        {
            InitializeComponent();
        }

        private void HUDSearch_Load(object sender, EventArgs e)
        {
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        string connectionString = "Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True";
        private DataTable SearchData(string searchText)
        {
            // Create a new SQL connection and command.
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand())
            {
                // Set the SQL command text to retrieve data from a table called "MyTable".
                command.CommandText = "SELECT * FROM UserDataTable WHERE Username LIKE @searchText";

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
