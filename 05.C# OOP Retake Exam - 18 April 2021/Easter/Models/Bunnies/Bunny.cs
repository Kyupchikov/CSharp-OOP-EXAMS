using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;


namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        internal readonly List<IDye> dyes;

        protected Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            this.dyes = new List<IDye>();
        }

        //        o If the name is null or whitespace, throw an ArgumentException with message: 
        //"Bunny name cannot be null or empty."

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }

                this.name = value;
            }
        }

        //        o The energy of a bunny
        //o If a Bunny’s energy drops below 0, set it to 0

        public int Energy
        {
            get => this.energy;
            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.energy = value;
            }
        }

        //        •	Dyes - ICollection<IDye>
        //o   A collection of a bunny's dyes

        public ICollection<IDye> Dyes
            => this.dyes;

        // This method adds the given Dye to the Bunny's collection of Dyes. 

        public void AddDye(IDye dye)
        {
            this.dyes.Add(dye);
        }

        //        abstract void Work()
        //The Work() method decreases the bunny's energy by 10. 
        //•	If a Bunny’s energy drops below 0, set it to 0.

        public abstract void Work();

    }
}
