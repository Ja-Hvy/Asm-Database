using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database
{
    public partial class frmDashBoard : Form
    {
        private UserofEmployee user;
        public frmDashBoard(UserofEmployee user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenForm(new frmCustomer(), sender);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenForm(new frmProduct(), sender);
        }

        private Form openForm;
        private void OpenForm(Form childForm, object btnOpen)
        {
            if (openForm != null)
                openForm.Close();
                openForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                this.pnlDesktop.Controls.Add(childForm);
                this.pnlDesktop.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenForm(new frmEmployee(), sender);
        }

        private void btnShowBill_Click(object sender, EventArgs e)
        {
            OpenForm(new frmShowBill(user), sender);
        }
    }
}
