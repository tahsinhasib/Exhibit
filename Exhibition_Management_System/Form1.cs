namespace Exhibition_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel3.Width += 3;

            if(panel3.Width >= 599)
            {
                timer1.Stop();
                Login login= new Login();
                login.Show();
                this.Hide();
            }
        }
    }
}