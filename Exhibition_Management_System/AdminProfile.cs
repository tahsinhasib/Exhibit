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
    public partial class AdminProfile : Form
    {
        public AdminProfile(string Profile)
        {
            InitializeComponent();

            SqlConnection con = null;

            try
            {
                con = new SqlConnection("Data Source=DESKTOP-TGP1F01;Initial Catalog=ExhibitDB;Integrated Security=True");
                con.Open();

                string query1 = "SELECT Username FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                DataSet ds1 = new DataSet();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(ds1);
                DataTable dt1 = ds1.Tables[0];
                string Username = dt1.Rows[0]["Username"].ToString();
                label9.Text = Username;

                string query2 = "SELECT Password FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                DataSet ds2 = new DataSet();
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                adp2.Fill(ds2);
                DataTable dt2 = ds2.Tables[0];
                string Password = dt2.Rows[0]["Password"].ToString();
                label10.Text = Password;

                string query3 = "SELECT Phone FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                DataSet ds3 = new DataSet();
                SqlDataAdapter adp3 = new SqlDataAdapter(cmd3);
                adp3.Fill(ds3);
                DataTable dt3 = ds3.Tables[0];
                string Phone = dt3.Rows[0]["Phone"].ToString();
                label11.Text = Phone;

                string query4 = "SELECT Email FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd4 = new SqlCommand(query4, con);
                DataSet ds4 = new DataSet();
                SqlDataAdapter adp4 = new SqlDataAdapter(cmd4);
                adp4.Fill(ds4);
                DataTable dt4 = ds4.Tables[0];
                string Email = dt4.Rows[0]["Email"].ToString();
                label12.Text = Email;

                string query5 = "SELECT Gender FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd5 = new SqlCommand(query5, con);
                DataSet ds5 = new DataSet();
                SqlDataAdapter adp5 = new SqlDataAdapter(cmd5);
                adp5.Fill(ds5);
                DataTable dt5 = ds5.Tables[0];
                string Gender = dt5.Rows[0]["Gender"].ToString();
                label13.Text = Gender;

                string query6 = "SELECT Account FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd6 = new SqlCommand(query6, con);
                DataSet ds6 = new DataSet();
                SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
                adp6.Fill(ds6);
                DataTable dt6 = ds6.Tables[0];
                string Account = dt6.Rows[0]["Account"].ToString();
                label14.Text = Account;

                string query7 = "SELECT Address FROM UserDataTable WHERE Username = '" + Profile + "'";
                SqlCommand cmd7 = new SqlCommand(query7, con);
                DataSet ds7 = new DataSet();
                SqlDataAdapter adp7 = new SqlDataAdapter(cmd7);
                adp7.Fill(ds7);
                DataTable dt7 = ds7.Tables[0];
                string Address = dt7.Rows[0]["Address"].ToString();
                label15.Text = Address;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }
        }

        private void AdminProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
