using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class frmCustomer : Form
    {
        SqlConnection connection;
        public frmCustomer()
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
        }
        public void FillData()
        {
            string query = "SELECT * FROM Customer";
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(tbl);
            dgvCustomer.DataSource = tbl;
            connection.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string ID = txtCustomerID.Text;
            string Name = txtCustomerName.Text;
            string Phone = txtPhoneNumber.Text;
            string Address = txtAddress.Text;
            string Mess = "";
            int Error = 0;
            if (ID == "" || Name == "" || Phone == "" || Address == "")
            {
                Error++;
                MessageBox.Show("Information can't be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (int.TryParse(ID, out Error) == false)
                {
                    Error++;
                    Mess += "ID ";
                }
                if (int.TryParse(Phone, out Error) == false)
                {
                    Error++;
                    Mess += "Phone ";
                }
                if (Mess != "")
                {
                    MessageBox.Show($"{Mess}must be number");
                }
                else
                {
                    Error = 0;
                }
            }
            if (Error == 0)
            {
                string insertOrEdit = "Insert into Customer (CustomerCode, CustomerName, PhoneNumber, Address) values (@ID, @Name, @Phone, @Address)";
                connection.Open();

                SqlCommand cmd = new SqlCommand(insertOrEdit, connection);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                cmd.Parameters.Add("@Phone", SqlDbType.Decimal).Value = Phone;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;

                cmd.ExecuteNonQuery();
                FillData();
                MessageBox.Show("Insert successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            connection.Open();
            FillData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(txtCustomerID.Text);
            int Phone = Convert.ToInt32(txtPhoneNumber.Text);
            string Address = txtAddress.Text;
            string Name = txtCustomerName.Text;
            if (dgvCustomer != null)
            {
                if (MessageBox.Show(this, "Are you sure to update?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string update = "update Customer set CustomerName = @CustomerName, PhoneNumber = @Phone, Address = @Address"
                        + " where CustomerCode = @CustomerID";
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(update, connection);
                    cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar).Value = ID;
                    cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Phone;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;

                    cmd.Parameters.Add("@CustomerCode", SqlDbType.Int).Value = dgvCustomer.CurrentRow.Cells["CustomerID"].Value;
                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        FillData();
                        MessageBox.Show("Update successfully");
                    }
                    else
                    {
                        MessageBox.Show("False to update");
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerID.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtAddress.Text = string.Empty;

        }

        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvCustomer.CurrentRow != null)
            {
                if (MessageBox.Show("Are you sure want to delete this?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int CustomerID = Convert.ToInt32(dgvCustomer.CurrentRow.Cells["CustomerID"].Value);

                    connection.Open();
                    string deleteQuery = "DELETE FROM Customer WHERE CustomerCode = @CustomerID";
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                    cmd.ExecuteNonQuery();
                    FillData();
                    MessageBox.Show(this, "Deleted successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                FillData();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
            txtCustomerID.Text = row.Cells[0].Value.ToString();
            txtCustomerName.Text = row.Cells[1].Value.ToString();
            txtPhoneNumber.Text = row.Cells[2].Value.ToString();
            txtAddress.Text = row.Cells[3].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch != null)
            {
                string Search = "Select * From Customer where CustomerName like @name";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(Search, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + txtSearch.Text + "%");
                DataTable tbl = new DataTable();
                adapter.Fill(tbl);
                dgvCustomer.DataSource = tbl;
                connection.Close();
                dgvCustomer.DataSource = tbl.Rows.Count > 0 ? tbl : null;
                if (tbl.Rows.Count == 0)
                {
                    MessageBox.Show("The letter is not in the current list!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
