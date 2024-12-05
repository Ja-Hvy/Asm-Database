using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Database
{
    public partial class frmBillProduct : Form
    {
        SqlConnection connection;
        public frmBillProduct()
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
        }


        public void FillData()
        {
            string query = "SELECT ProductCode, ProductName, InventoryQuantity, SellingPrice FROM Product";
            
            DataTable dataTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(dataTable);
            dgvProductList.DataSource = dataTable;
            connection.Close();

        }



        private void frmBillProduct_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void CheckQuantityAd(out int quantity)
        {
            quantity = 0;
            string check = txtQuantity.Text;
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Quantity add can't be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try 
            {
                quantity = Convert.ToInt32(txtQuantity.Text);
                int inven = Convert.ToInt32(txtInventory.Text);
                if(quantity < 0)
                {
                    MessageBox.Show("The store does not have a debt purchase policy", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (quantity > inven) 
                {
                    MessageBox.Show("Quantity add can't higher than inventory", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }               
            }
            catch
            {
                MessageBox.Show("Quantity add must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dgvProductList.Rows[e.RowIndex];
                row.Selected = true;
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtInventory.Text = row.Cells["Quantity"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
            }
            catch
            {
                return;
            }
        }
        public AddProduct SelectedProduct { get; private set; }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckQuantityAd(out int quantity);
            if (quantity == 0) 
            {
                return;
            }

            if (dgvProductList.SelectedRows.Count == 0)
            {
                MessageBox.Show("You aren't choose product yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int id = Convert.ToInt32(dgvProductList.CurrentRow.Cells[0].Value.ToString());
                string name = txtProductName.Text;                
                decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
                decimal price  = Convert.ToDecimal(txtPrice.Text);

                SelectedProduct = new AddProduct(id, name, quantity, price, totalAmount);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string Name = txtSearch.Text;

            string searchName = @"
                    SELECT 
                        ProductCode,
                        ProductName,
                        InventoryQuantity,
                        SellingPrice
                    FROM
                        Product                              
                    WHERE
                        ProductName LIKE @searchName";

            connection.Open();

            SqlDataAdapter adapterName = new SqlDataAdapter(searchName, connection);
            adapterName.SelectCommand.Parameters.AddWithValue("@searchName", "%" + Name + "%");
            DataTable tbl = new DataTable();
            adapterName.Fill(tbl);
            if (tbl != null && tbl.Rows.Count > 0)
            {
                dgvProductList.DataSource = tbl;
            }
            else
            {
                FillData();

            }
            connection.Close();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CheckQuantityAd(out int quantity);

            decimal price = Convert.ToDecimal(txtPrice.Text);

            txtTotalAmount.Text = (quantity * price).ToString();
        }
    }
}
