﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AddProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }

        public AddProduct() { }

        public AddProduct(int productID, string productName, int quantity, decimal price, decimal totalAmount)
        {
            ProductID = productID;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            TotalAmount = totalAmount;
        }
    }
}