using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace Easter.Core.Contracts
{
    public class Controller : IController
    {
        //        •	bunnies - BunnyRepository 
        //•	eggs - EggRepository

        private BunnyRepository bunnyRepository;
        private EggRepository eggRepository;

        public Controller()
        {
            this.bunnyRepository = new BunnyRepository();
            this.eggRepository = new EggRepository();
        }

        //        Adds a bunny.Valid types are: "HappyBunny" and "SleepyBunny".
        //If the bunny type is invalid, you have to throw an InvalidOperationException with the following message:
        //•	"Invalid bunny type."
        //If the bunny is added successfully, the method should return the following string:
        //•	"Successfully added {bunnyType} named {bunnyName}."

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny = null;

            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            this.bunnyRepository.Add(bunny);

            return $"Successfully added {bunnyType} named {bunnyName}.";
        }

        //        Creates a dye with the given power and adds it to the collection of the bunny.
        //If the bunny doesn't exist, throw an InvalidOperationException with message:
        //"The bunny you want to add a dye to doesn't exist!"
        //The method should return the following message:
        //"Successfully added dye with power {dyePower} to bunny {bunnyName}!"

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = this.bunnyRepository.FindByName(bunnyName);

            if (bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            IDye dye = new Dye(power);

            bunny.AddDye(dye);

            return $"Successfully added dye with power {power} to bunny {bunnyName}!";
        }

        //        Creates an egg with the provided name and required energy.
        //The method should return the following message:
        //"Successfully added egg: {eggName}!"

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);

            this.eggRepository.Add(egg);

            return $"Successfully added egg: {eggName}!";
        }

        //        When the color command is called, the action happens.
        //You should start coloring the given egg, by assigning bunnies which are most ready(first the bunnies with the most energy) :
        //•	The bunnies that you should select are the ones with energy equal to or above 50 units.
        //•	The suitable ones start working on the given egg.
        //•	If a bunny’s energy becomes 0, remove it from the repository.
        //•	If no bunnies are ready, throw InvalidOperationException with the following message: 
        //"There is no bunny ready to start coloring!"
        //•	After the work is done, you must return the following message, reporting whether the Egg is done:
        //"Egg {eggName} is {done/not done}."
        //Note: The name of the egg you receive will always be a valid one.

        public string ColorEgg(string eggName)
        {
            var myBuunyes = this.bunnyRepository.Models.OrderByDescending(x => x.Energy).Where(x => x.Energy >= 50).ToList();
            var egg = this.eggRepository.FindByName(eggName);
            IWorkshop workshop = new Workshop();

            if (myBuunyes.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            int counter = myBuunyes.Count();

            while (!egg.IsDone())
            {
                var bunnyToWork = myBuunyes.Where(x => x.Dyes.Count != 0).First();

                workshop.Color(egg, bunnyToWork);

                if (bunnyToWork.Energy == 0)
                {
                    this.bunnyRepository.Remove(bunnyToWork);
                }

                counter--;

                if (counter == 0)
                {
                    break;
                }
            }

            if (!egg.IsDone())
            {
                return $"Egg {eggName} is not done.";
            }

            return $"Egg {eggName} is done.";
        }

        //        Returns information about colored eggs and bunnies:
        //"{countColoredEggs} eggs are done!"
        //"Bunnies info:"
        //"Name: {bunnyName1}"
        //"Energy: {bunnyEnergy1}"
        //"Dyes: {countDyes} not finished"
        //…
        //"Name: {bunnyNameN}"
        //"Energy: {bunnyEnergyN}"
        //"Dyes {countDyes} not finished left"

        public string Report()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.eggRepository.Models.Where(x => x.IsDone() == true).Count()} eggs are done!");
            stringBuilder.AppendLine("Bunnies info:");

            foreach (var bunny in this.bunnyRepository.Models)
            {
                stringBuilder.AppendLine($"Name: {bunny.Name}");
                stringBuilder.AppendLine($"Energy: {bunny.Energy}");
                stringBuilder.AppendLine($"Dyes: {bunny.Dyes.Where(x => x.Power > 0).Count()} not finished");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
