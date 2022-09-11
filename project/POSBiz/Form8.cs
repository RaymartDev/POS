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
    public partial class Form8 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        public Form8()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        

        private int GetStoreInfo()
        {
            int count = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select count(*) from storeTable", connection);
                count = int.Parse(command.ExecuteScalar().ToString());
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return count;
            }

            return count;
        }

        public void LoadRecords()
        {
            try
            {
                connection.Open();
                command = new SqlCommand("select * from storeTable", connection);
                sqlDataReader = command.ExecuteReader();
                sqlDataReader.Read();
                if(sqlDataReader.HasRows)
                {
                    addressBox.Text = sqlDataReader["address"].ToString();
                    storeBox.Text = sqlDataReader["store"].ToString();
                } else
                {
                    addressBox.Clear();
                    storeBox.Clear();
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

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(storeBox.Text == String.Empty && addressBox.Text == String.Empty)
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand("truncate table storeTable", connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully removed store details!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    connection.Close();
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            try
            {
                
                
                if(GetStoreInfo() > 0)
                {
                    connection.Open();
                    command = new SqlCommand("update storeTable set store=@store, address=@address", connection);
                    command.Parameters.AddWithValue("@store", storeBox.Text);
                    command.Parameters.AddWithValue("@address", addressBox.Text);
                    command.ExecuteNonQuery();
                } else
                {
                    connection.Open();
                    command = new SqlCommand("insert into storeTable (store, address) values (@store, @address)", connection);
                    command.Parameters.AddWithValue("@store", storeBox.Text);
                    command.Parameters.AddWithValue("@address", addressBox.Text);
                    command.ExecuteNonQuery();
                }
                this.Dispose();
                MessageBox.Show("Successfully saved store details!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
