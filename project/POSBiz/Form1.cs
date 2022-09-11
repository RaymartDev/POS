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
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        string title = "Paboritos POS System";
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            NotifyCriticalItems();
            LoadDashboard();
        }

        public void NotifyCriticalItems()
        {
            string critical = "";
            int i = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select * from vwCriticalItems", connection);
                sqlDataReader = command.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    i += 1;
                    critical += i + ". " + sqlDataReader["description"].ToString() + Environment.NewLine;
                }
                sqlDataReader.Close();
                connection.Close();

                if(i > 0)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 formProduct = new Form2();
            formProduct.LoadRecords();
            formProduct.TopLevel = false;
            panelScreen.Controls.Add(formProduct);
            formProduct.BringToFront();
            formProduct.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form4 formStockIn = new Form4();
            formStockIn.LoadStocks();
            formStockIn.TopLevel = false;
            panelScreen.Controls.Add(formStockIn);
            formStockIn.BringToFront();
            formStockIn.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool superAdmin = false;
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM users WHERE username=@username", connection);
                command.Parameters.AddWithValue("@username", labelUser.Text);
                sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader["role"].ToString().ToLower() == "superadmin")
                    {
                        superAdmin = true;
                    }
                    sqlDataReader.Close();
                    connection.Close();
                }
                sqlDataReader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
            if(superAdmin)
            {
                Form6 form6 = new Form6();
                form6.TopLevel = false;
                panelScreen.Controls.Add(form6);
                form6.BringToFront();
                form6.Show();
            }
            else
            {
                MessageBox.Show("I'm sorry, you are not permitted to do that", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CustomerForm6 customerForm6 = new CustomerForm6();
            customerForm6.LoadRecord();
            customerForm6.user = labelUser.Text;
            customerForm6.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 form6 = new Form7();
            form6.ShowDialog();
        }

        private void LoadDashboard()
        {
            Form9 form6 = new Form9();
            form6.TopLevel = false;
            form6.labelDailySales.Text = dbConnection.DailySales().ToString("#,##0.00");
            form6.labelProductLine.Text = dbConnection.ProductLine().ToString("#,##0");
            form6.labelStocks.Text = dbConnection.Stocks().ToString("#,##0");
            form6.labelCriticalItems.Text = dbConnection.CriticalItems().ToString("#,##0");
            panelScreen.Controls.Add(form6);
            form6.BringToFront();
            form6.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 form6 = new Form9();
            form6.TopLevel = false;
            form6.labelDailySales.Text = dbConnection.DailySales().ToString("#,##0.00");
            form6.labelProductLine.Text = dbConnection.ProductLine().ToString("#,##0");
            form6.labelStocks.Text = dbConnection.Stocks().ToString("#,##0");
            form6.labelCriticalItems.Text = dbConnection.CriticalItems().ToString("#,##0");
            panelScreen.Controls.Add(form6);
            form6.BringToFront();
            form6.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form11 form6 = new Form11();
            form6.LoadRecords();
            form6.TopLevel = false;
            panelScreen.Controls.Add(form6);
            form6.BringToFront();
            form6.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            Form12 form6 = new Form12();
            form6.txtUser.Text = labelUser.Text;
            form6.TopLevel = false;
            panelScreen.Controls.Add(form6);
            form6.BringToFront();
            form6.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool superAdmin = false;
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT * FROM users WHERE username=@username", connection);
                command.Parameters.AddWithValue("@username", labelUser.Text);
                sqlDataReader = command.ExecuteReader();
                if(sqlDataReader.Read())
                {
                    if (sqlDataReader["role"].ToString().ToLower() == "superadmin")
                    {
                        superAdmin = true;
                    }
                    sqlDataReader.Close();
                    connection.Close();
                }
                sqlDataReader.Close();
                connection.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }

            if(superAdmin)
            {
                Form14 form6 = new Form14();
                form6.TopLevel = false;
                panelScreen.Controls.Add(form6);
                form6.BringToFront();
                form6.Show();
            } else
            {
                MessageBox.Show("I'm sorry, you are not permitted to do that", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
