using Microsoft.VisualBasic;
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
    public partial class Form6 : Form

    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            sda = new SqlDataAdapter("SELECT id, nom FROM Fournisseur", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Récupérez l'ID de la ligne sélectionnée
                int id = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["id"].Value);

                // Demander à l'utilisateur le nouveau nom du fournisseur
                string newSupplierName = Microsoft.VisualBasic.Interaction.InputBox("Enter the new supplier name:", "Update Supplier Name");

                // Mettre à jour l'enregistrement du fournisseur dans la base de données en utilisant l'instruction SQL UPDATE
                string updateQuery = "UPDATE Fournisseur SET nom = @name WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", newSupplierName);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier name updated successfully");
                    }
                }

                // Mettre à jour le DataTable et la base de données sous-jacente en utilisant SqlDataAdapter et SqlCommandBuilder
                dt.Rows[selectedIndex]["nom"] = newSupplierName;
                scb = new SqlCommandBuilder(sda);
                //sda.Update(dt);
            }
            else
            {
                MessageBox.Show("Please select a supplier to update");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the ID of the selected supplier
                string supplierId = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();

                // Define the SQL DELETE statement
                string deleteSql = "DELETE FROM Fournisseur WHERE id = @id";

                // Create a new SqlCommand object
                using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                {
                    // Add the parameter value for the DELETE statement
                    cmd.Parameters.AddWithValue("@id", supplierId);

                    // Execute the SqlCommand
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    // Check if the DELETE was successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error deleting supplier.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            // Get the supplier information from the user using a prompt
            string supplierId = Interaction.InputBox("Enter the supplier ID:", "Add Supplier", "");
            string supplierName = Interaction.InputBox("Enter the supplier name:", "Add Supplier", "");

            // If the user clicked Cancel or did not enter any information, exit the method
            if (string.IsNullOrEmpty(supplierId) || string.IsNullOrEmpty(supplierName))
            {
                return;
            }

            // Define the SQL INSERT statement
            string insertSql = "INSERT INTO Fournisseur (id, nom) VALUES (@id, @nom)";

            // Create a new SqlCommand object
            using (SqlCommand cmd = new SqlCommand(insertSql, conn))
            {
                // Add the parameter values for the INSERT statement
                cmd.Parameters.AddWithValue("@id", supplierId);
                cmd.Parameters.AddWithValue("@nom", supplierName);

                // Execute the SqlCommand
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                // Check if the INSERT was successful
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Supplier added successfully.");
                }
                else
                {
                    MessageBox.Show("Error adding supplier.");
                }
            }


        }
    }
}
