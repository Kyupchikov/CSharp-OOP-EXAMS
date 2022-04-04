using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        protected int drivingExperience;
        private ICar car;

        protected Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
           this.Username = username;
           this.RacingBehavior = racingBehavior;
           this.DrivingExperience = drivingExperience;
           this.Car = car;
        }

        /* •	Username - string 
o	If the username is null or whitespace, throw an ArgumentException with message: "Username cannot be null or empty."
o	All usernames are unique
•	RacingBehavior -  string
o	If the racing behavior is null or whitespace, throw an ArgumentException with message: "Racing behavior cannot be null or empty."
•	DrivingExperience -  int
o	If the driving experience is below 0 or over 100, throw an ArgumentException with message: "Racer driving experience must be between 0 and 100."
 •	Car -  Car
o	If the car is null, throw an ArgumentException with message:
 "Car cannot be null or empty."

*/
        public string Username
        {
            get => this.username;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be null or empty.");
                }

                this.username = value;
            }
        }

        public string RacingBehavior
        {
            get => this.racingBehavior;

            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Racing behavior cannot be null or empty.");
                }

                this.racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => this.drivingExperience;

            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Racer driving experience must be between 0 and 100.");
                }

                this.drivingExperience = value;
            }
        }

        public ICar Car
        {
            get => this.car;

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Car cannot be null or empty.");
                }

                this.car = value;
            }
        }

        public bool IsAvailable()
        {
            return this.Car.FuelAvailable >= this.Car.FuelConsumptionPerRace;
        }

        public virtual void Race()
        {
            this.car.Drive();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.username}");
            sb.AppendLine($"--Driving behavior: {this.racingBehavior}");
            sb.AppendLine($"--Driving experience: {this.drivingExperience}");
            sb.AppendLine($"--Car: {this.car.Make} {this.car.Model} ({this.car.VIN})");

            return sb.ToString().TrimEnd();
        }
    }
}
