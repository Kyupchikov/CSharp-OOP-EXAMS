using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        //        •	equipment - EquipmentRepository 
        //        •	gyms - a collection of IGym


        private readonly EquipmentRepository equipment;

        private readonly List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public IReadOnlyCollection<IGym> Gyms
                => gyms;

        public IReadOnlyCollection< EquipmentRepository> Equipment
                => (IReadOnlyCollection<EquipmentRepository>)equipment;

//        Creates and adds an Athlete to the Gym with the given name.Valid Athletes types are: "Boxer" (can exercise in a "BoxingGym")
//        , and "Weightlifter" (can exercise in a "WeightliftingGym").
//Return one of the following messages:
//•	If the Athlete type is invalid, throw an InvalidOperationException with the following message: "Invalid athlete type."
//•	If the Athlete cannot exercise in the given Gym, return a string with the following message: "The gym is not appropriate."
//•	If no errors are thrown, return a string with the following message: "Successfully added {athleteType} to {gymName}."


        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete;
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException("Invalid athlete type.");
            }

            var gym = this.gyms.First(x => x.Name == gymName);

            if (gym.GetType().Name == "WeightliftingGym" && athleteType == "Weightlifter")
            {
                gym.AddAthlete(athlete);
            }
            else if (gym.GetType().Name == "BoxingGym" && athleteType == "Boxer")
            {
                gym.AddAthlete(athlete);
            }
            else
            {
                return "The gym is not appropriate.";
            }

            return $"Successfully added {athleteType} to {gymName}.";
        }

//        Creates equipment of the given type and adds it to the EquipmentRepository.Valid types are: "BoxingGloves" and "Kettlebell". 
//•	If the equipment type is invalid, throw an InvalidOperationException with a message: "Invalid equipment type."
//•	If no errors are thrown, return a string with the following message: "Successfully added {equipmentType}."


        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment;
            if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else
            {
                throw new InvalidOperationException("Invalid equipment type.");
            }

            this.equipment.Add(equipment);

            return $"Successfully added {equipmentType}.";
        }

        //          Adds a Gym to the gym's collection. Valid types of gyms are: " BoxingGym" and " WeightliftingGym".
        //      •	If the Gym type is invalid, throw an InvalidOperationException with the following message: "Invalid gym type."
        //      •	If the Gym is added successfully, return the following message: "Successfully added {gymType}."


        public string AddGym(string gymType, string gymName)
        {
            IGym gym ;

            if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException("Invalid gym type.");
            }

            this.gyms.Add(gym);

            return $"Successfully added {gymType}.";
        }

//        Calculates the weight of all available equipment of the Gym with the given name.It is calculated by the sum of all inserted equipment in the Gym.
//Return a string in the following format:
//•	"The total weight of the equipment in the gym {gymName} is {value} grams."
//o The value should be formatted to the 2nd decimal place!


        public string EquipmentWeight(string gymName)
        {
            var gym = this.gyms.FirstOrDefault(x => x.Name == gymName);
            double result = gym.EquipmentWeight;

            return $"The total weight of the equipment in the gym {gymName} is {result:f2} grams.";
        }

//        Adds the desired Equipment to the Gym with the given name.You have to remove the Equipment from the EquipmentRepository if the insert is successful.
//•	If there is no such equipment, throw an InvalidOperationException with the following message: "There isn’t equipment of type {equipmentType}."
//•	If no errors are thrown, return a string with the following message: "Successfully added {equipmentType} to {gymName}."


        public string InsertEquipment(string gymName, string equipmentType)
        {
            var equipmentResult = this.equipment.FindByType(equipmentType);

            if (equipmentResult == null)
            {
                throw new InvalidOperationException($"There isn’t equipment of type {equipmentType}.");
            }

            IGym gym = this.gyms.FirstOrDefault(x => x.Name == gymName);
             
                gym.AddEquipment(equipmentResult);
            equipment.Remove(equipmentResult);

            return $"Successfully added {equipmentType} to {gymName}.";
            
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in this.gyms)
            {
                sb.Append(gym.GymInfo().ToString());
            }
            return sb.ToString().TrimEnd();
        }

//        Exercise all athletes in the Gym with the given name.Returns a string with information about how many athletes did exercise, in the following format:
//•	"Exercise athletes: {athletesCount}."


        public string TrainAthletes(string gymName)
        {
            IGym gym = this.gyms.FirstOrDefault(x => x.Name == gymName);

            foreach (var athlete in gym.Athletes)
            {
                athlete.Exercise();
            }

            return $"Exercise athletes: {gym.Athletes.Count}.";
        }
    }
}
