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

namespace _18AvrilProject
{
    public partial class Form2 : Form
    {
        
        public Form2(int roleId)
        {
            InitializeComponent();
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }


        private void clientToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void achatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
   
                Form7 form7 = new Form7();
                form7.Show();
                this.Hide();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private int GetRoleId()
        {
            // Get the role id of the currently logged in user from the database
            int roleId = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
            {
                string query = "SELECT role_id FROM [User] WHERE email = @email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@email", loggedInUser);
                    roleId = (int)cmd.ExecuteScalar();
                }
            }
            return roleId;
        }

        private void commandesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void fournisseursXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
