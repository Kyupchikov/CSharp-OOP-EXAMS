using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        // •	FoodOrders – collection of foods accessible only by the base class
        private readonly List<IBakedFood> foodOrders;
        // •	DrinkOrders – collection of drinks accessible only by the base class
        private readonly List<IDrink> drinkOrders;
        private int tableNumber;
        private int capacity;
        private int numberOfPeople;
        private decimal pricePerPerson;
        private bool isReserved;
    

        //           int tableNumber, int capacity, decimal pricePerPerson
        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
            this.foodOrders = new List<IBakedFood>();
            this.drinkOrders = new List<IDrink>();
        }

        // •	TableNumber – int the table number 
        public int TableNumber
        {
            get => this.tableNumber;
            private set
            {
                this.tableNumber = value;
            }
        }

        //•	Capacity – int the table capacity (capacity can’t be less than zero.
        //In these cases, throw an ArgumentException with message "Capacity has to be greater than 0")

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                this.capacity = value;
            }
        }

        // •	NumberOfPeople – int the count of people who want a table (number of people cannot be less or equal to 0.
        // In these cases, throw an ArgumentException with message "Cannot place zero or less people!")

        public int NumberOfPeople
        {
            get => this.numberOfPeople;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                this.numberOfPeople = value;
            }
        }

        // •	PricePerPerson – decimal the price per person for the table

        public decimal PricePerPerson
        {
            get => this.pricePerPerson;
            private set
            {
                this.pricePerPerson = value;
            }
        }

        // •	IsReserved – bool returns true if the table is reserved

        public bool IsReserved
        {
            get => this.isReserved;
            private set
            {
                this.isReserved = value;
            }
        }

        // •	Price – calculated property, which calculates the price for all people

        public decimal Price
            => this.PricePerPerson * this.NumberOfPeople;

        // Removes all of the ordered drinks and food and finally frees the table and sets the count of people to 0.

        public void Clear()
        {
            this.foodOrders.Clear();
            this.drinkOrders.Clear();
            this.numberOfPeople = 0;
            this.isReserved = false;
        }

        // Returns the bill for all of the ordered drinks and food.

        public decimal GetBill()
        {
            decimal priceForFood = this.foodOrders.Sum(x => x.Price);
            decimal priceForDrinks = this.drinkOrders.Sum(x => x.Price);
            decimal totalPricePerPerson = this.PricePerPerson * this.NumberOfPeople;
            decimal totalSum = priceForDrinks + priceForFood + totalPricePerPerson;

            return totalSum;
        }

        //"Table: {table number}"
        //"Type: {table type}"
        //"Capacity: {table capacity}"
        //"Price per Person: {price per person for the current table}"

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();

            if (this.IsReserved == false)
            {
                sb.AppendLine($"Table: {this.tableNumber}");
                sb.AppendLine($"Type: {this.GetType().Name}");
                sb.AppendLine($"Capacity: {this.Capacity}");
                sb.AppendLine($"Price per Person: {this.PricePerPerson}");
            }

            return sb.ToString().TrimEnd();
        }

        // Orders the provided drink (think of a way to collect all the drinks which are ordered).

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrders.Add(drink);
        }

        // Orders the provided food (think of a way to collect all the food which is ordered).

        public void OrderFood(IBakedFood food)
        {
            this.foodOrders.Add(food);
        }

        // Reserves the table with the count of people given.

        public void Reserve(int numberOfPeople)
        {
            this.NumberOfPeople = numberOfPeople;
            this.IsReserved = true;
        }
    }
}
