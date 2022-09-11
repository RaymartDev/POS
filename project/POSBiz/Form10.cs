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
    public partial class Form10 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        String title = "Paboritos POS System";
        Form11 form11;
        public Form10(Form11 form11)
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            this.form11 = form11;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                command = new SqlCommand("insert into vendor(vendor, address, contactperson, telephone, email) values (@vendor, @address, @contactperson, @telephone, @email)", connection);
                command.Parameters.AddWithValue("@vendor", txtVendor.Text);
                command.Parameters.AddWithValue("@address", txtAddress.Text);
                command.Parameters.AddWithValue("@contactperson", txtContact.Text);
                command.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("RECORD HAS BEEN SAVED SUCCESSFULLY!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                this.Dispose();
                form11.LoadRecords();
            }catch(Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear()
        {
            txtVendor.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtTelephone.Clear();
            txtEmail.Clear();
            txtVendor.Focus();
            btnSave.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm to update", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    connection.Open();
                    command = new SqlCommand("UPDATE vendor SET vendor=@vendor, address=@address, contactperson=@contactperson, telephone=@telephone, email=@email WHERE id=@id", connection);
                    command.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    command.Parameters.AddWithValue("@address", txtAddress.Text);
                    command.Parameters.AddWithValue("@contactperson", txtContact.Text);
                    command.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    command.Parameters.AddWithValue("@email", txtEmail.Text);
                    command.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Updated", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form11.LoadRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }
    }
}
