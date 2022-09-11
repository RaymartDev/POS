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
    public partial class CustomerForm7 : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        DBConnection dbConnection = new DBConnection();
        CustomerForm6 customerForm6;
        public CustomerForm7(CustomerForm6 customerForm6)
        {
            InitializeComponent();
            connection = new SqlConnection(dbConnection.MyConnection());
            this.customerForm6 = customerForm6;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(comboInventory.Text == String.Empty || txtCancelQuantity.Text == String.Empty || txtReason.Text == String.Empty)
            {
                return;
            }
            CustomerForm8 customerForm8 = new CustomerForm8(this);
            customerForm8.ShowDialog();
        }

        public void RefreshList()
        {
            customerForm6.LoadRecord();
        }

        private void CustomerForm7_Load(object sender, EventArgs e)
        {

        }

        private void txtPCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDisc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQTY_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTrans_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCancelQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboInventory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
