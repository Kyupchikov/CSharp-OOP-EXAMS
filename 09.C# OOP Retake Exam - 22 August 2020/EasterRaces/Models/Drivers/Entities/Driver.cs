using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        private ICar car;
        private int numberOfWins;
        private bool canParticipate;

        public Driver(string name)
        {
            Name = name;
        }

        // o	If the name is null, empty or less than 5 symbols throw an ArgumentException with message "Name {name} cannot be less than 5 symbols."

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Name {value} cannot be less than 5 symbols.");
                }

                name = value;
            }
        }

        public ICar Car
        {
            get => this.car;
            private set
            {
                this.car = value;
            }
        }

        public int NumberOfWins
        {
            get => this.numberOfWins;
            private set
            {
                numberOfWins = value;
            }
        }

        // o Default behaviour is false
        // o Driver can participate in race, ONLY if he has a Car(Car is not null)

        public bool CanParticipate
        {
            get => this.canParticipate;
            private set
            {
                if (this.car != null)
                {
                    canParticipate = true;
                }
                else
                {
                    canParticipate = false;
                }
            }
        }

        // This method adds a Car to the Driver. If the car is null, throw ArgumentNullException with message "Car cannot be null.".
        // If the given Car is not null, set the current Car as the given one and after that Driver can participate to race.


        public void AddCar(ICar car)
        {
            if (car == null )
            {
                throw new ArgumentNullException(nameof(car), "Car cannot be null.");
            }

            this.Car = car;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
    }
}
