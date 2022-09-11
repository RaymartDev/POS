using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace POSBiz
{
    
    public class DBConnection
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        private double dailySales;
        private int productLine, stocks, criticalItems;
        private string con;
        public string MyConnection()
        {
            con = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            return con;
        }

        public double DailySales()
        {
            string date = DateTime.Now.ToShortDateString();
            connection = new SqlConnection();
            connection.ConnectionString = con;
            try
            {
                connection.Open();

                command = new SqlCommand("select isnull(sum(total),0) as total from cart where date between '" + date + "' and '" + date + "' and status='Sold'", connection);
                dailySales = double.Parse(command.ExecuteScalar().ToString());

                connection.Close();
            }
            catch (Exception) { 
            }
            return dailySales;
        }

        public int ProductLine()
        {
            connection = new SqlConnection();
            connection.ConnectionString = con;
            try
            {
                connection.Open();

                command = new SqlCommand("select count(*) from product", connection);
                productLine = int.Parse(command.ExecuteScalar().ToString());

                connection.Close();
            }
            catch (Exception)
            {
            }
            return productLine;
        }

        public int Stocks()
        {
            connection = new SqlConnection();
            connection.ConnectionString = con;
            try
            {
                connection.Open();

                command = new SqlCommand("select isnull(sum(quantity), 0) as quantity from product", connection);
                stocks = int.Parse(command.ExecuteScalar().ToString());

                connection.Close();
            }
            catch (Exception)
            {
            }
            return stocks;
        }

        public int CriticalItems()
        {
            connection = new SqlConnection();
            connection.ConnectionString = con;
            try
            {
                connection.Open();

                command = new SqlCommand("select count(*) from vwCriticalItems", connection);
                criticalItems = int.Parse(command.ExecuteScalar().ToString());

                connection.Close();
            }
            catch (Exception)
            {
            }
            return criticalItems;
        }
    }
}
