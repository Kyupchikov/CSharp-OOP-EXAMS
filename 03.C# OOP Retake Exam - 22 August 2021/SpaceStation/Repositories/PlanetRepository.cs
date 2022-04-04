using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models
            => this.planets;

        //        •	Adds a planet for exploration.
        //•	Every planet is unique and it is guaranteed that there will not be a planet with the same name

        public void Add(IPlanet model)
        {
            this.planets.Add(model);
        }

        //        •	Returns a planet with that name.
        //•	It is guaranteed that the planet exists in the collection.

        public IPlanet FindByName(string name)
        {
            return this.planets.FirstOrDefault(x => x.Name == name);
        }

        // •	Removes a planet from the collection. Returns true if the deletion was sucessful.

        public bool Remove(IPlanet model)
        {
            return this.planets.Remove(model);
        }
    }
}
