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
    public partial class frmLogin : Form
    {
        SqlConnection connection;
        public frmLogin()
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
        private UserofEmployee user;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            string query = "select * from Employee where Username = @user and Password = @Pass";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@user", SqlDbType.VarChar).Value = username;
            cmd.Parameters.AddWithValue("@pass", SqlDbType.VarChar).Value = password;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int Id = Convert.ToInt32(reader["EmployeeCode"].ToString());
                string Name = reader["EmployeeName"].ToString();
                string Username = reader["Username"].ToString();
                string Access = reader["Position"].ToString();
                user = new UserofEmployee(Id, Name, Username, Access);

                frmDashBoard dashboard = new frmDashBoard(user);
                dashboard.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Wrong user or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            if(txtPassword.UseSystemPasswordChar == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}
