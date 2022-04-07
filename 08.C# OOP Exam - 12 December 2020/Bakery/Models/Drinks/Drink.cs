using Bakery.Models.Drinks.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Drinks
{
    public abstract class Drink : IDrink
    {
        private string name;
        private int portion;
        private decimal price;
        private string brand;

        //              string name, int portion, decimal price, string brand
        protected Drink(string name, int portion, decimal price, string brand)
        {
            Name = name;
            Portion = portion;
            Price = price;
            Brand = brand;
        }

        // •	Name – string (If the name is null or whitespace throw an ArgumenException with message "Name cannot be null or white space!")

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidName);
                }

                this.name = value;
            }
        }

        // •	Portion – int (can’t be less or equal to 0. In these cases, throw an ArgumentException with message "Portion cannot be less or equal to zero")

        public int Portion
        {
            get => this.portion;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPortion);
                }

                this.portion = value;
            }
        }

        // •	Price – decimal (can’t be less or equal to 0. In these cases, throw an ArgumentException with message "Price cannot be less or equal to zero!")

        public decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPrice);
                }

                this.price = value;
            }
        }

        // •	Brand -  string (If the brand is null or whitespace thrown ArgumentException with message "Brand cannot be null or white space!")

        public string Brand
        {
            get => this.brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBrand);
                }

                this.brand = value;
            }
        }

        //Returns a string with information about each drink.The returned string must be in the following format:
        //"{current drink name} {current brand name} - {current portion}ml - {current price - formatted to the second digit}lv"

        public override string ToString()
        {
            return $"{this.Name} {this.Brand} - {this.Portion}ml - {this.Price:f2}lv";
        }
    }
}
