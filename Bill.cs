using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class frmBill : Form
    {
        SqlConnection connection;
        private UserofEmployee user;
        public frmBill(UserofEmployee user)
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
            this.user = user;
            txtCreatedBy.Text = this.user.Name;
        }
        public void GetCustomer()
        {
            string query = "Select CustomerCode, CustomerName from Customer";
            connection.Open();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(table);
            cboSelectCustomer.DataSource = table;
            cboSelectCustomer.DisplayMember = "CustomerName";
            cboSelectCustomer.ValueMember = "CustomerCode";
            connection.Close();
        }

        private void Bill_Load(object sender, EventArgs e)
        {
            GetCustomer();
        }

        List<AddProduct> addProducts = new List<AddProduct>();
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmBillProduct frmBillProduct = new frmBillProduct();
            frmBillProduct.ShowDialog();

            addProducts.Add(frmBillProduct.SelectedProduct);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ProductCode");
            dataTable.Columns.Add("ProductName");
            dataTable.Columns.Add("InventoryQuantity");
            dataTable.Columns.Add("SellingPrice");
            dataTable.Columns.Add("TotalAmount");
            foreach (var product in addProducts)
            {
                if (product != null)
                {
                    dataTable.Rows.Add(product.ProductID, product.ProductName, product.Quantity, product.Price, product.TotalAmount);               
                } 
            }
            dgvBill.DataSource = dataTable;
            txtTotalAmount.Text = CalculateSum().ToString();

        }

        private decimal CalculateSum()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                if (row.Cells["ToatlAmount"].Value != null)
                {
                    decimal amount = Convert.ToDecimal(row.Cells["ToatlAmount"].Value);
                    totalAmount += amount;
                }
            }
            return totalAmount;
        }

        private void InsertInvoice()
        {
            string queryPur = "INSERT INTO Purchase OUTPUT INSERTED.PurchaseCode VALUES (@CusID, @PurDate)";
            connection.Open();
            SqlCommand cmd1 = new SqlCommand(queryPur, connection);
            cmd1.Parameters.Add("@CusID", SqlDbType.Int).Value = cboSelectCustomer.SelectedValue.ToString();
            cmd1.Parameters.Add("@PurDate", SqlDbType.Date).Value = dtmInvoice.Value;
            int purID = (int)cmd1.ExecuteScalar();

            if(purID == 0)
            {
                MessageBox.Show("Error output PurchaseID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string queryDetails = "INSERT INTO PurchaseDetails VALUES(@PurID, @EmpID, @ProID, @Quantity)";
            SqlCommand cmd2 = new SqlCommand(queryDetails, connection);
            int check = 0;
            foreach(var product in addProducts)
            {
                if (product != null)
                {
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.Add("@PurID", SqlDbType.Int).Value = purID;
                    cmd2.Parameters.Add("@EmpID", SqlDbType.Int).Value = user.Id;
                    cmd2.Parameters.Add("@ProID", SqlDbType.Int).Value = product.ProductID;
                    cmd2.Parameters.Add("@Quantity", SqlDbType.Int).Value = product.Quantity;
                    check = cmd2.ExecuteNonQuery();
                }
            }
            if(check > 0) 
            {
                MessageBox.Show("Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Fail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();

        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            int error = 0;
            if (cboSelectCustomer.SelectedValue == null)
            {
                error++;
                MessageBox.Show("Customer can't be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtCreatedBy.Text))
            {
                error++;
                MessageBox.Show("Bill must be created by someone", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgvBill.Rows.Count == 0 || dgvBill.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
            {
                MessageBox.Show("No products have been added yet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            InsertInvoice();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTotalAmount.Text = string.Empty;
        }

        private void dgvBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvBill.SelectedRows.Count == 0)
            {
                return;
            }
            if (e.KeyCode == Keys.Delete)
            {
                int id = Convert.ToInt32(dgvBill.CurrentRow.Cells["ProductID"].Value.ToString());
                AddProduct removeProduct = addProducts.Find(p => p.ProductID == id);
                if (removeProduct != null)
                {
                    addProducts.Remove(removeProduct);
                }
            }
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ProductCode");
            dataTable.Columns.Add("ProductName");
            dataTable.Columns.Add("InventoryQuantity");
            dataTable.Columns.Add("SellingPrice");
            dataTable.Columns.Add("TotalAmount");
            foreach (var product in addProducts)
            {
                if (product != null)
                {
                    dataTable.Rows.Add(product.ProductID, product.ProductName, product.Quantity, product.Price, product.TotalAmount);
                }
            }
            dgvBill.DataSource = dataTable;
        }

        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvBill.Rows[e.RowIndex].Selected=true;
        }
    }
}
