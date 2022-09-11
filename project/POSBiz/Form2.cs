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

namespace POSBiz
{
    public partial class Form2 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        String title = "Paboritos POS System";
        public Form2()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM product WHERE description LIKE '%" + txtSearch.Text + "%' ORDER BY pcode", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i,
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        double.Parse(sqlDataReader["price"].ToString()).ToString("#,##0.00"),
                        sqlDataReader["reorder"].ToString()
                        );
                }
                sqlDataReader.Close();
                connection.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
