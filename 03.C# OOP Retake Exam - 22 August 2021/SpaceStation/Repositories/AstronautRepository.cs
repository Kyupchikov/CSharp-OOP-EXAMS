using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> astronauts;

        public AstronautRepository()
        {
            this.astronauts = new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models
            => this.astronauts;

        //        •	Adds an astronaut in the Space Station.
        //Every astronaut is unique and it is guaranteed that there will not be an astronaut with the same name

        public void Add(IAstronaut model)
        {
            astronauts.Add(model);
        }

        //•	Returns an astronaut with that name, if he exists.If he doesn't, returns null.

        public IAstronaut FindByName(string name)
        {
            return this.astronauts.FirstOrDefault(x => x.Name == name);
        }

        // •	Removes an astronaut from the collection. Returns true if the deletion was sucessful.

        public bool Remove(IAstronaut model)
        {
            return this.astronauts.Remove(model);
        }
    }
}
