using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class frmShowBill : Form
    {
        private UserofEmployee user;
        SqlConnection connection;
        public frmShowBill(UserofEmployee user)
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
            this.user = user;
        }

        public void FillData()
        {
            string query = @"
                            SELECT
                                PurchaseDetails.PurchaseCode,
                                CUSTOMER.CustomerName,
                                Product.ProductName,
                                Employee.EmployeeName,
                                PurchaseDetails.Quantity,
                                PurchaseDetails.Quantity * Product.SellingPrice AS TotalPrice
                            FROM
                                PurchaseDetails
                            JOIN 
                                Purchase ON Purchase.PurchaseCode = PurchaseDetails.PurchaseCode
                            JOIN
                                CUSTOMER ON CUSTOMER.CustomerCode = Purchase.CustomerCode
                            JOIN
                                Product ON Product.ProductCode = PurchaseDetails.ProductCode
                            JOIN
                                Employee ON Employee.EmployeeCode = PurchaseDetails.EmployeeCode";

            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(tbl);
            dgvAllBill.DataSource = tbl;
            connection.Close();
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            frmBill bill = new frmBill(user);
            bill.ShowDialog();
        }

        private void frmShowBill_Load(object sender, EventArgs e)
        {
            connection.Open();
            FillData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string dropQuery = @"DROP TABLE PurchaseDetails
                                 DROP TABLE Purchase
                                 CREATE TABLE Purchase(
                                 PurchaseCode INT PRIMARY KEY IDENTITY(1,1), 
                                 CustomerCode INT FOREIGN KEY REFERENCES Customer(CustomerCode),
                                 PurchaseDate DATE
                                 )

                                 CREATE TABLE PurchaseDetails(
                                 PurchaseDetailID  INT PRIMARY KEY IDENTITY(1,1),
                                 PurchaseCode INT FOREIGN KEY REFERENCES Purchase(PurchaseCode),
                                 EmployeeCode INT FOREIGN KEY REFERENCES Employee(EmployeeCode),
                                 ProductCode INT FOREIGN KEY REFERENCES Product(ProductCode),
                                 Quantity INT
                                 )";
            connection.Open();
            SqlCommand cmd = new SqlCommand(dropQuery, connection);
            cmd.ExecuteNonQuery();
            FillData();
        }
    }
}
