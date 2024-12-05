namespace Database
{
    partial class frmBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvBill = new System.Windows.Forms.DataGridView();
            this.gbCustomer = new System.Windows.Forms.GroupBox();
            this.cboSelectCustomer = new System.Windows.Forms.ComboBox();
            this.lblSelectCustomer = new System.Windows.Forms.Label();
            this.gbInvoiceDetails = new System.Windows.Forms.GroupBox();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.txtInvoiceCode = new System.Windows.Forms.TextBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblDateCreated = new System.Windows.Forms.Label();
            this.lblInvoiceCode = new System.Windows.Forms.Label();
            this.gbTotal = new System.Windows.Forms.GroupBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnSaveInvoice = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToatlAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtmInvoice = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).BeginInit();
            this.gbCustomer.SuspendLayout();
            this.gbInvoiceDetails.SuspendLayout();
            this.gbTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBill
            // 
            this.dgvBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductID,
            this.ProductName,
            this.Quantity,
            this.Price,
            this.ToatlAmount});
            this.dgvBill.Location = new System.Drawing.Point(12, 29);
            this.dgvBill.Name = "dgvBill";
            this.dgvBill.RowHeadersWidth = 51;
            this.dgvBill.RowTemplate.Height = 24;
            this.dgvBill.Size = new System.Drawing.Size(1037, 318);
            this.dgvBill.TabIndex = 0;
            // 
            // gbCustomer
            // 
            this.gbCustomer.Controls.Add(this.cboSelectCustomer);
            this.gbCustomer.Controls.Add(this.lblSelectCustomer);
            this.gbCustomer.Location = new System.Drawing.Point(12, 369);
            this.gbCustomer.Name = "gbCustomer";
            this.gbCustomer.Size = new System.Drawing.Size(279, 118);
            this.gbCustomer.TabIndex = 1;
            this.gbCustomer.TabStop = false;
            this.gbCustomer.Text = "Customer ";
            // 
            // cboSelectCustomer
            // 
            this.cboSelectCustomer.FormattingEnabled = true;
            this.cboSelectCustomer.Location = new System.Drawing.Point(10, 70);
            this.cboSelectCustomer.Name = "cboSelectCustomer";
            this.cboSelectCustomer.Size = new System.Drawing.Size(263, 28);
            this.cboSelectCustomer.TabIndex = 1;
            // 
            // lblSelectCustomer
            // 
            this.lblSelectCustomer.AutoSize = true;
            this.lblSelectCustomer.Location = new System.Drawing.Point(6, 36);
            this.lblSelectCustomer.Name = "lblSelectCustomer";
            this.lblSelectCustomer.Size = new System.Drawing.Size(134, 20);
            this.lblSelectCustomer.TabIndex = 0;
            this.lblSelectCustomer.Text = "Select Customer";
            // 
            // gbInvoiceDetails
            // 
            this.gbInvoiceDetails.Controls.Add(this.dtmInvoice);
            this.gbInvoiceDetails.Controls.Add(this.txtCreatedBy);
            this.gbInvoiceDetails.Controls.Add(this.txtInvoiceCode);
            this.gbInvoiceDetails.Controls.Add(this.lblCreatedBy);
            this.gbInvoiceDetails.Controls.Add(this.lblDateCreated);
            this.gbInvoiceDetails.Controls.Add(this.lblInvoiceCode);
            this.gbInvoiceDetails.Location = new System.Drawing.Point(312, 369);
            this.gbInvoiceDetails.Name = "gbInvoiceDetails";
            this.gbInvoiceDetails.Size = new System.Drawing.Size(279, 254);
            this.gbInvoiceDetails.TabIndex = 2;
            this.gbInvoiceDetails.TabStop = false;
            this.gbInvoiceDetails.Text = "Invoice Details";
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(10, 195);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(263, 27);
            this.txtCreatedBy.TabIndex = 2;
            // 
            // txtInvoiceCode
            // 
            this.txtInvoiceCode.ForeColor = System.Drawing.Color.Silver;
            this.txtInvoiceCode.Location = new System.Drawing.Point(10, 59);
            this.txtInvoiceCode.Name = "txtInvoiceCode";
            this.txtInvoiceCode.ReadOnly = true;
            this.txtInvoiceCode.Size = new System.Drawing.Size(263, 27);
            this.txtInvoiceCode.TabIndex = 2;
            this.txtInvoiceCode.Text = "Automatic ID";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(6, 172);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(93, 20);
            this.lblCreatedBy.TabIndex = 1;
            this.lblCreatedBy.Text = "Created By";
            // 
            // lblDateCreated
            // 
            this.lblDateCreated.AutoSize = true;
            this.lblDateCreated.Location = new System.Drawing.Point(6, 98);
            this.lblDateCreated.Name = "lblDateCreated";
            this.lblDateCreated.Size = new System.Drawing.Size(109, 20);
            this.lblDateCreated.TabIndex = 1;
            this.lblDateCreated.Text = "Date Created";
            // 
            // lblInvoiceCode
            // 
            this.lblInvoiceCode.AutoSize = true;
            this.lblInvoiceCode.Location = new System.Drawing.Point(6, 36);
            this.lblInvoiceCode.Name = "lblInvoiceCode";
            this.lblInvoiceCode.Size = new System.Drawing.Size(105, 20);
            this.lblInvoiceCode.TabIndex = 0;
            this.lblInvoiceCode.Text = "Invoice Code";
            // 
            // gbTotal
            // 
            this.gbTotal.Controls.Add(this.txtTotalAmount);
            this.gbTotal.Controls.Add(this.lblTotalAmount);
            this.gbTotal.Location = new System.Drawing.Point(12, 505);
            this.gbTotal.Name = "gbTotal";
            this.gbTotal.Size = new System.Drawing.Size(279, 118);
            this.gbTotal.TabIndex = 3;
            this.gbTotal.TabStop = false;
            this.gbTotal.Text = "Total";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(11, 59);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(262, 27);
            this.txtTotalAmount.TabIndex = 3;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(6, 36);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(108, 20);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "Total Amount";
            // 
            // btnSaveInvoice
            // 
            this.btnSaveInvoice.Location = new System.Drawing.Point(639, 485);
            this.btnSaveInvoice.Name = "btnSaveInvoice";
            this.btnSaveInvoice.Size = new System.Drawing.Size(136, 38);
            this.btnSaveInvoice.TabIndex = 4;
            this.btnSaveInvoice.Text = "Save Invoice";
            this.btnSaveInvoice.UseVisualStyleBackColor = true;
            this.btnSaveInvoice.Click += new System.EventHandler(this.btnSaveInvoice_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(639, 553);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 38);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(639, 417);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(136, 38);
            this.btnAddProduct.TabIndex = 5;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // ProductID
            // 
            this.ProductID.DataPropertyName = "ProductCode";
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.MinimumWidth = 6;
            this.ProductID.Name = "ProductID";
            this.ProductID.Visible = false;
            this.ProductID.Width = 125;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.MinimumWidth = 6;
            this.ProductName.Name = "ProductName";
            this.ProductName.Width = 250;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "InventoryQuantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 250;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "SellingPrice";
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.Width = 240;
            // 
            // ToatlAmount
            // 
            this.ToatlAmount.DataPropertyName = "TotalAmount";
            this.ToatlAmount.HeaderText = "Total Amount";
            this.ToatlAmount.MinimumWidth = 6;
            this.ToatlAmount.Name = "ToatlAmount";
            this.ToatlAmount.Width = 243;
            // 
            // dtmInvoice
            // 
            this.dtmInvoice.Location = new System.Drawing.Point(10, 127);
            this.dtmInvoice.Name = "dtmInvoice";
            this.dtmInvoice.Size = new System.Drawing.Size(263, 27);
            this.dtmInvoice.TabIndex = 3;
            // 
            // frmBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 680);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbCustomer);
            this.Controls.Add(this.btnSaveInvoice);
            this.Controls.Add(this.gbTotal);
            this.Controls.Add(this.gbInvoiceDetails);
            this.Controls.Add(this.dgvBill);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmBill";
            this.Text = "Bill";
            this.Load += new System.EventHandler(this.Bill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBill)).EndInit();
            this.gbCustomer.ResumeLayout(false);
            this.gbCustomer.PerformLayout();
            this.gbInvoiceDetails.ResumeLayout(false);
            this.gbInvoiceDetails.PerformLayout();
            this.gbTotal.ResumeLayout(false);
            this.gbTotal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBill;
        private System.Windows.Forms.GroupBox gbCustomer;
        private System.Windows.Forms.GroupBox gbInvoiceDetails;
        private System.Windows.Forms.GroupBox gbTotal;
        private System.Windows.Forms.Button btnSaveInvoice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.ComboBox cboSelectCustomer;
        private System.Windows.Forms.Label lblSelectCustomer;
        private System.Windows.Forms.Label lblInvoiceCode;
        private System.Windows.Forms.TextBox txtCreatedBy;
        private System.Windows.Forms.TextBox txtInvoiceCode;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblDateCreated;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToatlAmount;
        private System.Windows.Forms.DateTimePicker dtmInvoice;
    }
}