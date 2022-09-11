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
    public partial class CustomerForm4 : Form
    {
        CustomerForm1 customerForm1;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        public CustomerForm4(CustomerForm1 customerForm1) 
        {
            InitializeComponent();
            this.customerForm1 = customerForm1;
            connection = new SqlConnection(dbConnection.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double price = double.Parse(txtPrice.Text);
                if(double.Parse(txtDiscount.Text) > 100)
                {
                    txtDiscount.Text = "100";
                }
                if(double.Parse(txtDiscount.Text) < 0)
                {
                    txtDiscount.Text = "0";
                }
                double discount = price * double.Parse(txtDiscount.Text);
                txtDiscountedAmount.Text = (price - discount).ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtDiscountedAmount.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtDiscount.Text == "0" || txtDiscount.Text == String.Empty)
            {
                return;
            }
            try
            {
                if(MessageBox.Show("Add a discount?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    connection.Open();
                    command = new SqlCommand("update cart set discount = case when @discount <= 0 then (price * quantity) else (price * quantity) - ((price * quantity) * @discount) end where id = @id", connection);
                    command.Parameters.AddWithValue("@discount", double.Parse(txtDiscount.Text));
                    command.Parameters.AddWithValue("@id", int.Parse(labelID.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                    customerForm1.LoadCart();
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
}
