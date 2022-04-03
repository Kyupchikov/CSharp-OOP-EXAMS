using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Repositories;
using System;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Maps;
using System.Text;
using System.Linq;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository cars = new CarRepository();
        private RacerRepository racers = new RacerRepository();
        private IMap map = new Map();

        public RacerRepository Racers => racers;

        //        // Adds a Car and adds it to the CarRepository. Valid types are: "SuperCar" and "TunedCar".
        //        If the Car type is invalid, you have to throw an ArgumentException with the following message:
        //•	"Invalid car type!"
        //If the Car is added successfully, the method should return the following string:
        //•	"Successfully added car {carMake} {carModel} ({VIN})."

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car;

            if (type == "SuperCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == "TunedCar")
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                return $"Invalid car type!";
            }

            cars.Add(car);
            return $"Successfully added car {make} {model} ({VIN}).";
        }

        // Creates a Racer of the given type and adds it to the RacerRepository. Valid types are: "ProfessionalRacer" and "StreetRacer". 
        //If the car is not found throw ArgumentException with message:
        //•	"Car cannot be found!"
        //If the racer type is invalid, throw an ArgumentException with message:
        //•	"Invalid racer type!"
        //The method should return the following string if the operation is successful:
        //•	"Successfully added racer {playerUsername}."


        public string AddRacer(string type, string username, string carVIN)
        {
            var result = cars.FindBy(carVIN);
            IRacer racer;

            if (result == null)
            {
                throw new ArgumentException("Car cannot be found!");
            }
            else if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, result);
            }
            else if (type == "StreetRacer")
            {
                racer = new StreetRacer(username, result);
            }
            else
            {
                throw new ArgumentException("Invalid racer type!");
            }

            racers.Add(racer);
            return $"Successfully added racer {racer.Username}.";
        }

        //Finds both Racers and they start racing.  It returns the result from StartRace() method.
        //        If one of the racers cannot be found throw ArgumentException with message:
        //•	"Racer {racerUsername} cannot be found!"


        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            //var result1 = racers.FindBy(racerOneUsername);
            //var result2 = racers.FindBy(racerTwoUsername);
            
            if (racers.FindBy(racerOneUsername) == null)
            {
                throw new ArgumentException($"Racer {racerOneUsername} cannot be found!");
            }
            if (racers.FindBy(racerTwoUsername) == null)
            {
                throw new ArgumentException($"Racer {racerTwoUsername} cannot be found!");
            }

            IMap map = new Map();
            
            return map.StartRace(racers.FindBy(racerOneUsername), racers.FindBy(racerTwoUsername));
        }

        /*Returns information about each racer separated with a new line. Order them by driving experience descending, then by username alphabetically.
         * You can use the overridden ToString() Racer method.
"{racer type}: {racer username}"
"--Driving behavior: {racingBehavior}"
"--Driving experience: {drivingExperience}"
"--Car: {carMake} {carModel} ({carVIN})"
*/

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            var result = racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(x => x.Username);

            foreach (var racer in result)
            {
                sb.AppendLine($"{racer}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
