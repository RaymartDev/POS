using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace POSBiz
{
    public partial class Form14 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        string title = "Paboritos POS System";
        public Form14()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadRecords();
            NotifyCriticalItems();
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM inventory WHERE description LIKE '%" + txtSearch.Text + "%' ORDER BY pcode", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i,
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        double.Parse(sqlDataReader["price"].ToString()).ToString("#,##0.00"),
                        int.Parse(sqlDataReader["quantity"].ToString()).ToString("#,##0"),
                        sqlDataReader["reorder"].ToString()
                        );
                }
                sqlDataReader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        public void NotifyCriticalItems()
        {
            string critical = "";
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vwCriticalItemsInventory", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    critical += i + ". " + sqlDataReader["description"].ToString() + Environment.NewLine;
                }
                sqlDataReader.Close();
                connection.Close();

                if (i > 0)
                {
                    PopupNotifier popup = new PopupNotifier();
                    popup.Image = Properties.Resources.icons8_cancel_24;
                    popup.TitleText = "CRITICAL ITEM(S)";
                    popup.ContentText = critical;
                    popup.Popup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                Form3 form = new Form3(this);
                form.labelID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                form.pCode.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                form.description.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                form.price.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                form.quantity.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                form.reorder.Text = dataGridView1[5, e.RowIndex].Value.ToString();
                form.btnSave.Enabled = false;
                form.ShowDialog();
                return;
            }
            if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show("Confirm to delete this product", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        connection.Open();
                        command = new SqlCommand("DELETE FROM adjustment WHERE pcode=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        connection.Close();

                        connection.Open();
                        command = new SqlCommand("DELETE FROM cancel WHERE pcode=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        connection.Close();

                        connection.Open();
                        command = new SqlCommand("DELETE FROM cart WHERE pcode=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        connection.Close();

                        connection.Open();
                        command = new SqlCommand("DELETE FROM stockin WHERE pcode=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        connection.Close();

                        connection.Open();
                        command = new SqlCommand("DELETE FROM product WHERE pcode=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product has been successfully deleted");
                        connection.Close();

                        connection.Open();
                        command = new SqlCommand("DELETE FROM inventory WHERE pcode=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product has been successfully deleted");
                        connection.Close();
                        LoadRecords();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 updateForm = new Form3(this);
            updateForm.btnUpdate.Enabled = false;
            updateForm.ShowDialog();
        }
    }
}
