using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {

        //        •	The astronauts start going out in open space one by one.They can't go, if they don't have any oxygen left.
        //•	An astronaut lands on a planet and starts collecting its items one by one.
        //•	He finds an item and he takes a breath.
        //•	He adds the item to his backpack and respectively the item must be removed from the planet.
        //•	Astronauts can't keep collecting items if their oxygen becomes 0.
        //•	If it becomes 0, the next astronaut starts exploring.

        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            IAstronaut currentAstronaut = astronauts.FirstOrDefault();
            Planet currentPlanet = planet as Planet;

            while (currentAstronaut != null && currentPlanet.Items.Count > 0)
            {
                while (currentAstronaut.CanBreath)
                {
                    if (currentPlanet.Items.Count > 0)
                    {
                        currentAstronaut.Breath();
                        string temp = currentPlanet.items[0];
                        currentPlanet.items.RemoveAt(0);

                        currentAstronaut.Bag.Items.Add(temp);
                    }
                    else
                    {
                        break;
                    }
                }

                if (!currentAstronaut.CanBreath)
                {
                    astronauts.Remove(currentAstronaut);
                    currentAstronaut = astronauts.FirstOrDefault();
                }
            }
        }
    }
}
