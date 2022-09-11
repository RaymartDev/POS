using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace POSBiz
{
    public partial class Form13 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        public Form13()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadChartSold(string firstDate, string secondDate)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                sqlDataAdapter = new SqlDataAdapter("select p.description, sum(c.total) as total from cart as c inner join product as p on c.pcode = p.pcode where status = 'Sold' and date between '" + firstDate + "' and '" + secondDate + "' group by p.description", connection);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "SOLD");
                chart1.DataSource = dataSet.Tables["SOLD"];
                Series series = chart1.Series[0];
                series.ChartType = SeriesChartType.Column;

                series.Name = "SOLD ITEMS";
                chart1.Series[0].XValueMember = "description";
                chart1.Series[0].YValueMembers = "total";
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Series[0].LabelFormat = "₱{#,##0.00}";
            }
            catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
