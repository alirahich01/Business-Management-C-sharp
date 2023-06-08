using Microsoft.VisualBasic;
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
    public partial class Form3 : Form
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;

        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void afficher_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            sda = new SqlDataAdapter("SELECT id, nom FROM Client", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Retrieve the ID of the selected row
                int id = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["id"].Value);

                // Delete the client record from the database using SQL DELETE statement
                string deleteQuery = "DELETE FROM Client WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Client deleted successfully");
                    }
                }

                // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
                dt.Rows[selectedIndex].Delete();
                scb = new SqlCommandBuilder(sda);
                sda.Update(dt);
            }
            else
            {
                MessageBox.Show("Please select a client to delete");
            }

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Prompt the user for the new client id and name
            string newId = Microsoft.VisualBasic.Interaction.InputBox("Enter the new client id:", "Add New Client");
            string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter the new client name:", "Add New Client");

            // Insert the new client record into the database using SQL INSERT statement
            string insertQuery = "INSERT INTO Client (id, nom) VALUES (@id, @name)";
            using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", newId);
                cmd.Parameters.AddWithValue("@name", newName);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("New client added successfully");
                }
            }

            // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
            dt.Rows.Add(new object[] { newId, newName });
            scb = new SqlCommandBuilder(sda);
           // sda.Update(dt);

        } 

        private void Form3_Load_2(object sender, EventArgs e)
        {

        }

        private void Form3_Load_3(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Retrieve the ID of the selected row
                int id = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["id"].Value);

                // Prompt the user for the new client name
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter the new client name:", "Update Client Name");

                // Update the client record in the database using SQL UPDATE statement
                string updateQuery = "UPDATE Client SET nom = @name WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", newName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Client name updated successfully");
                    }
                }

                // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
                dt.Rows[selectedIndex]["nom"] = newName;
                scb = new SqlCommandBuilder(sda);
                //sda.Update(dt);
            }
            else
            {
                MessageBox.Show("Please select a client to update");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
    }
}