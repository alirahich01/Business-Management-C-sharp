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
    public partial class Form4 : Form
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;
        private object newwId;

        public Form4()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void afficher_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            sda = new SqlDataAdapter("SELECT id, pu, libelle, quantite FROM Article", conn);
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

                // Delete the article record from the database using SQL DELETE statement
                string deleteQuery = "DELETE FROM Article WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Article deleted successfully");
                    }
                }

                // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
                dt.Rows[selectedIndex].Delete();
                scb = new SqlCommandBuilder(sda);
                //sda.Update(dt);
            }
            else
            {
                MessageBox.Show("Please select an article to delete");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Retrieve the ID of the selected row
                int id = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["id"].Value);

                // Prompt the user for the new article details
                string newPu = Microsoft.VisualBasic.Interaction.InputBox("Enter the new unit price:", "Update Article");
                string newQuantite = Microsoft.VisualBasic.Interaction.InputBox("Enter the new quantity:", "Update Article");

                // Update the article record in the database using SQL UPDATE statement
                string updateQuery = "UPDATE Article SET pu = @pu, quantite = @quantite WHERE id = @id";
                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pu", newPu);
                    cmd.Parameters.AddWithValue("@quantite", newQuantite);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Article updated successfully");
                    }
                }

                // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
                dt.Rows[selectedIndex]["pu"] = newPu;
                dt.Rows[selectedIndex]["quantite"] = newQuantite;
                scb = new SqlCommandBuilder(sda);
                //sda.Update(dt);
            }
            else
            {
                MessageBox.Show("Please select an article to update");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Prompt the user for the new article details
            string newIdc = Microsoft.VisualBasic.Interaction.InputBox("Enter the id of the new article:", "Add New Article");
            string newPu = Microsoft.VisualBasic.Interaction.InputBox("Enter the unit price of the new article:", "Add New Article");
            string newLibelle = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the new article:", "Add New Article");
            string newQuantite = Microsoft.VisualBasic.Interaction.InputBox("Enter the quantity of the new article:", "Add New Article");

            // Insert the new article record into the database using SQL INSERT statement
            string insertQuery = "INSERT INTO Article (id, pu, libelle, quantite) VALUES (@newId, @pu, @libelle, @quantite)";
            using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@newId", newIdc);
                cmd.Parameters.AddWithValue("@pu", newPu);
                cmd.Parameters.AddWithValue("@libelle", newLibelle);
                cmd.Parameters.AddWithValue("@quantite", newQuantite);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("New article added successfully");
                }
            }

            // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
            dt.Rows.Add(new object[] { newwId, newPu, newLibelle, newQuantite });

            scb = new SqlCommandBuilder(sda);
            // sda.Update(dt);

        }
    }
}
