using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace _18AvrilProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
        }
        SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button_login_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE email=@username AND pwd=@password", conn);
                cmd.Parameters.AddWithValue("@username", text_username.Text);
                cmd.Parameters.AddWithValue("@password", text_password.Text);
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    // Les informations d'identification sont correctes. 
                    // Rediriger vers l'interface principale de l'application.
                    MessageBox.Show("correct.");
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();


                }
                else
                {
                    // Les informations d'identification sont incorrectes. 
                    // Afficher un message d'erreur.
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

           
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            text_username.Clear();
            text_password.Clear();
        }

        private void button_login_Click_1(object sender, EventArgs e)
        {

        }

        private void button_clear_Click_1(object sender, EventArgs e)
        {

        }
    }
}