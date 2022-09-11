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
    public partial class CustomerForm6 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        public string user = "";
        String title = "Paboritos POS System";
        public CustomerForm6()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            date1.Value = DateTime.Now;
            date2.Value = DateTime.Now;
            LoadRecord();
            LoadCashier();
            comboCashier.Text = "All Cashier";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord()
        {
            int i = 0;
            double total = 0;
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();
                if (comboCashier.Text == "All Cashier")
                {
                    command = new SqlCommand("select c.id, c.transaction_no, c.pcode, p.description, p.price, c.quantity, c.discount, c.total from cart as c inner join product as p on c.pcode = p.pcode where status = 'Sold' and date between '" + date1.Value + "' and '" + date2.Value + "'", connection);
                }
                else
                {
                    command = new SqlCommand("select c.id, c.transaction_no, c.pcode, p.description, p.price, c.quantity, c.discount, c.total from cart as c inner join product as p on c.pcode = p.pcode where status = 'Sold' and date between '" + date1.Value + "' and '" + date2.Value + "' and cashier='" + comboCashier.Text + "'", connection);
                }
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    double discount = (double.Parse(sqlDataReader["price"].ToString()) * double.Parse(sqlDataReader["quantity"].ToString())) - double.Parse(sqlDataReader["total"].ToString());
                    total += double.Parse(sqlDataReader["total"].ToString());
                    dataGridView1.Rows.Add(i, sqlDataReader["id"].ToString(),
                        sqlDataReader["transaction_no"].ToString(),
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        sqlDataReader["price"].ToString(),
                        sqlDataReader["quantity"].ToString(),
                        discount.ToString(),
                        sqlDataReader["total"].ToString());
                }
                sqlDataReader.Close();
                connection.Close();
                labelTotal.Text = total.ToString("#,##0.00");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void date1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void date2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void LoadCashier()
        {
            comboCashier.Items.Clear();
            try
            {
                connection.Open();

                command = new SqlCommand("select * from users where role = 'Cashier'", connection);
                sqlDataReader = command.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    comboCashier.Items.Add(sqlDataReader["username"].ToString());
                }

                connection.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "btnCancel")
            {
                CustomerForm7 customerForm7 = new CustomerForm7(this);
                customerForm7.txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerForm7.txtTrans.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerForm7.txtPCode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                customerForm7.txtDesc.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                customerForm7.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                customerForm7.txtQTY.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                customerForm7.txtDisc.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                customerForm7.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                customerForm7.txtCancelBy.Text = user;
                customerForm7.ShowDialog();
            }
        }

        private void comboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
