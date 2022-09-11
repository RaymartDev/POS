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
using System.Windows.Forms.DataVisualization.Charting;

namespace POSBiz
{
    public partial class Form7 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        public Form7()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadCriticalItems();
            LoadInventoryList();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("select top 10 pcode, description, isnull(sum(quantity), 0) as quantity, isnull(sum(total), 0) as total from vwSoldItems where date between '" + date1.Value.ToString() + "' and '" + date2.Value.ToString() + "' and status='Sold' group by pcode, description order by quantity desc", connection);
                sqlDataReader = command.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView1.Rows.Add(i, sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        sqlDataReader["quantity"].ToString(),
                        double.Parse(sqlDataReader["total"].ToString()).ToString("#,##0.00"));
                }
                sqlDataReader.Close();
                connection.Close();

                if(i > 0)
                {
                    LoadChartTopSelling();
                }
                
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            dataGridView2.Rows.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("select c.pcode, p.description, c.price, sum(c.quantity) as quantity, sum(c.discount) as discount, sum(c.total) as total from cart as c inner join product as p on c.pcode = p.pcode where status = 'Sold' and date between '" + date3.Value.ToString() + "' and '" + date4.Value.ToString() + "' group by c.pcode, p.description, c.price", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    double discount = double.Parse(sqlDataReader["total"].ToString()) - double.Parse(sqlDataReader["discount"].ToString());
                    i += 1;
                    dataGridView2.Rows.Add(i, sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        sqlDataReader["price"].ToString(),
                        sqlDataReader["quantity"].ToString(),
                        discount.ToString("#,##0.00"),
                        double.Parse(sqlDataReader["total"].ToString()).ToString("#,##0.00"));
                }
                sqlDataReader.Close();
                connection.Close();

                connection.Open();
                command = new SqlCommand("select isnull(sum(c.total), 0) as total from cart as c inner join product as p on c.pcode = p.pcode where status = 'Sold' and date between '" + date3.Value.ToString() + "' and '" + date4.Value.ToString() + "'", connection);
                labelTotal.Text = double.Parse(command.ExecuteScalar().ToString()).ToString("#,##0.00");
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadCriticalItems()
        {
            dataGridView3.Rows.Clear();
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vwCriticalItems order by reorder desc", connection);
                sqlDataReader = command.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView3.Rows.Add(i,
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        double.Parse(sqlDataReader["price"].ToString()).ToString("#,##0.00"),
                        sqlDataReader["reorder"].ToString(),
                        sqlDataReader["quantity"].ToString());
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

        public void LoadInventoryList()
        {
            dataGridView4.Rows.Clear();
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from product order by pcode", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView4.Rows.Add(i,
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        double.Parse(sqlDataReader["price"].ToString()).ToString("#,##0.00"),
                        sqlDataReader["reorder"].ToString(),
                        sqlDataReader["quantity"].ToString());
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

        public void LoadCancelledOrders()
        {
            dataGridView5.Rows.Clear();
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vwCancelledOrder where date between '" + datePicker1.Value.ToString() + "' and '" + datePicker2.Value.ToString() + "'", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView5.Rows.Add(i,
                        sqlDataReader["transaction_no"],
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        double.Parse(sqlDataReader["price"].ToString()).ToString("#,##0.00"),
                        sqlDataReader["quantity"].ToString(),
                        sqlDataReader["total"].ToString(),
                        sqlDataReader["date"].ToString(),
                        sqlDataReader["voidby"].ToString(),
                        sqlDataReader["canceledby"].ToString(),
                        sqlDataReader["reason"].ToString());
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

        public void LoadStockInHistory()
        {
            dataGridView6.Rows.Clear();
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vwStockIn where date between '" + datePicker3.Value.ToString() + "' and '" + datePicker4.Value.ToString() + "'", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    i += 1;
                    dataGridView6.Rows.Add(i,
                        sqlDataReader["reference_no"],
                        sqlDataReader["pcode"].ToString(),
                        sqlDataReader["description"].ToString(),
                        sqlDataReader["quantity"].ToString(),
                        sqlDataReader["date"].ToString(),
                        sqlDataReader["stockin_by"].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            LoadCancelledOrders();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        public void LoadChartTopSelling()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                sqlDataAdapter = new SqlDataAdapter("select top 10 description, isnull(sum(quantity), 0) as quantity from vwSoldItems where date between '" + date1.Value.ToString() + "' and '" + date2.Value.ToString() + "' and status='Sold' group by pcode, description order by quantity desc", connection);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "TOPSELLING");
                chart1.DataSource = dataSet.Tables["TOPSELLING"];
                Series series = chart1.Series["Series1"];
                series.ChartType = SeriesChartType.Doughnut;

                series.Name = "TOP SELLING";
                var chart = chart1;
                chart.Series[0].XValueMember = "description";
                chart.Series[0].YValueMembers = "quantity";
                chart.Series[0].IsValueShownAsLabel = true;
                chart.Series[0].LabelFormat = "{#,##0}";

                connection.Close();
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.labelTitle.Text = "SOLD ITEMS [" + date3.Value.ToShortDateString() + " - " + date4.Value.ToShortDateString() + "]";
            form13.LoadChartSold(date3.Value.ToShortDateString(), date4.Value.ToShortDateString());
            form13.ShowDialog();
        }
    }
}
