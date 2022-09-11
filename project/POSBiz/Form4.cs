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
    public partial class Form4 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader reader;
        string title = "Paboritos POS System";
        public Form4()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadVendor();
        }

        public void LoadStocks()
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM vwstockin WHERE status='Pending'", connection);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    i+=1;
                    dataGridView2.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),reader["vendor"].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                }
                reader.Close();

                connection.Close();
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(vendorBox.Text.ToLower() == "comissary" || vendorBox.Text.ToLower() == "commissary")
            {
                try
                {
                    if (dataGridView2.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Are you sure you want to save this records?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                connection.Open();
                                command = new SqlCommand("UPDATE product SET quantity = quantity + " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + " WHERE pcode = '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'", connection);
                                command.ExecuteNonQuery();
                                connection.Close();

                                connection.Open();
                                command = new SqlCommand("UPDATE stockin SET quantity = quantity + " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + ", status= 'Done' WHERE reference_no ='" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "'", connection);
                                command.ExecuteNonQuery();
                                connection.Close();

                                connection.Open();
                                command = new SqlCommand("UPDATE inventory SET quantity = quantity - " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + " WHERE pcode = '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'", connection);
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                            //MessageBox.Show("Successfully Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            reference.Clear();
                            stockInBy.Clear();
                            stockInDate.Value = DateTime.Now;
                            LoadStocks();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            try
            {
                if(dataGridView2.Rows.Count > 0)
                {
                    if(MessageBox.Show("Are you sure you want to save this records?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        
                        for(int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            connection.Open();
                            command = new SqlCommand("UPDATE product SET quantity = quantity + " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + " WHERE pcode = '" + dataGridView2.Rows[i].Cells[3].Value.ToString() + "'", connection);
                            command.ExecuteNonQuery();
                            connection.Close();
                            
                            connection.Open();
                            command = new SqlCommand("UPDATE stockin SET quantity = quantity + " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + ", status= 'Done' WHERE reference_no ='" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "'", connection);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        //MessageBox.Show("Successfully Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        reference.Clear();
                        stockInBy.Clear();
                        stockInDate.Value = DateTime.Now;
                        LoadStocks();
                        
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
            if(colName == "colDelete")
            {
                if(MessageBox.Show("Remove this item?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    connection.Open();
                    command = new SqlCommand("DELETE FROM stockin WHERE id = @id and reference_no=@reference", connection);
                    command.Parameters.AddWithValue("@id", int.Parse(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    command.Parameters.AddWithValue("@reference", dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Item has been removed successfully", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStocks();
                }
            }
        }

        public void LoadVendor()
        {
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vendor", connection);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    vendorBox.Items.Add(reader["vendor"]);
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

        private void reference_KeyPress(object sender, KeyPressEventArgs e)
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!IsNumber(txtID.Text) || txtID.Text == String.Empty)
            {
                MessageBox.Show("Please choose appropriate vendor!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Form5 form5 = new Form5(this);
            form5.LoadProducts();
            form5.ShowDialog();
        }

        private Boolean IsNumber(String thisText)
        {
            try
            {
                int.Parse(thisText);
            }catch(Exception)
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        public void LoadStockInHistory()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM vwStockIn WHERE date BETWEEN '" + date1.Value + "' AND '" + date2.Value + "' AND status = 'Done'", connection);
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(),reader["vendor"].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vendorBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vendor where vendor='" + vendorBox.Text + "'", connection);
                reader = command.ExecuteReader();
                if(reader.Read())
                {
                    txtID.Text = reader["id"].ToString();
                    txtContactPerson.Text = reader["contactperson"].ToString();
                    txtAddress.Text = reader["address"].ToString();
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random random = new Random();
            reference.Clear();
            reference.Text += random.Next();
        }
    }
}
