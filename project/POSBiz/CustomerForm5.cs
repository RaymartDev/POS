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
    public partial class CustomerForm5 : Form
    {
        String title = "Paboritos POS System";
        CustomerForm1 customerForm1;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        public CustomerForm5(CustomerForm1 customerForm1)
        {
            InitializeComponent();
            txtChange.Text = "0.00";
            this.customerForm1 = customerForm1;
            connection = new SqlConnection(dbConnection.MyConnection());
            txtTotal.Text = customerForm1.labelPrice.Text;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double total = Double.Parse(txtTotal.Text);
                double cash = Double.Parse(txtCash.Text);
                double change = cash-total;

                txtChange.Text = change.ToString("#,##0.00");
            }catch(Exception)
            {
                txtChange.Text = "0.00";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if(txtCash.Text == "0")
            {
                return;
            }
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            if (txtCash.Text == "0")
            {
                return;
            }
            txtCash.Text += btn00.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                double cash = double.Parse(txtCash.Text);
                double total = double.Parse(txtTotal.Text);
                if(cash <= 0 || total > cash)
                {
                    MessageBox.Show("Insufficient amount!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for(int i = 0; i < customerForm1.dataGridView1.Rows.Count; i++)
                {
                    connection.Open();
                    command = new SqlCommand("UPDATE product SET quantity = quantity - " + int.Parse(customerForm1.dataGridView1.Rows[i].Cells[5].Value.ToString()) + " WHERE pcode=@pcode", connection);
                    command.Parameters.AddWithValue("@pcode", customerForm1.dataGridView1.Rows[i].Cells[2].Value.ToString());
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("update cart set status = 'Sold' where pcode = '" + customerForm1.dataGridView1.Rows[i].Cells[2].Value.ToString() + "' and transaction_no = '" + customerForm1.labelTransactionNo.Text + "'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Payment has been successful!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                customerForm1.GetTransactionNo();
                customerForm1.LoadCart();
                this.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
