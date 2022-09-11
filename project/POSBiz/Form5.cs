using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POSBiz
{
    public partial class Form5 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader reader;
        String title = "Paboritos POS System";
        Form4 form4;
        public Form5(Form4 form4)
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            this.form4 = form4;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if (form4.reference.Text == string.Empty) { MessageBox.Show("Please generate reference #", title, MessageBoxButtons.OK, MessageBoxIcon.Warning); form4.reference.Focus(); return; }
                if (form4.stockInBy.Text == string.Empty) { MessageBox.Show("Please enter stock in by", title, MessageBoxButtons.OK, MessageBoxIcon.Warning); form4.stockInBy.Focus(); return; }
                try
                {
                    if (MessageBox.Show("Add this item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        connection.Open();
                        command = new SqlCommand("INSERT INTO stockin (reference_no, pcode, quantity, date, stockin_by,vendorid) VALUES (@reference_no, @pcode, @quantity, @date, @stockin_by, @vendorid)", connection);
                        command.Parameters.AddWithValue("@reference_no", form4.reference.Text);
                        command.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                        command.Parameters.AddWithValue("@quantity", 0);
                        command.Parameters.AddWithValue("@date", form4.stockInDate.Value);
                        command.Parameters.AddWithValue("@stockin_by", form4.stockInBy.Text);
                        command.Parameters.AddWithValue("@vendorid", int.Parse(form4.txtID.Text));
                        command.ExecuteNonQuery();
                        MessageBox.Show("Successfully Added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        connection.Close();
                        form4.LoadStocks();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }

        public void LoadProducts()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();

                command = new SqlCommand("SELECT pcode, description, quantity FROM product WHERE description LIKE '%" + txtSearch.Text + "%' ORDER BY description", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                }
                reader.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
