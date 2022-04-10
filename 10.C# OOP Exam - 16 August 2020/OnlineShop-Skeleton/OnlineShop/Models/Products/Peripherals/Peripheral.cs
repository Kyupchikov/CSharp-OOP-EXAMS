using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models
{
    public abstract class Peripheral : Product,IPeripheral
    {
        private string connectionType;

        //                int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType
        public Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.ConnectionType = connectionType;
        }

        public string ConnectionType    
        {
            get => this.connectionType;
            private set
            {
                this.connectionType = value;
            }
        }

        
        // base.ToString "Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id})"
        // this.ToString "Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id}) Connection Type: {connection type}"
        public override string ToString()
        {
            return $"{base.ToString()} Connection Type: {this.connectionType}";
        }
    }
}
