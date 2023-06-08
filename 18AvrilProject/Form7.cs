using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18AvrilProject
{
    public partial class Form7 : Form
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;
       

        public Form7(int userId)
        {
            InitializeComponent();
            this.roleId = roleId;
            LoadData();


        }

        private void LoadData()
        {
            try
            {
                string connectionString = @"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True";
                string query = "SELECT * FROM [Order]";
                if (roleId != 1) // Vérifie si le rôle de l'utilisateur connecté est différent de 1
                {
                    query += " WHERE [User_Id] = @userId";
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (roleId != 1) // Ajoute le paramètre userId s'il y a restriction d'accès
                        {
                            command.Parameters.AddWithValue("@userId", UserSession.UserId);
                        }

                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            sda = new SqlDataAdapter("SELECT id, date_ach, fourn_id FROM Achat", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BACK_Click(object sender, EventArgs e)
        {
            // Create an instance of Form2
            Form2 form2 = new Form2();

            // Show Form2 and hide Form3
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Retrieve the ID of the selected row
                int id = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["id"].Value);

                // Delete the related Ligne_ach records from the database using SQL DELETE statement
                string deleteLigneAchQuery = "DELETE FROM Ligne_ach WHERE ach_id = @ach_id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(deleteLigneAchQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ach_id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Delete the Achat record from the database using SQL DELETE statement
                string deleteAchatQuery = "DELETE FROM Achat WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(deleteAchatQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Achat deleted successfully");
                    }
                }

                // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
                dt.Rows[selectedIndex].Delete();
                scb = new SqlCommandBuilder(sda);
               // sda.Update(dt);
            }
            else
            {
                MessageBox.Show("Please select an achat to delete");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Prompt the user for input using an input box
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the date_ach, fourn_id values separated by comma", "Add new achat");

            // Split the input into separate values
            string[] values = input.Split(',');

            // Create a new SqlConnection object and open the connection
            using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
            {
                conn.Open();

                // Create a SqlCommand object with the INSERT statement
                SqlCommand cmd = new SqlCommand("INSERT INTO Achat (date_ach, fourn_id) VALUES (@date_ach, @fourn_id)", conn);

                // Add parameters to the SqlCommand object
                cmd.Parameters.AddWithValue("@date_ach", values[0].Trim());
                cmd.Parameters.AddWithValue("@fourn_id", values[1].Trim());

                // Execute the INSERT statement
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Record added successfully!");
                }
                else
                {
                    MessageBox.Show("Record not added!");
                }
            }

        }
    }
}
