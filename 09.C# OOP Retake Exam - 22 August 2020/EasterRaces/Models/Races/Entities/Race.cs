using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private readonly List<IDriver> drivers;

        public Race(string name, int laps)
        {
            Name = name;
            Laps = laps;
            this.drivers = new List<IDriver>();
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

        // o	Throws ArgumentException with message "Laps cannot be less than 1.", if the laps are less than 1

        public int Laps
        {
            get => this.laps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Laps cannot be less than 1.");
                }

                laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers
            => this.drivers.ToArray();

        //        •	If a Driver is null throw an ArgumentNullException with message "Driver cannot be null."
        //        •	If a Driver cannot participate in the Race(the Driver doesn't own a Car) throw an ArgumentException
        //          with message "Driver {driver name} could not participate in race."
        //        •	If the Driver already exists in the Race throw an ArgumentNullException with message:
        //          "Driver {driver name} is already added in {race name} race."

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "Driver cannot be null.");
            }
            else if (driver.Car == null)
            {
                throw new ArgumentException($"Driver {driver.Name} could not participate in race.");
            }
            else if (this.drivers.Contains(driver))
            {
                throw new ArgumentNullException(nameof(driver), $"Driver {driver.Name} is already added in {this.Name} race.");
            }

            this.drivers.Add(driver);
        }
    }
}
