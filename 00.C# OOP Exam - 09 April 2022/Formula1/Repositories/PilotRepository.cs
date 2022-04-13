using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots;

        public PilotRepository()
        {
            this.pilots = new List<IPilot>();
        }

        // •	Models - a collection of pilots (unmodifiable

        public IReadOnlyCollection<IPilot> Models
            => this.pilots.AsReadOnly();

        // •	Adds a pilot to the collection.

        public void Add(IPilot model)
        {
            this.pilots.Add(model);
        }

        // Returns the first pilot with the given fullName. Otherwise, returns null

        public IPilot FindByName(string name)
        {
            return this.pilots.FirstOrDefault(x => x.FullName == name);
        }

        // •	Removes a pilot from the collection. Returns true if the deletion was successful, otherwise - false.

        public bool Remove(IPilot model)
        {
            return this.pilots.Remove(model);
        }
    }
}
