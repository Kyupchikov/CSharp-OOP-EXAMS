using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxyden;
        private IBag bag;

        protected Astronaut(string name, double oxygen)
        {
            Name = name;
            Oxygen = oxygen;

            this.bag = new Backpack();
        }

        //  o If the name is null or whitespace, throw an ArgumentNullException with message: "Astronaut name cannot be null or empty."

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value),"Astronaut name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        // o	If the oxygen is below 0, throw an ArgumentException with message: "Cannot create Astronaut with negative oxygen!"

        public double Oxygen
        {
            get => this.oxyden;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Cannot create Astronaut with negative oxygen!");
                }

                this.oxyden = value;
            }
        }

        public bool CanBreath
                => this.oxyden > 0;

        public IBag Bag
                => this.bag;

        //        The Breath() method decreases astronauts' oxygen. Keep in mind that some types of Astronaut can implement the method in a different way. 
        //•	The method decreases the astronauts' oxygen by 10 units.
        //•	Astronaut's oxygen should not drop below zero

        public virtual void Breath()
        {
            Oxygen -= 10;

            if (Oxygen < 0)
            {
                Oxygen = 0;
            }
        }
    }
}
