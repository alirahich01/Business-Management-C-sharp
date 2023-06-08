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
    public partial class Form5 : Form
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True");
            sda = new SqlDataAdapter("SELECT id, date_cmd, client_id FROM Commande", conn);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int id = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["id"].Value);

                using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Ligne_cmd WHERE cmd_id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Commande WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Commande deleted successfully.");
                            dt.Rows.RemoveAt(selectedRowIndex);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete commande.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Retrieve the ID of the selected row
                int id = Convert.ToInt32(dataGridView1.Rows[selectedIndex].Cells["id"].Value);

                // Prompt the user for the new date
                string newDateStr = Microsoft.VisualBasic.Interaction.InputBox("Enter the new date (yyyy-MM-dd):", "Update Date");

                // Attempt to parse the new date string into a DateTime object
                if (DateTime.TryParse(newDateStr, out DateTime newDate))
                {
                    // Reload the data from the database
                    dt.Clear();
                    sda.Fill(dt);

                    // Update the Commande record in the database using SQL UPDATE statement
                    string updateQuery = "UPDATE Commande SET date_cmd = @date WHERE id = @id";
                    using (SqlConnection conn = new SqlConnection(@"Data Source=MOUAD;Initial Catalog=OrderManagement;Integrated Security=True"))
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@date", newDate);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Date updated successfully");
                        }
                    }

                    // Update the DataTable and the underlying database using SqlDataAdapter and SqlCommandBuilder
                    dt.Rows[selectedIndex]["date_cmd"] = newDate;
                    scb = new SqlCommandBuilder(sda);
                    //sda.Update(dt);
                }
                else
                {
                    MessageBox.Show("Invalid date format. Please enter date in yyyy-MM-dd format.");
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update");
            }
        }
    }
}
