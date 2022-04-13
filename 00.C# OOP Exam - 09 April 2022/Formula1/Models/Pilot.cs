using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private bool canRace;
        private IFormulaOneCar car;
        private int numberOfWins;

        //           string fullName
        public Pilot(string fullName)
        {
            FullName = fullName;
            this.CanRace = false;
        }

        // o	If the pilot's full name is null, white space or the length is less than 5 symbols,
        // throw an ArgumentException with a message: "Invalid pilot name: { fullName }."

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Invalid pilot name: {value}.");
                }

                this.fullName = value;
            }
        }

        // o	If the car is null throw a NullReferenceException with a message: "Pilot car can not be null."

        public IFormulaOneCar Car
        {
            get => car;
            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Pilot car can not be null.");
                }

                car = value;
            }
        }

        public int NumberOfWins
        {
            get => numberOfWins;
            internal set
            {
                numberOfWins = value;
            }
        }

        public bool CanRace
        {
            get => canRace;
            internal set
            {
                canRace = value;
            }
        }

        // Sets a car to the pilot, and set CanRace to true.

        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.CanRace = true;
        }

        // The WinRace method increases the NumberOfWins by one (1) every time a pilot wins a race.

        public void WinRace()
        {
            this.NumberOfWins += 1;
        }

        //Returns a string with information about the number of wins for the pilot.The returned string must be in the following format:
        //"Pilot { full name } has { number of wins } wins."

        public override string ToString()
        {
            return $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
        }
    }
}
