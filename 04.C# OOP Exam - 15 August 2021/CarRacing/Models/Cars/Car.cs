using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public abstract class Car : ICar
    {
        private string make;
        private string model;
        private string vin;
        protected int horsePower;
        private double fuelAvailable;
        private double fuelConsumptionPerRace;

        protected Car(string make, string model, string VIN, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            this.VIN = VIN;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;
        }


        /*•	Make – string
o	If the make is null or whitespace, throw an ArgumentException with message: "Car make cannot be null or empty."
•	Model – string  
o	If the model is null or whitespace, throw an ArgumentException with message: "Car model cannot be null or empty."
•	VIN – string
o	If the VIN is not exactly 17 characters long, throw an ArgumentException with message: "Car VIN must be exactly 17 characters long."
o	All VINs will be unique
        */

        public string Make
        {
            get => this.make;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car make cannot be null or empty.");
                }

                make = value;
            }
        }

        public string Model
        {
            get => this.model;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Car model cannot be null or empty.");
                }

                this.model = value;
            }
        }

        public string VIN
        {
            get => this.vin;

            private set
            {
                if (value.Length != 17)
                {
                    throw new ArgumentException("Car VIN must be exactly 17 characters long.");
                }

                this.vin = value;
            }
        }

        /* •	HorsePower – int
o	If the horse power is below 0, throw an ArgumentException with message: "Horse power cannot be below 0."
•	FuelAvailable – double
o	If the fuel available drops below 0, just set it to 0
•	FuelConsumtpionPerRace – double
o	If the fuel consumption per race is below 0, throw an ArgumentException with message: "Fuel consumption cannot be below 0."
*/

        public int HorsePower
        {
            get => this.horsePower;

            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Horse power cannot be below 0.");
                }

                this.horsePower = value;
            }
        }

        public double FuelAvailable
        {
            get => this.fuelAvailable;

            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.fuelAvailable = value;
            }
        }

        public double FuelConsumptionPerRace
        {
            get => this.fuelConsumptionPerRace;

            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fuel consumption cannot be below 0.");
                }

                this.fuelConsumptionPerRace = value;
            }
        }

        public virtual void Drive()
        {
            this.FuelAvailable -= this.FuelConsumptionPerRace;
        }
    }
}
