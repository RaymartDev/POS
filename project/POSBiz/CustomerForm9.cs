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
    public partial class CustomerForm9 : Form
    {
        CustomerForm1 customerForm1;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        String title = "Paboritos POS System";
        public CustomerForm9(CustomerForm1 customerForm1)
        {
            InitializeComponent();
            this.customerForm1 = customerForm1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(old.Text == String.Empty || newPass.Text == String.Empty || newPassConfirm.Text == String.Empty)
            {
                return;
            }
            string oldPass = HashUtil.HashPassword(old.Text);
            if(newPass.Text != newPassConfirm.Text)
            {
                MessageBox.Show("New password did not match!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string newPassword = HashUtil.HashPassword(newPass.Text);

            if(newPassword == oldPass)
            {
                MessageBox.Show("New password should not be the same as your old password!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("select * from users where username=@username and password=@password", connection);
                command.Parameters.AddWithValue("@username", customerForm1.labelUser.Text);
                command.Parameters.AddWithValue("@password", oldPass);
                sqlDataReader = command.ExecuteReader();
                sqlDataReader.Read();

                if(sqlDataReader.HasRows)
                {
                    sqlDataReader.Close();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("update users set password=@new where username=@username and password=@password", connection);
                    command.Parameters.AddWithValue("@new", newPassword);
                    command.Parameters.AddWithValue("@username", customerForm1.labelUser.Text);
                    command.Parameters.AddWithValue("@password", oldPass);
                    command.ExecuteNonQuery();
                    connection.Close();
                    this.Dispose();
                    MessageBox.Show("Password Successfully Changed!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    sqlDataReader.Close();
                    connection.Close();

                    MessageBox.Show("Old password is incorrect!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
