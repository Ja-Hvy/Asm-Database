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
    public partial class frmProduct : Form
    {
        SqlConnection connection;
        public frmProduct()
        {
            InitializeComponent();
            connection = new SqlConnection("Server=DESKTOP-6Q1VRGD;Database=StoreX;Integrated Security = true");
        }

        public void FillData()
        {
            string query = "SELECT * FROM Product";
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(tbl);
            dgvProduct.DataSource = tbl;
            connection.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string ID = txtProductID.Text;
            string Name = txtProductName.Text;
            string Price = txtPrice.Text;
            string Quantity = txtQuantity.Text;
            string Mess = "";
            int Error = 0;
            if (ID == "" || Name == "" || Price == "" || Quantity == "")
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
                if (int.TryParse(Price, out Error) == false)
                {
                    Error++;
                    Mess += "Price ";
                }
                if (int.TryParse(Quantity, out Error) == false)
                {
                    Error++;
                    Mess += "Quantity ";
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
                string insertOrEdit = "Insert into Product (ProductCode, ProductName, SellingPrice, InventoryQuantity, ProductImage) values (@ID, @Name, @Price, @Quantity, @Image)";
                connection.Open();

                SqlCommand cmd = new SqlCommand(insertOrEdit, connection);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = Price;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;

                if (picImage.Image != null)
                {
                    byte[] imageBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        imageBytes = ms.ToArray();
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary);
                        cmd.Parameters["@Image"].Value = imageBytes;
                    }
                }
                else
                {
                    cmd.Parameters.Add("@Image", SqlDbType.VarBinary);
                    cmd.Parameters["@Image"].Value = DBNull.Value;
                }

                cmd.ExecuteNonQuery();
                FillData();
                MessageBox.Show("Insert successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            connection.Open();
            FillData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(txtProductID.Text);
            int Price = Convert.ToInt32(txtPrice.Text);
            int Quantity = Convert.ToInt32(txtQuantity.Text);
            string Name = txtProductName.Text;
            if (dgvProduct != null)
            {
                if (MessageBox.Show(this, "Are you sure to update?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string update = "update Product set ProductName = @ProductName, SellingPrice = @Price, InventoryQuantity = @Quantity, ProductImage = @Image"
                        + " where ProductCode = @ProductID";
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(update, connection);
                    cmd.Parameters.Add("@ProductID", SqlDbType.VarChar).Value = ID;
                    cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Name;
                    cmd.Parameters.Add("@Price", SqlDbType.VarChar).Value = Price;
                    cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = Quantity;

                    if (picImage.Image != null)
                    {
                        byte[] imageBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            picImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageBytes = ms.ToArray();
                            cmd.Parameters.Add("@Image", SqlDbType.VarBinary);
                            cmd.Parameters["@Image"].Value = imageBytes;
                        }
                    }
                    else
                    {
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary);
                        cmd.Parameters["@Image"].Value = DBNull.Value;
                    }

                    cmd.Parameters.Add("@ProductCode", SqlDbType.Int).Value = dgvProduct.CurrentRow.Cells["ProductID"].Value;
                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        FillData();
                        MessageBox.Show("Update successfully");
                    }
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    txtImage.Text = path;
                    picImage.ImageLocation = openFileDialog.FileName;
                    picImage.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtImage.Text = string.Empty;
            picImage.Image = null;
        }

        private void dgvProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvProduct.CurrentRow != null)
            {
                if (MessageBox.Show("Are you sure want to delete this?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    int ProductID = Convert.ToInt32(dgvProduct.CurrentRow.Cells["ProductID"].Value);

                    connection.Open();
                    string deleteQuery = "DELETE FROM Product WHERE ProductCode = @ProductID";
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

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

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvProduct.Rows[e.RowIndex];
            txtProductID.Text = row.Cells[0].Value.ToString();
            txtProductName.Text = row.Cells[1].Value.ToString();
            txtPrice.Text = row.Cells[2].Value.ToString();
            txtQuantity.Text = row.Cells[3].Value.ToString();
            txtImage.Text = row.Cells[4].Value.ToString();
            picImage.Image = row.Cells[4].FormattedValue as Image;
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch != null)
            {
                string Search = "Select * From Product where ProductName like @name";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(Search, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@name", "%" + txtSearch.Text + "%");
                DataTable tbl = new DataTable();
                adapter.Fill(tbl);
                dgvProduct.DataSource = tbl;
                connection.Close();
                dgvProduct.DataSource = tbl.Rows.Count > 0 ? tbl : null;
                if (tbl.Rows.Count == 0)
                {
                    MessageBox.Show("The letter is not in the current list!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
