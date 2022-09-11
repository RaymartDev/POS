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
using Tulpep.NotificationWindow;

namespace POSBiz
{
    public partial class CustomerForm1 : Form
    {
        String id = "", price = "";
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader reader;
        String title = "Paboritos POS System";
        public CustomerForm1()
        {
            InitializeComponent();
            labelDate.Text = DateTime.Now.ToLongDateString();
            connection = new SqlConnection(dbConnection.MyConnection());
            this.KeyPreview = true;
            txtSearch.Enabled = false;
            NotifyCriticalItems();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (labelTransactionNo.Text != "0000000000")
            {
                return;
            }
            GetTransactionNo();
            txtSearch.Enabled = true;
        }

        public void NotifyCriticalItems()
        {
            string critical = "";
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vwCriticalItems", connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i += 1;
                    critical += i + ". " + reader["description"].ToString() + Environment.NewLine;
                }
                reader.Close();
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

        public void GetTransactionNo()
        {
            try
            {
                string date = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count;
                connection.Open();
                command = new SqlCommand("select top 1 transaction_no from cart where transaction_no like '" + date + "%' order by id desc", connection);
                reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    transno = reader[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    labelTransactionNo.Text = date + (count + 1);
                }
                else
                {
                    transno = date + "1001";
                    labelTransactionNo.Text = transno;
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadCart()
        {
            int i = 0;
            double total = 0, discounted_price = 0, subTotal = 0, discount = 0;
            bool hasRecord = false;
            try
            {
                dataGridView1.Rows.Clear();
                connection.Open();
                command = new SqlCommand("select c.id, c.pcode, p.description, c.price, c.quantity, c.discount, c.total from cart as c inner join product as p on c.pcode = p.pcode where transaction_no = '" + labelTransactionNo.Text + "' and status = 'Pending'", connection);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    i += 1;
                    subTotal += double.Parse(reader["price"].ToString()) * double.Parse(reader["quantity"].ToString());
                    total += double.Parse(reader["total"].ToString());
                    discounted_price += double.Parse(reader["discount"].ToString());
                    double row_discount = (double.Parse(reader["discount"].ToString()) == 0) ? 0 : (double.Parse(reader["price"].ToString()) * double.Parse(reader["quantity"].ToString())) - (double.Parse(reader["discount"].ToString()));
                    discount += row_discount;
                    dataGridView1.Rows.Add(i,reader["id"], reader["pcode"], reader["description"].ToString(), reader["price"].ToString(), reader["quantity"].ToString(),row_discount, reader["total"].ToString());
                    if(!hasRecord)
                    {
                        hasRecord = true;
                        btnAddDisc.Enabled = true;
                        btnSettlePayment.Enabled = true;
                        btnCancelSale.Enabled = true;
                    }
                } 
                reader.Close();
                connection.Close();
                labelSubTotal.Text = subTotal.ToString("#,##0.00");
                labelDiscount.Text = discount == 0 ? "0.00" : discount.ToString("#,##0.00");
                labelPrice.Text = total.ToString("#,##0.00");
                labelTotal.Text = total.ToString("#,##0.00");
                if(!hasRecord)
                {
                    btnAddDisc.Enabled = false;
                    btnSettlePayment.Enabled = false;
                    btnCancelSale.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Logout Application?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (labelTransactionNo.Text == "0000000000")
            {
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("select * from product where pcode = '" + txtSearch.Text + "'", connection);
                reader = command.ExecuteReader();
                if(reader.Read())
                {
                    CustomerForm2 customerForm2 = new CustomerForm2(this);
                    customerForm2.ProductDetails(reader["pcode"].ToString(), reader["price"].ToString(), labelTransactionNo.Text);
                    reader.Close();
                    connection.Close();
                    customerForm2.ShowDialog();
                } else
                {
                    reader.Close();
                    connection.Close();
                }
                
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            if(labelTransactionNo.Text == "0000000000") 
            {
                return; 
            }
            CustomerForm3 customerForm3 = new CustomerForm3(this);
            customerForm3.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "colDelete")
            {
                try
                {
                    if (MessageBox.Show("Confirm to delete this from cart?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        connection.Open();
                        command = new SqlCommand("DELETE FROM cart WHERE id=@id", connection);
                        command.Parameters.AddWithValue("@id", int.Parse(dataGridView1[1, e.RowIndex].Value.ToString()));
                        command.ExecuteNonQuery();
                        connection.Close();
                        LoadCart();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }

        private void btnAddDisc_Click(object sender, EventArgs e)
        {
            if (labelTransactionNo.Text == "0000000000")
            {
                return;
            }
            if(id == String.Empty)
            {
                return;
            }
            CustomerForm4 customerForm4 = new CustomerForm4(this);
            customerForm4.labelID.Text = id;
            customerForm4.txtPrice.Text = price;
            customerForm4.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("hh:MM:ss tt");
            labelDay.Text = DateTime.Now.ToLongDateString();
        }

        private void btnSettlePayment_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count <= 0)
            {
                return;
            }
            CustomerForm5 customerForm5 = new CustomerForm5(this);
            customerForm5.ShowDialog();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            CustomerForm6 customerForm6 = new CustomerForm6();
            customerForm6.comboCashier.Enabled = false;
            customerForm6.date1.Enabled = false;
            customerForm6.date2.Enabled = false;
            customerForm6.comboCashier.Text = labelUser.Text;
            customerForm6.user = labelUser.Text;
            customerForm6.ShowDialog();
        }

        private void btnAddDisc_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                btnNew_Click(sender, e);
                return;
            }
            if(e.KeyCode == Keys.F2)
            {
                btnSearchProduct_Click(sender, e);
                return;
            }
            if(e.KeyCode == Keys.F3)
            {
                btnAddDisc_Click(sender, e);
                return;
            }
            if(e.KeyCode == Keys.F4)
            {
                btnSettlePayment_Click(sender, e);
                return;
            }
            if(e.KeyCode == Keys.F5)
            {
                btnCancelSale_Click(sender, e);
                return;
            }
            if (e.KeyCode == Keys.F6)
            {
                btnDailySales_Click(sender, e);
                return;
            }
            if (e.KeyCode == Keys.F7)
            {
                btnChangePass_Click(sender, e);
                return;
            }
            if (e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
                return;
            }
        }

        private void btnCancelSale_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Remove All Items?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                connection.Open();
                command = new SqlCommand("delete from cart where transaction_no='" + labelTransactionNo.Text + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
                LoadCart();
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            CustomerForm9 customerForm9 = new CustomerForm9(this);
            customerForm9.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[1, i].Value.ToString();
            price = dataGridView1[4, i].Value.ToString();
        }
    }
}
