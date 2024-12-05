using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class frmEmployee : Form
    {
        SqlConnection connection;
        public frmEmployee()
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
        }
        public void FillData()
        {
            string query = "SELECT * FROM Employee";
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(tbl);
            dgvEmployee.DataSource = tbl;
            connection.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string ID = txtEmployeeID.Text;
            string Name = txtEmployeeName.Text;
            string Position = txtPosition.Text;
            string Authority = cboAuthority.Text;
            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;
            string Mess = "";
            int Error = 0;
            if (ID == "" || Name == "" || Position == "" || Authority == "" || UserName == "" || Password == "")
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
                    MessageBox.Show($"{Mess}must be number");
                }
                else
                {
                    Error = 0;
                }
            }
            if (Error == 0)
            {
                string insertOrEdit = "Insert into Employee (EmployeeCode, EmployeeName, Position, Authority, UserName, Password) values (@ID, @Name, @Position, @Authority, @UserName, @Password)";
                connection.Open();

                SqlCommand cmd = new SqlCommand(insertOrEdit, connection);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                cmd.Parameters.Add("@Position", SqlDbType.VarChar).Value = Position;
                cmd.Parameters.Add("@Authority", SqlDbType.VarChar).Value = Authority;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;

                cmd.ExecuteNonQuery();
                FillData();
                MessageBox.Show("Insert successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            connection.Open();
            FillData();
            cboAuthority.SelectedItem = "Employee";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(txtEmployeeID.Text);
            string Name = txtEmployeeName.Text;
            string Position = txtPosition.Text;
            string Authority = cboAuthority.Text;
            string UserName = txtUserName.Text;
            string Password = txtPassword.Text;
            if (dgvEmployee != null)
            {
                if (MessageBox.Show(this, "Are you sure to update?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string update = "update Employee set EmployeeName = @EmployeeName, Position = @Position, Authority = @Authority, UserName = @UserName, Password = @Password"
                        + " where EmployeeCode = @EmployeeID";
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(update, connection);
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = ID;
                    cmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@Position", SqlDbType.VarChar).Value = Position;
                    cmd.Parameters.Add("@Authority", SqlDbType.VarChar).Value = Authority;
                    cmd.Parameters.Add("Password", SqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add("UserName", SqlDbType.VarChar).Value = UserName;

                    cmd.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = ID;
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

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];
            txtEmployeeID.Text = row.Cells[0].Value.ToString();
            txtEmployeeName.Text = row.Cells[1].Value.ToString();
            txtPosition.Text = row.Cells[2].Value.ToString();
            cboAuthority.Text = row.Cells[3].Value.ToString();
            txtUserName.Text = row.Cells[4].Value.ToString();
            txtPassword.Text = row.Cells[5].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch != null)
            {
                string Search = "Select * From Employee where EmployeeName like @name";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(Search, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + txtSearch.Text + "%");
                DataTable tbl = new DataTable();
                adapter.Fill(tbl);
                dgvEmployee.DataSource = tbl;
                connection.Close();
                dgvEmployee.DataSource = tbl.Rows.Count > 0 ? tbl : null;
                if (tbl.Rows.Count == 0)
                {
                    MessageBox.Show("The letter is not in the current list!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmployeeID.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtPosition.Text = string.Empty;
            cboAuthority.SelectedItem = "Employee";
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void dgvEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvEmployee.CurrentRow != null)
            {
                if (MessageBox.Show("Are you sure want to delete this?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int EmployeeID = Convert.ToInt32(dgvEmployee.CurrentRow.Cells["EmployeeID"].Value);

                    connection.Open();
                    string deleteQuery = "DELETE FROM EMployee WHERE EmployeeCode = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

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
    }
}
