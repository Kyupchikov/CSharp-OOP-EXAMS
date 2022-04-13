using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }


        // •	Models - a collection of races (unmodifiable)

        public IReadOnlyCollection<IRace> Models
            => this.races.AsReadOnly();

        // •	Adds a race to the collection.
        public void Add(IRace model)
        {
            this.races.Add(model);
        }

        // •	Returns the first race of a given model. Otherwise, returns null.

        public IRace FindByName(string name)
        {
            return this.races.FirstOrDefault(x => x.RaceName == name);
        }

        // •	Removes a race from the collection. Returns true if the deletion was successful, otherwise - false.

        public bool Remove(IRace model)
        {
            return this.races.Remove(model);
        }
    }
}
