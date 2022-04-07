using Bakery.Models.BakedFoods.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.BakedFoods
{
    public abstract class BakedFood : IBakedFood
    {
        private string name;
        private int portion;
        private decimal price;

        //                  string name, int portion, decimal price
        protected BakedFood(string name, int portion, decimal price)
        {
            //if (string.IsNullOrWhiteSpace(name))
            //{
            //    throw new ArgumentException(ExceptionMessages.InvalidName);
            //}

            //this.name = name;

            //if (portion <= 0)
            //{
            //    throw new ArgumentException(ExceptionMessages.InvalidPortion);
            //}

            //this.portion = portion;

            //if (price <= 0)
            //{
            //    throw new ArgumentException(ExceptionMessages.InvalidPrice);
            //}

            //this.price = price;

            Name = name;
            Portion = portion;
            Price = price;
        }

        // •	Name – string (If the name is null or whitespace throw an ArgumentException with message "Name cannot be null or white space!")

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

        //Returns a string with information about each food. The returned string must be in the following format:
        //"{currentBakedFoodName}: {currentPortion}g - {currentPrice - formatted to the second digit}"

        public override string ToString()
        {
            return $"{this.Name}: {this.Portion}g - {this.Price:f2}";
        }
    }
}
