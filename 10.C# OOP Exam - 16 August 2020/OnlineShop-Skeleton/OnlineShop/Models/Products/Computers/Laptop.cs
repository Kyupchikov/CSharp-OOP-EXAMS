using OnlineShop.Models.Products.Computers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products
{
    public class Laptop : Computer
    {
        public Laptop(int id, string manufacturer, string model, decimal price)
            : base(id, manufacturer, model, price, 10)
        {
        }
    }
}
