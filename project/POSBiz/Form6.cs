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
    public partial class Form6 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        SqlDataReader sqlDataReader;
        public Form6()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            LoadAccountsToDelete();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(textPass.Text != textConfirm.Text)
            {
                MessageBox.Show("Password did not match!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("insert into users (username, password, role, name) values (@username, @password, @role, @name)", connection);
                command.Parameters.AddWithValue("@username", textUser.Text);
                command.Parameters.AddWithValue("@password", HashUtil.HashPassword(textPass.Text));
                command.Parameters.AddWithValue("@role", textRole.Text);
                command.Parameters.AddWithValue("@name", textName.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("User has been added successfully", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                Clear();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        private void Clear()
        {
            textUser.Clear();
            textPass.Clear();
            textConfirm.Clear();
            textRole.Text = "";
            textName.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textPass.Text != textConfirm.Text)
            {
                MessageBox.Show("Password did not match!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("select * from users where username='" + textUser.Text + "'", connection);
                sqlDataReader = command.ExecuteReader();
                sqlDataReader.Read();
                if(sqlDataReader.HasRows)
                {
                    connection.Close();
                    sqlDataReader.Close();
                    string password = HashUtil.HashPassword(changeTextPassword.Text);
                    connection.Open();
                    command = new SqlCommand("update users set password='" + password + "'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    connection.Close();
                    sqlDataReader.Close();
                    MessageBox.Show("This is not an existing username!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        private void ClearChange()
        {
            userBox.Text = "";
            changeTextPassword.Clear();
            changeTextConfirm.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(accountBox.Text == String.Empty)
            {
                return;
            }
            if(MessageBox.Show("Are you sure you want to delete this user?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    connection.Open();
                    command = new SqlCommand("delete from users where username = @username", connection);
                    command.Parameters.AddWithValue("@username", accountBox.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Successfully deleted user", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    accountBox.Text = "";
                    accountBox.Items.Clear();
                    LoadAccountsToDelete();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadAccountsToDelete()
        {
            accountBox.Items.Clear();
            userBox.Items.Clear();
            activeBox.Items.Clear();
            try
            {
                connection.Open();
                command = new SqlCommand("select * from users", connection);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    string user = sqlDataReader["username"].ToString();
                    accountBox.Items.Add(user);
                    userBox.Items.Add(user);
                    activeBox.Items.Add(user);
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

        private void accountBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearChange();
        }

        private void userBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(activeBox.Text == String.Empty)
            {
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("select * from users where username='" + activeBox.Text + "'", connection);
                sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    string activeStatus = sqlDataReader["active"].ToString();
                    if(activeStatus == "Y")
                    {
                        MessageBox.Show("This user is already active!", title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        sqlDataReader.Close();
                        connection.Close();
                        return;
                    }

                    sqlDataReader.Close();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("update users set active='Y' where username='" + activeBox.Text + "'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("User has been activated!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Unable to find that user!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sqlDataReader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (activeBox.Text == String.Empty)
            {
                return;
            }
            try
            {
                connection.Open();
                command = new SqlCommand("select * from users where username='" + activeBox.Text + "'", connection);
                sqlDataReader = command.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    string activeStatus = sqlDataReader["active"].ToString();
                    if (activeStatus == "N")
                    {
                        MessageBox.Show("This user is already inactive!", title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        sqlDataReader.Close();
                        connection.Close();
                        return;
                    }

                    sqlDataReader.Close();
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("update users set active='N' where username='" + activeBox.Text + "'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("User has been deactivated!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("Unable to find that user!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sqlDataReader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void activeBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
