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
    public partial class CustomerForm2 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        CustomerForm1 customerForm1;
        private String transno, pcode;
        private double price;
        public CustomerForm2(CustomerForm1 customerForm1)
        {
            InitializeComponent();
            this.customerForm1 = customerForm1;
            connection = new SqlConnection(dbConnection.MyConnection());
        }

        public void ProductDetails(String pcode, String price, String transno)
        {
            this.pcode = pcode;
            this.transno = transno;
            this.price = double.Parse(price);
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar == 13) && (txtQuantity.Text != String.Empty))
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand("select * from cart where pcode = @pcode and transaction_no = @trans", connection);
                    command.Parameters.AddWithValue("@pcode", pcode);
                    command.Parameters.AddWithValue("@trans", transno);
                    sqlDataReader = command.ExecuteReader();
                    if(sqlDataReader.Read())
                    {
                        sqlDataReader.Close();
                        connection.Close();

                        connection.Open();
                        command = new SqlCommand("update cart set quantity=quantity+@quantity, date=@date, cashier=@cashier where transaction_no=@trans and pcode=@pcode", connection);
                        command.Parameters.AddWithValue("@pcode", pcode);
                        command.Parameters.AddWithValue("@trans", transno);
                        command.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                        command.Parameters.AddWithValue("@date", DateTime.Now);
                        command.Parameters.AddWithValue("@cashier", customerForm1.labelUser.Text);
                        command.ExecuteNonQuery();
                        this.Dispose();
                        connection.Close();
                        customerForm1.LoadCart();
                        customerForm1.txtSearch.Clear();
                    } else
                    {
                        connection.Close();
                        sqlDataReader.Close();

                        connection.Open();
                        command = new SqlCommand("insert into cart (transaction_no, pcode, price, quantity, date, cashier) values (@transno, @pcode, @price, @quantity, @date, @cashier)", connection);
                        command.Parameters.AddWithValue("@transno", transno);
                        command.Parameters.AddWithValue("@pcode", pcode);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                        command.Parameters.AddWithValue("@date", DateTime.Now);
                        command.Parameters.AddWithValue("@cashier", customerForm1.labelUser.Text);
                        command.ExecuteNonQuery();
                        this.Dispose();
                        connection.Close();
                        customerForm1.LoadCart();
                        customerForm1.txtSearch.Clear();
                    }
                }
                catch(Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
