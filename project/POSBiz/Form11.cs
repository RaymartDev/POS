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
    public partial class Form11 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        public Form11()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecords()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vendor", connection);
                sqlDataReader = command.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i,
                        sqlDataReader["id"].ToString(),
                        sqlDataReader["vendor"].ToString(),
                        sqlDataReader["address"].ToString(),
                        sqlDataReader["contactperson"].ToString(),
                        sqlDataReader["telephone"].ToString(),
                        sqlDataReader["email"].ToString());
                }
                sqlDataReader.Close();
                connection.Close();
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10(this);
            form10.btnUpdate.Enabled = false;
            form10.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                Form10 form = new Form10(this);
                form.txtID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                form.txtVendor.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                form.txtAddress.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                form.txtContact.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                form.txtTelephone.Text = dataGridView1[5, e.RowIndex].Value.ToString();
                form.txtEmail.Text = dataGridView1[6, e.RowIndex].Value.ToString();
                form.btnSave.Enabled = false;
                form.ShowDialog();
                return;
            }
            if (colName == "Delete")
            {
                try
                {
                    if (MessageBox.Show("Confirm to delete this vendor", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        connection.Open();
                        command = new SqlCommand("DELETE FROM vendor WHERE id=@id", connection);
                        command.Parameters.AddWithValue("@id", dataGridView1[1, e.RowIndex].Value.ToString());
                        command.ExecuteNonQuery();
                        MessageBox.Show("Vendor has been successfully deleted");
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
    }
}
