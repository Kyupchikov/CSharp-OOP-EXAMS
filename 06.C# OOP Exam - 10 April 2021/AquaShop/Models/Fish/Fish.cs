using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public abstract class Fish : IFish
    {
        private string name;
        private string species;
        private int size;
        private decimal price;

        //             string name, string species, decimal price
        protected Fish(string name, string species, decimal price)
        {
            Name = name;
            Species = species;
            Price = price;
        }

        //        •	Name - string
        //o   If the name is null or whitespace, throw an ArgumentException with message: "Fish name cannot be null or empty."
        //o All names are unique

        public string Name
        {
            get => name; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Fish name cannot be null or empty.");
                }

                name = value;
            }
        }

        //        •	Species -  string
        //o   If the species is null or whitespace, throw an ArgumentException with message: "Fish species cannot be null or empty."

        public string Species
        {
            get => this.species;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Fish species cannot be null or empty.");
                }

                this.species = value;
            }
        }

        public int Size
        {
            get => this.size;
            protected set
            {
                this.size = value;
            }
        }

        //        o The price of the Fish
        //o If the price is below or equal 0, throw an ArgumentException with message:
        // "Fish price cannot be below or equal to 0."

        public decimal Price
        {
            get => this.price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Fish price cannot be below or equal to 0."); 
                }

                this.price = value;
            }
        }

        // The Eat() method increases the Fish’s size.

        public abstract void Eat();
        
    }
}
