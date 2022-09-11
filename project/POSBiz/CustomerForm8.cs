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
    public partial class CustomerForm8 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        String title = "Paboritos POS System";
        CustomerForm7 customerForm7;
        public CustomerForm8(CustomerForm7 customerForm7)
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            this.customerForm7 = customerForm7;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string user = customerForm7.txtCancelBy.Text;
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM users WHERE username=@user AND password=@pass", connection);
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@pass", HashUtil.HashPassword(textPass.Text));
                sqlDataReader = command.ExecuteReader();
                sqlDataReader.Read();
                if(sqlDataReader.HasRows)
                {
                    sqlDataReader.Close();
                    connection.Close();

                    SaveCancelOrder(user);

                    if (customerForm7.comboInventory.Text == "Yes")
                    {
                        SendToInventory();
                    }
                    UpdateCart();
                    MessageBox.Show("Order has been cancelled", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                    customerForm7.Dispose();
                    customerForm7.RefreshList();
                    return;
                }
                sqlDataReader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SaveCancelOrder(String user)
        {
            try
            {
                connection.Open();
                command = new SqlCommand("" +
                    "insert into cancel(transaction_no, pcode, price, quantity, date, voidby, canceledby, reason) values (@transaction_no, @pcode, @price, @quantity, @date, @voidby, @canceledby, @reason)" +
                    "", connection);
                command.Parameters.AddWithValue("@transaction_no", customerForm7.txtTrans.Text);
                command.Parameters.AddWithValue("@pcode", customerForm7.txtPCode.Text);
                command.Parameters.AddWithValue("@price", double.Parse(customerForm7.txtPrice.Text));
                command.Parameters.AddWithValue("@quantity", int.Parse(customerForm7.txtQTY.Text));
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@voidby", customerForm7.txtVoidBy.Text);
                command.Parameters.AddWithValue("@canceledby", customerForm7.txtCancelBy.Text);
                command.Parameters.AddWithValue("@reason", customerForm7.txtReason.Text);
                command.Parameters.AddWithValue("@action", customerForm7.comboInventory.Text);
                command.ExecuteNonQuery();
                connection.Close();

            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendToInventory()
        {
            int cancelQuantity = int.Parse(customerForm7.txtCancelQuantity.Text) >= int.Parse(customerForm7.txtQTY.Text) ? int.Parse(customerForm7.txtQTY.Text) : int.Parse(customerForm7.txtCancelQuantity.Text);
            try
            {
                connection.Open();
                command = new SqlCommand("update product set quantity = quantity+" + cancelQuantity + " where pcode='" + customerForm7.txtPCode.Text + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCart()
        {
            int cancelQuantity = int.Parse(customerForm7.txtCancelQuantity.Text) >= int.Parse(customerForm7.txtQTY.Text) ? int.Parse(customerForm7.txtQTY.Text) : int.Parse(customerForm7.txtCancelQuantity.Text);
            try
            {
                connection.Open();
                command = new SqlCommand("update cart set quantity = quantity-" + cancelQuantity + " where id='" + customerForm7.txtID.Text + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
