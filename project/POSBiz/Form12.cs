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
    public partial class Form12 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        private int quantitySelected;
        public Form12()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadRecords();
            ReferenceNo();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ReferenceNo()
        {
            Random random = new Random();
            txtReferenceNo.Text = random.Next().ToString();
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
                        sqlDataReader["quantity"].ToString()
                        );
                }
                sqlDataReader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void Clear()
        {
            txtPCODE.Clear();
            txtDesc.Clear();
            txtQuantity.Clear();
            comboCommand.Text = "";
            txtRemarks.Clear();
            LoadRecords();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "Select")
            {
                txtPCODE.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDesc.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                quantitySelected = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                

                if(comboCommand.Text == "REMOVE FROM INVENTORY")
                {
                    if (int.Parse(txtQuantity.Text) > quantitySelected)
                    {
                        MessageBox.Show("STOCK ON HAND MUST NOT BE GREATER THAN ADJUSTMENT QUANTITY!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Open();
                    command = new SqlCommand("update product set quantity=quantity-@adjustment where pcode=@pcode", connection);
                    command.Parameters.AddWithValue("@adjustment", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("insert into adjustment(reference_no, pcode, quantity, action, remarks, date, user) values (@reference_no, @pcode, @quantity, @action, @remarks, @date, @user)", connection);
                    command.Parameters.AddWithValue("@reference_no", txtReferenceNo.Text);
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@action", comboCommand.Text);
                    command.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@user", txtUser.Text);
                    connection.Close();

                    Clear();
                    MessageBox.Show("Stock has been updated successfully!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(comboCommand.Text == "ADD TO INVENTORY")
                {
                    connection.Open();
                    command = new SqlCommand("update product set quantity=quantity+@adjustment where pcode=@pcode", connection);
                    command.Parameters.AddWithValue("@adjustment", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("insert into adjustment(reference_no, pcode, quantity, action, remarks, date, user) values (@reference_no, @pcode, @quantity, @action, @remarks, @date, @user)", connection);
                    command.Parameters.AddWithValue("@reference_no", txtReferenceNo.Text);
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@action", comboCommand.Text);
                    command.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@user", txtUser.Text);
                    connection.Close();

                    Clear();
                    MessageBox.Show("Stock has been updated successfully!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (comboCommand.Text == "OUTLET TO COMMISSARY")
                {
                    if (int.Parse(txtQuantity.Text) > quantitySelected)
                    {
                        MessageBox.Show("STOCK ON OUTLET MUST NOT BE GREATER THAN ADJUSTMENT QUANTITY!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Open();
                    command = new SqlCommand("update product set quantity=quantity-@adjustment where pcode=@pcode", connection);
                    command.Parameters.AddWithValue("@adjustment", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("update inventory set quantity=quantity+@adjustment where pcode=@pcode", connection);
                    command.Parameters.AddWithValue("@adjustment", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("insert into adjustment(reference_no, pcode, quantity, action, remarks, date, user) values (@reference_no, @pcode, @quantity, @action, @remarks, @date, @user)", connection);
                    command.Parameters.AddWithValue("@reference_no", txtReferenceNo.Text);
                    command.Parameters.AddWithValue("@pcode", txtPCODE.Text);
                    command.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text));
                    command.Parameters.AddWithValue("@action", comboCommand.Text);
                    command.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());
                    command.Parameters.AddWithValue("@user", txtUser.Text);
                    connection.Close();

                    Clear();
                    MessageBox.Show("Stock has been updated successfully!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
