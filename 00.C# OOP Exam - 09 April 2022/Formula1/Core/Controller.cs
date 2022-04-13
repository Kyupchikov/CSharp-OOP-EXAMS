using Formula1.Core.Contracts;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Formula1.Models;

namespace Formula1.Core
{
    public class Controller : IController
    {
        //    •	pilotRepository - PilotRepository 
        //    •	raceRepository - RaceRepository 
        //    •	carRepository - FormulaOneCarRepository

        private readonly PilotRepository pilotRepository;
        private readonly RaceRepository raceRepository;
        private readonly FormulaOneCarRepository carRepository;

        public Controller()
        {
            this.pilotRepository = new PilotRepository();
            this.raceRepository = new RaceRepository();
            this.carRepository = new FormulaOneCarRepository();
        }

        //        Adds a car with the given car model to a pilot with the given name.After successfully adding a car to a pilot,
        //        remove the car from the FormulaOneCarRepository:
        //•	If the pilot does not exist, or the pilot already has a car, throw a InvalidOperationException with the following message:
        //"Pilot { pilot name } does not exist or has a car."
        //•	If the car model does not exist, throw a NullReferenceException with the following message: "Car { model } does not exist."
        //•	If no errors are thrown, return a string with the following message: "Pilot { pilot name } will drive a {type of car} { model } car."

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = this.pilotRepository.FindByName(pilotName);
            IFormulaOneCar car = this.carRepository.FindByName(carModel);

            if (pilot == null)
            {
                throw new InvalidOperationException($"Pilot { pilotName } does not exist or has a car.");
            }

            if (pilot.Car != null)
            {
                throw new InvalidOperationException($"Pilot { pilotName } does not exist or has a car.");
            }

            if (car == null)
            {
                throw new NullReferenceException($"Car { carModel} does not exist.");
            }

            this.carRepository.Remove(car);

            pilot.AddCar(car);

            return $"Pilot { pilotName} will drive a {car.GetType().Name} { carModel} car.";
        }

