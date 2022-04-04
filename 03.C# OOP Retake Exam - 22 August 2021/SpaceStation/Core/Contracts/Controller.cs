using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Core.Contracts
{
    public class Controller : IController
    {
        private AstronautRepository astronauts;
        private PlanetRepository planets;
        private int exploredPlanetsCount = 0;

        public Controller()
        {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
        }

        //        Creates an astronaut with the given name of the given type.If the astronaut is invalid, throw an InvalidOperationException with message:
        //"Astronaut type doesn't exists!"
        //The method should return the following message:
        //•	"Successfully added {astronautType}: {astronautName}!"

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;

            if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            this.astronauts.Add(astronaut);

            return $"Successfully added {type}: {astronautName}!";
        }

        //        Creates a planet with the provided items and name.
        //When the planet is created, keep it and return the following message:
        //•	"Successfully added Planet: {planetName}!". 

        public string AddPlanet(string planetName, params string[] items)
        {
            Planet planet = new Planet(planetName);
            for (int i = 0; i < items.Length; i++)
            {
                planet.Items.Add(items[i]);
            }

            this.planets.Add(planet);

            return $"Successfully added Planet: {planetName}!";
        }

        //        •	You call each of the astronauts and pick only the ones that have oxygen above 60 units.
        //•	You send the suitable astronauts on a mission to explore the planet.
        //•	If you don't have any suitable astronauts, throw an InvalidOperationException with the following message:
        //"You need at least one astronaut to explore the planet"
        //•	After a mission, you must return the following message, with the name of the explored planet and
        //the count of the astronauts that had given their lives for the mission:
        //"Planet: {planetName} was explored! Exploration finished with {deadAstronauts} dead astronauts!"

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> myastronauts = new List<IAstronaut>();
            IPlanet planet = this.planets.FindByName(planetName);

            foreach (var item in astronauts.Models)
            {
                if (item.Oxygen > 60)
                {
                    myastronauts.Add(item);
                }
            }

            int astronautsCount = myastronauts.Count;

            if (myastronauts.Count == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet!");
            }

            IMission mission = new Mission();

            mission.Explore(planet, myastronauts);
            int deadAstronauts = astronautsCount - myastronauts.Count;
            exploredPlanetsCount++;

            return $"Planet: {planetName} was explored! Exploration finished with {deadAstronauts} dead astronauts!";
        }

        //        Returns the information about the astronauts.If any of them doesn't have bag items, print "none" instead.
        //"{exploredPlanetsCount} planets were explored!
        //Astronauts info:
        //Name: { astronautName}
        //        Oxygen: {astronautOxygen
        //    }
        //    Bag items: {bagItem1, bagItem2, …, bagItemn
        //} / none
        //…
        //Name: { astronautName}
        //Oxygen: { astronautOxygen}
        //Bag items: { bagItem1, bagItem2, …, bagItemn} / none"

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{exploredPlanetsCount} planets were explored!");
            sb.AppendLine($"Astronauts info:");

            foreach (var item in this.astronauts.Models)
            {
                sb.AppendLine($"Name: {item.Name}");
                sb.AppendLine($"Oxygen: {item.Oxygen}");
                if (item.Bag.Items.Count > 0)

                {
                    sb.AppendLine("Bag items: " + string.Join(", ", item.Bag.Items));
                }
                else
                {
                    sb.AppendLine($"Bag items: none");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //        Retires the astronaut from the space station by removing it from its repository.If an astronaut with that name doesn't exist,
        //        throw InvalidOperationException with return the following message:
        //•	"Astronaut {astronautName} doesn't exists!"
        // If an astronaut is successfully retired, remove it from the repository and return the following message:
        //•	"Astronaut {astronautName} was retired!"

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronautWanted = this.astronauts.FindByName(astronautName);

            if (astronautWanted == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            this.astronauts.Remove(astronautWanted);

            return $"Astronaut {astronautName} was retired!";
        }
    }
}
