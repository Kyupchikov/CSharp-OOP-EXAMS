using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        internal readonly List<string> items;

        public Planet(string name)
        {
            Name = name;

            this.items = new List<string>();
        }

        public ICollection<string> Items
                => this.items;

        // o If the name is null or whitespace, throw an ArgumentNullException with message: "Invalid name!"

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(name), "Invalid name!");
                }
                this.name = value;
            }
        }
    }
}
