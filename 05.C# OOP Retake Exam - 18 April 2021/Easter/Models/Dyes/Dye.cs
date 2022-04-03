using Easter.Models.Dyes.Contracts;


namespace Easter.Models.Dyes
{
    public class Dye : IDye
    {
        private int power;

        public Dye(int power)
        {
            Power = power;
        }

        //        o The power of an Dye
        //o If the power is below 0, set it to 0.

        public int Power
        {
            get => this.power;
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.power = value;
            }
        }

        // •	This method returns true if the power is equal to 0

        public bool IsFinished()
        {
            return this.power == 0;
        }

        //        The Use() method decreases the Dye's power by 10. 
        //•	An Dye's power should not drop below 0, if the power becomes less than 0, set it to 0

        public void Use()
        {
            this.Power -= 10;
        }
    }
}