        //        Adds a pilot with the given name, to the race with the given race name.
        //•	If the race does not exist, throw a NullReferenceException with the following message: "Race { race name } does not exist."
        //•	If the pilot does not exist, or the pilot can not race, or the pilot is already in the race, throw a InvalidOperationException with the following message:
        //"Can not add pilot { pilot full name } to the race."
        //•	If no errors are thrown, return a string with the following message: "Pilot { pilot full name } is added to the { race name } race."

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IPilot pilot = this.pilotRepository.FindByName(pilotFullName);
            IRace race = this.raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException($"Race { raceName } does not exist.");
            }

            if (pilot == null || pilot.CanRace == false || race.Pilots.Any(x => x.FullName == pilotFullName))
            {
                throw new InvalidOperationException($"Can not add pilot { pilotFullName } to the race.");
            }


            race.AddPilot(pilot);

            return $"Pilot { pilotFullName } is added to the { raceName } race.";
        }

        //        Creates a formula one car with the given parameters and adds it to the FormulaOneCarRepository.Valid types are: "Ferrari" and "Williams":
        //•	If a car with the given model exists, throw an InvalidOperationException with a message: "Formula one car { model } is already created."
        //•	If the car type is invalid, throw an InvalidOperationException with a message: "Formula one car type { type } is not valid."
        //•	If no errors are thrown, return a string with the following message: "Car { type }, model { model } is created."

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar car = this.carRepository.FindByName(model);

            if (car != null)
            {
                throw new InvalidOperationException($"Formula one car { model } is already created.");
            }

            if (type == "Ferrari")
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == "Williams")
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }
            else
            {
                throw new InvalidOperationException($"Formula one car type {type} is not valid.");
            }

            this.carRepository.Add(car);

            return $"Car { type }, model { model } is created.";
        }

        //        Adds a Pilot to the PilotRepository.
        //•	If a pilot with the given full name exists, throw a InvalidOperationException with the following message: "Pilot { full name } is already created."
        //•	If the Pilot is added successfully to the repository, return the following message: "Pilot { full name } is created."

        public string CreatePilot(string fullName)
        {

            IPilot pilot = this.pilotRepository.FindByName(fullName);

            if (pilot != null)
            {
                throw new InvalidOperationException($"Pilot {fullName} is already created.");
            }

            IPilot pilotToAdd = new Pilot(fullName);

            this.pilotRepository.Add(pilotToAdd);

            return $"Pilot {fullName} is created.";
        }

        //        Creates a race with the given name, number of laps and adds it to the RaceRepository:
        //•	If a race with the given race name exists, throw a InvalidOperationException with the following message: "Race { race name } is already created."
        //•	If no errors are thrown, return a string with the following message: "Race { race name } is created."

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace race = this.raceRepository.FindByName(raceName);

            if (race != null)
            {
                throw new InvalidOperationException($"Race { raceName } is already created.");
            }

            IRace raceToAdd = new Race(raceName, numberOfLaps);

            this.raceRepository.Add(raceToAdd);

            return $"Race { raceName } is created.";
        }

        //        Returns information about each pilot, ordered by the number of wins descending.You can use the override ToString method in the Pilot class.
        //"Pilot {FullName} has {NumberOfWins} wins.
        //Pilot {FullName
        //    }
        //    has {NumberOfWins
        //}
        //wins.
        //(…)"

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.pilotRepository.Models.OrderByDescending(x => x.NumberOfWins))
            {
                sb.AppendLine($"{item}");
            }

            return sb.ToString().TrimEnd();
        }

        //        Returns information about each race that has been executed.You can use the RaceInfo method in the Race class.
        //"The { race name } race has:
        //Participants: { number of participants
        //    }
        //    Number of laps: { number of laps
        //}
        //Took place: Yes
        //The
        //{ race name }
        //race has:
        //Participants: { number of participants }
        //Number of laps: { number of laps }
        //Took place: Yes
        //(…)"

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.raceRepository.Models.Where(x => x.TookPlace == true))
            {
                sb.AppendLine($"The { item.RaceName} race has:");
                sb.AppendLine($"Participants: {item.Pilots.Count}");
                sb.AppendLine($"Number of laps: { item.NumberOfLaps}");
                sb.AppendLine($"Took place: Yes");
            }

            return sb.ToString().TrimEnd();
        }

        //If everything is valid, you should arrange for all pilots in the given race to start racing.As a result, this method returns the three fastest pilots.
        //To execute the race you should sort all riders in descending order by the result of the RaceScoreCalculator method in FormulaOneCar object.
        //In the end, if everything is valid set the race's TookPlace property to true, increase the winner's score, and return the corresponding message.
        //•	If the race does not exist, throw a NullReferenceException with the following message: "Race { race name } does not exist."
        //•	If the race has less than 3 pilots, throw an InvalidOperationException with the following message:
        //"Race { race name } cannot start with less than three participants."
        //•	If the race has been already executed, throw an InvalidOperationException with the following message: "Can not execute race { race name }."
        //•	If no errors are thrown, return a string with the following message: 
        //"Pilot { pilot full name } wins the { race name } race.
        //Pilot { pilot full name } is second in the { race name }
        //    race.
        //Pilot { pilot full name
        //} is third in the
        //{ race name }
        //race."

        public string StartRace(string raceName)
        {
            IRace race = this.raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException($"Race { raceName } does not exist.");
            }

            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException($"Race { raceName } cannot start with less than three participants.");
            }

            if (race.TookPlace == true)
            {
                throw new InvalidOperationException($"Can not execute race { raceName }.");
            }

            race.TookPlace = true;

            foreach (var item in race.Pilots)
            {
                Pilot pilot = item as Pilot;

                pilot.CanRace = true;
            }

            var firstThreeRacers = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).ToList();
            Pilot winner = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).First() as Pilot;
            winner.NumberOfWins += 1;

            //"Pilot { pilot full name } wins the { race name } race.
            //Pilot { pilot full name } is second in the { race name } race.
            //Pilot { pilot full name} is third in the { race name } race."

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Pilot {firstThreeRacers[0].FullName} wins the { race.RaceName } race.");
            sb.AppendLine($"Pilot {firstThreeRacers[1].FullName} is second in the {race.RaceName} race.");
            sb.AppendLine($"Pilot {firstThreeRacers[2].FullName} is third in the { race.RaceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
