using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Models.Fish;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorationRepository;
        private readonly List<IAquarium> aquariums;

        public Controller()
        {
            this.decorationRepository = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
        }

        //        Functionality
        //Adds an Aquarium.Valid types are: "FreshwaterAquarium" and "SaltwaterAquarium".
        //If the Aquarium type is invalid, you have to throw an InvalidOperationException with the following message:
        //•	"Invalid aquarium type."
        //If the Aquarium is added successfully, the method should return the following string:
        //•	"Successfully added {aquariumType}."

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;

            if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException("Invalid aquarium type.");
            }

            this.aquariums.Add(aquarium);

            return $"Successfully added {aquariumType}.";
        }

        //        Creates a decoration of the given type and adds it to the DecorationRepository.Valid types are: "Ornament" and "Plant".
        //        If the decoration type is invalid, throw an InvalidOperationException with message:
        //•	"Invalid decoration type."
        //The method should return the following string if the operation is successful:
        //•	"Successfully added {decorationType}."

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;

            if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else
            {
                throw new InvalidOperationException("Invalid decoration type.");
            }

            this.decorationRepository.Add(decoration);

            return $"Successfully added {decorationType}.";
        }

        //        Adds the desired Fish to the Aquarium with the given name.Valid Fish types are: "FreshwaterFish", "SaltwaterFish".
        //If the Fish type is invalid, you have to throw an InvalidOperationException with the following message "Invalid fish type.".
        //If no errors are thrown, return one of the following messages:
        //•	"Water not suitable." - if the Fish cannot live in the Aquarium
        //•	"Successfully added {fishType} to {aquariumName}." - if the Fish is added successfully in the Aquarium

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IFish fishWanted;
            IAquarium aquariumWanted = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

            if (fishType == "SaltwaterFish")
            {
                fishWanted = new SaltwaterFish(fishName,fishSpecies,price);
            }
            else if (fishType == "FreshwaterFish")
            {
                fishWanted = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException("Invalid fish type.");
            }

            string aquariumType = aquariumWanted.GetType().Name;

            if (aquariumType == "FreshwaterAquarium" && fishType == "FreshwaterFish")
            {

            }
            else if (aquariumType == "SaltwaterAquarium" && fishType == "SaltwaterFish")
            {

            }
            else
            {
                return "Water not suitable.";
            }

            aquariumWanted.AddFish(fishWanted);

            return $"Successfully added {fishType} to {aquariumName}.";
        }

        //        Calculates the value of the Aquarium with the given name.It is calculated by the sum of all Fish’s and Decorations’ prices in the Aquarium.
        //Return a string in the following format:
        //•	"The value of Aquarium {aquariumName} is {value}."
        //o The value should be formatted to the 2nd decimal place!

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquariumWanted = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

            decimal sumByFish = aquariumWanted.Fish.Sum(x => x.Price);
            decimal sumByDecoration = aquariumWanted.Decorations.Sum(x => x.Price);

            return $"The value of Aquarium {aquariumName} is {(sumByFish + sumByDecoration):f2}.";
        }

        //        Feeds all Fish in the Aquarium with the given name.
        //Returns a string with information about how many fish were fed, in the following format:
        //•	"Fish fed: {fedCount}"

        public string FeedFish(string aquariumName)
        {
            IAquarium aquariumWanted = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);

            aquariumWanted.Feed();

            return $"Fish fed: {aquariumWanted.Fish.Count}";
        }

        //Adds the desired Decoration to the Aquarium with the given name.
        //You have to remove the Decoration from the DecorationRepository if the insert is successful.
        //If there is no such decoration, you have to throw an InvalidOperationException with the following message:
        //•	"There isn't a decoration of type {decorationType}."
        //If no errors are thrown, return a string with the following message "Successfully added {decorationType} to {aquariumName}.".

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decorationWanted = decorationRepository.FindByType(decorationType);

            if (decorationWanted == null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }

            this.decorationRepository.Remove(decorationWanted);
            IAquarium aquariumWanted = this.aquariums.FirstOrDefault(x => x.Name == aquariumName);
            aquariumWanted.AddDecoration(decorationWanted);

            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aqua in this.aquariums)
            {
                sb.AppendLine($"{aqua.GetInfo()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
