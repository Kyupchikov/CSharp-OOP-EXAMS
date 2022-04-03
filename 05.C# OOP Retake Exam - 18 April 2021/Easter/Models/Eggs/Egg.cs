using Easter.Models.Eggs.Contracts;
using Easter.Utilities.Messages;
using System;


namespace Easter.Models.Eggs
{
    public class Egg : IEgg
    {
        private string name;
        private int energyRequired;

        public Egg(string name, int energyRequired)
        {
            Name = name;
            EnergyRequired = energyRequired;
        }

        //        o The name of a Egg
        //o If the name is null or whitespace, throw an ArgumentException with message: 
        //"Egg name cannot be null or empty."

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEggName);
                }

                this.name = value;
            }
        }

        //        o The energy an egg requires in order to be colored
        //o If the energyRequired is below 0, set it to 0

        public int EnergyRequired
        {
            get => this.energyRequired;
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.energyRequired = value;
            }
        }

        //        The GetColored() method decreases the required energy of the egg by 10 units.
        //•	An egg's required energy should not drop below 0.

        public void GetColored()
        {
            this.energyRequired -= 10;
        }

        // The IsDone() method returns true if the energyRequired is equal to 0.

        public bool IsDone()
        {
            return this.energyRequired == 0;
        }
    }
}
