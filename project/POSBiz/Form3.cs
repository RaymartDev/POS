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
    public partial class Form3 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        string title = "Paboritos POS System";
        Form14 form3;

        public Form3(Form14 form3)
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            this.form3 = form3;
        }

        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            pCode.Clear();  
            description.Clear();
            price.Clear();
            quantity.Clear();
            pCode.Focus();
        }

        private void ClearUpdate()
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            pCode.Clear();
            description.Clear();
            price.Clear();
            quantity.Clear();
            pCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM inventory WHERE pcode=@product", connection);
                command.Parameters.AddWithValue("@product", pCode.Text);
                sqlDataReader = command.ExecuteReader();
                if (!sqlDataReader.Read())
                {
                    sqlDataReader.Close();
                    command = new SqlCommand("INSERT INTO inventory(pcode, description, price, quantity, reorder) VALUES(@product, @desc, @price, @quantity, @reorder)", connection);
                    command.Parameters.AddWithValue("@product", pCode.Text);
                    command.Parameters.AddWithValue("@desc", description.Text);
                    command.Parameters.AddWithValue("@price", double.Parse(price.Text));
                    command.Parameters.AddWithValue("@quantity", int.Parse(quantity.Text));
                    command.Parameters.AddWithValue("@reorder", int.Parse(reorder.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                    form3.LoadRecords();
                }
                else
                {
                    MessageBox.Show("Unsuccessful: PCODE Exists!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    Clear();
                    form3.LoadRecords();
                    sqlDataReader.Close();
                    return;
                }

                connection.Open();
                command = new SqlCommand("SELECT * FROM product WHERE pcode=@product", connection);
                command.Parameters.AddWithValue("@product", pCode.Text);
                sqlDataReader = command.ExecuteReader();
                if (!sqlDataReader.Read())
                {
                    sqlDataReader.Close();
                    command = new SqlCommand("INSERT INTO product(pcode, description, price, quantity, reorder) VALUES(@product, @desc, @price, @quantity, @reorder)", connection);
                    command.Parameters.AddWithValue("@product", pCode.Text);
                    command.Parameters.AddWithValue("@desc", description.Text);
                    command.Parameters.AddWithValue("@price", double.Parse(price.Text));
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@reorder", int.Parse(reorder.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record has been successfully saved.");
                    Clear();
                } else
                {
                    connection.Close();
                    Clear();
                    sqlDataReader.Close();
                }
                this.Dispose();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm to update", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    connection.Open();
                    command = new SqlCommand("UPDATE product SET pcode=@product, description=@desc, price=@price, reorder=@reorder WHERE pcode=@product", connection);
                    command.Parameters.AddWithValue("@product", pCode.Text);
                    command.Parameters.AddWithValue("@desc", description.Text);
                    command.Parameters.AddWithValue("@price", double.Parse(price.Text));
                    command.Parameters.AddWithValue("@reorder", int.Parse(reorder.Text));
                    command.ExecuteNonQuery();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("UPDATE inventory SET pcode=@product, description=@desc, price=@price, quantity=@quantity, reorder=@reorder WHERE pcode=@product", connection);
                    command.Parameters.AddWithValue("@product", pCode.Text);
                    command.Parameters.AddWithValue("@desc", description.Text);
                    command.Parameters.AddWithValue("@price", double.Parse(price.Text));
                    command.Parameters.AddWithValue("@quantity", int.Parse(quantity.Text));
                    command.Parameters.AddWithValue("@reorder", int.Parse(reorder.Text));
                    command.ExecuteNonQuery();
                    ClearUpdate();
                    form3.LoadRecords();
                    connection.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 46)
            {

            } else if(e.KeyChar == 8)
            {
                
            } else if((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {

            }
            else if (e.KeyChar == 8)
            {

            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {

            }
            else if (e.KeyChar == 8)
            {

            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void reorder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {

            }
            else if (e.KeyChar == 8)
            {

            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }
    }
}
