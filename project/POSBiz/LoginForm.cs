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
    public partial class LoginForm : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        SqlDataReader sqlDataReader;
        string title = "Paboritos POS System";
        public LoginForm()
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(textUser.Text == String.Empty || textPass.Text == String.Empty)
            {
                return;
            }
            bool found = false;
            string role = "", name = "";
            String user = textUser.Text;
            String password = HashUtil.HashPassword(textPass.Text);
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT count(*) FROM users", connection);
                int result = int.Parse(command.ExecuteScalar().ToString());
                if(result <= 0)
                {
                    connection.Close();

                    connection.Open();
                    command = new SqlCommand("INSERT INTO users (username, password, role, name) VALUES (@username, @password, @role, @name)", connection);
                    command.Parameters.AddWithValue("@username", "PABORITOS");
                    command.Parameters.AddWithValue("@password", HashUtil.HashPassword("admin"));
                    command.Parameters.AddWithValue("@role", "SuperAdmin");
                    command.Parameters.AddWithValue("@name", "Paboritos");
                    command.ExecuteNonQuery();

                    Form1 form1 = new Form1();
                    this.Hide();
                    form1.labelUser.Text = "PABORITOS";
                    form1.labelName.Text = "Paboritos";
                    form1.ShowDialog();
                    connection.Close();
                    return;
                }
                connection.Close();


                connection.Open();
                command = new SqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", connection);
                command.Parameters.AddWithValue("@username", user);
                command.Parameters.AddWithValue("@password", password);
                sqlDataReader = command.ExecuteReader();
                sqlDataReader.Read();
                if(sqlDataReader.HasRows)
                {
                    found = true;
                    role = sqlDataReader["role"].ToString();
                    name = sqlDataReader["name"].ToString();

                    if(sqlDataReader["active"].ToString() == "N")
                    {
                        MessageBox.Show("Your account is currently disabled!. Please contact your administrator.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sqlDataReader.Close();
                        connection.Close();
                        return;
                    }

                } else
                {
                    found = false;
                }
                sqlDataReader.Close();
                connection.Close();

                if(found)
                {
                    if (role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + name + "!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CustomerForm1 form1 = new CustomerForm1();
                        textPass.Clear();
                        textUser.Clear();
                        this.Hide();
                        form1.labelUser.Text = user;
                        form1.labelName.Text = name;
                        form1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Welcome " + name + "!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form1 form1 = new Form1();
                        textPass.Clear();
                        textUser.Clear();
                        this.Hide();
                        form1.labelUser.Text = user;
                        form1.labelName.Text = name;
                        form1.ShowDialog();

                    }
                } else
                {
                    if(MessageBox.Show("Incorrect Information!", title, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        textPass.Clear();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                btnSave_Click(sender, e);
            }
        }

        private void textPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSave_Click(sender, e);
            }
        }
    }
}
