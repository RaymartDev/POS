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
    public partial class Form9 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        public Form9()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadChart();
            LoadStoreDetails();
        }

        private void Form9_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }

        private void LoadStoreDetails()
        {
            try
            {
                connection.Open();
                command = new SqlCommand("select * from storeTable", connection);
                sqlDataReader = command.ExecuteReader();
                if(sqlDataReader.Read())
                {
                    labelStoreName.Text = sqlDataReader["store"].ToString();
                    labelAddress.Text = sqlDataReader["address"].ToString();
                } else
                {
                    labelStoreName.Visible = false;
                    labelAddress.Visible = false;
                    panel6.BackColor = Color.White;
                }
                sqlDataReader.Close();
                connection.Close();
            }catch(Exception)
            {
                connection.Close();
            }
        }

        public void LoadChart()
        {
            try
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select Year(date) as year,isnull(sum(total),0) as total from cart where status='Sold' group by Year(date)", connection);
                DataSet dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Sales");
                chart1.DataSource = dataSet.Tables["Sales"];
                Series series1 = chart1.Series["Series1"];
                series1.ChartType = SeriesChartType.Doughnut;

                series1.Name = "SALES";

                var chart = chart1;
                chart.Series[series1.Name].XValueMember = "year";
                chart.Series[series1.Name].YValueMembers = "total";
                chart.Series[0].IsValueShownAsLabel = true;
                chart.Series[0].LabelFormat = "₱{#,##0.00}";

                connection.Close();
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
