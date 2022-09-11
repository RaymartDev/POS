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
    public partial class CustomerForm3 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        string title = "Paboritos POS System";
        CustomerForm1 customerForm1;
        public CustomerForm3(CustomerForm1 customerForm1)
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadRecords();
            this.customerForm1 = customerForm1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        public void LoadRecords()
        {
            int i = 0;
            try
            {
                dataGridView1.Rows.Clear();
                connection.Open();
                command = new SqlCommand("SELECT * FROM product WHERE description LIKE '%" + txtSearch.Text + "%' ORDER BY description", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i,
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        sqlDataReader["price"].ToString(),
                        sqlDataReader["quantity"].ToString()
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                CustomerForm2 customerForm2 = new CustomerForm2(customerForm1);
                customerForm2.ProductDetails(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(), customerForm1.labelTransactionNo.Text);
                customerForm2.ShowDialog();
            }
        }
    }
}
