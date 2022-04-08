using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {

        private DriverRepository driverRepository;
        private CarRepository carRepository;
        private RaceRepository raceRepository;

        public ChampionshipController()
        {
            this.driverRepository = new DriverRepository();
            this.carRepository = new CarRepository();
            this.raceRepository = new RaceRepository();
        }

        //    Gives the Car with given name to the Driver with given name(if exists).
        //    If the Driver does not exist in the DriverRepository, throw InvalidOperationException with message 
        //    •	"Driver {name} could not be found."
        //    If the Car does not exist in the CarRepository, throw InvalidOperationException with message 
        //    •	"Car {name} could not be found."
        //    If everything is successful you should add the Car to the Driver and return the following message:
        //    •	"Driver {driver name} received car {car name}."

        public string AddCarToDriver(string driverName, string carModel)
        {
            ICar car = carRepository.GetByName(carModel);
            IDriver driver = driverRepository.GetByName(driverName);

            if (driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }
            else if (car == null)
            {
                throw new InvalidOperationException($"Car {carModel} could not be found.");
            }

            driver.AddCar(car);

            return $"Driver {driver.Name} received car {car.Model}.";
        }

        //   Adds a Driver to the Race.
        //   If the Race does not exist in the RaceRepository, throw an InvalidOperationException with message:
        //   •	"Race {name} could not be found."
        //   If the Driver does not exist in the DriverRepository, throw an InvalidOperationException with message:
        //   •	"Driver {name} could not be found."
        //   If everything is successful, you should add the Driver to the Race and return the following message:
        //   •	"Driver {driver name} added in {race name} race."


        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace race = this.raceRepository.GetByName(raceName);
            IDriver driver = this.driverRepository.GetByName(driverName);

            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            else if (driver == null)
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }

            race.AddDriver(driver);

            return $"Driver {driver.Name} added in {race.Name} race.";
        }

        //   Create a Car with the provided model and horsepower and add it to the repository.There are two types of Car: "Muscle" and "Sports".
        //   If the Car already exists in the appropriate repository throw an ArgumentException with following message:
        //   "Car {model} is already created."
        //   If the Car is successfully created, the method should return the following message:
        //   "{"MuscleCar"/ "SportsCar"} {model} is created."


        public string CreateCar(string type, string model, int horsePower)
        {
            ICar car = null;

            if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            else if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }

            ICar result = carRepository.GetByName(model);

            if (result != null)
            {
                throw new ArgumentException($"Car {model} is already created.");
            }

            this.carRepository.Add(car);
            return $"{car.GetType().Name} {model} is created.";
        }

        //   Creates a Driver with the given name and adds it to the appropriate repository.
        //   The method should return the following message:
        //   "Driver {name} is created."
        //   If a driver with the given name already exists in the driver repository, throw an ArgumentException with message 
        //   "Driver {name} is already created."

        public string CreateDriver(string driverName)
        {
            IDriver driver = new Driver(driverName);
            IDriver result = driverRepository.GetByName(driverName);

            if (result != null)
            {
                throw new ArgumentException($"Driver {driverName} is already created.");
            }

            driverRepository.Add(driver);
            return $"Driver {driverName} is created.";
        }

        //   Creates a Race with the given name and laps and adds it to the RaceRepository.
        //   If the Race with the given name already exists, throw an InvalidOperationException with message:
        //   •	"Race {name} is already create."
        //   If everything is successful you should return the following message:
        //   •	"Race {name} is created."


        public string CreateRace(string name, int laps)
        {
            IRace race = this.raceRepository.GetByName(name);

            if (race != null)
            {
                throw new InvalidOperationException($"Race {name} is already create.");
            }

            race = new Race(name, laps);

            this.raceRepository.Add(race);

            return $"Race {name} is created.";
        }

        //   If the Race does not exist in RaceRepository, throw an InvalidOperationException with message:
        //   •	"Race {name} could not be found."
        //   If the participants in the race are less than 3, throw an InvalidOperationException with message:
        //   •	"Race {race name} cannot start with less than 3 participants."
        //   If everything is successful, you should return the following message:
        //   •	"Driver {first driver name} wins {race name} race."
        //   "Driver {second driver name} is second in {race name} race."
        //   "Driver {third driver name} is third in {race name} race."


        public string StartRace(string raceName)
        {
            IRace race = this.raceRepository.GetByName(raceName);

            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }
            else if (race.Drivers.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than 3 participants.");
            }

            List<IDriver> result = race.Drivers.OrderByDescending(x => x.Car.CalculateRacePoints(race.Laps)).Take(3).ToList();

            this.raceRepository.Remove(race);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Driver {result[0].Name} wins {raceName} race.");
            sb.AppendLine($"Driver {result[1].Name} is second in {raceName} race.");
            sb.AppendLine($"Driver {result[2].Name} is third in {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
