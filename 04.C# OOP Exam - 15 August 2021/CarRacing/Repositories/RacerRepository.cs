using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> racers = new List<IRacer>();

        public IReadOnlyCollection<IRacer> Models => this.racers;

        // •	If the racer is null, throw an ArgumentException with message: "Cannot add null in Racer Repository".

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Racer Repository");
            }

            racers.Add(model);
        }

        // •	Returns the first player with the given username, if there is such player. Otherwise, returns null

        public IRacer FindBy(string property)
        {
            return racers.FirstOrDefault(x => x.Username == property);
        }

        // •	Removes a racer from the collection. Returns true if the removal was sucessful, otherwise - false

        public bool Remove(IRacer model)
        {
            return racers.Remove(model);
        }
    }
}
