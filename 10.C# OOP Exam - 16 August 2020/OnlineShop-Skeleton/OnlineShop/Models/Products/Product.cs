using OnlineShop.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models
{
    public abstract class Product : IProduct
    {
        private int id;
        private string manufacturer;
        private string model;
        private decimal price;
        private double overallPerformance;

        //                int id, string manufacturer, string model, decimal price, double overallPerformance
        protected Product(int id, string manufacturer, string model, decimal price, double overallPerformance)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Price = price;
            OverallPerformance = overallPerformance;
        }

        //•	Id – int (cannot be less than or equal to 0. In that case, throw ArgumentException with message "Id can not be less or equal than 0.")

        public int Id
        {
            get => this.id;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Id can not be less or equal than 0.");
                }

                this.id = value;
            }
        }

        // •	Manufacturer – string (cannot be null or whitespace. In that case, throw ArgumentException with message "Manufacturer can not be empty.")

        public string Manufacturer
        {
            get => this.manufacturer;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Manufacturer can not be empty.");
                }

                this.manufacturer = value;
            }
        }

        // •	Model – string (cannot be null or whitespace. In that case, throw ArgumentException with message "Model can not be empty.")

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Model can not be empty.");
                }

                this.model = value;
            }
        }

        // •	Price – decimal (cannot be less than or equal to 0. In that case, throw ArgumentException with message "Price can not be less or equal than 0.")

        public virtual decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price can not be less or equal than 0.");
                }

                this.price = value;
            }
        }

        // •	OverallPerformance – double (cannot be less than or equal to 0. In that case,
        // throw ArgumentException with message "Overall Performance can not be less or equal than 0.")

        public virtual double OverallPerformance
        {
            get => this.overallPerformance;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Overall Performance can not be less or equal than 0.");
                }

                this.overallPerformance = value;
            }
        }

        // "Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id})"
        //TODO     product type

        public override string ToString()
        {
            return $"Overall Performance: {this.OverallPerformance:f2}. Price: {this.Price:f2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id})";
        }
    }
}
