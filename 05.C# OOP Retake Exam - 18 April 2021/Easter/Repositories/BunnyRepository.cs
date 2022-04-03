
using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private readonly List<IBunny> bunnies;

        public BunnyRepository()
        {
            this.bunnies = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models
             => this.bunnies.AsReadOnly();
        
        //        •	Adds a bunny in the collection.
        //•	Every bunny is unique and it is guaranteed that there will not be a Bunny with the same name

        public void Add(IBunny model)
        {
            this.bunnies.Add(model);
        }

        // •	Returns the first bunny with the given name, if such exists. Otherwise, returns null.

        public IBunny FindByName(string name)
        {
            return this.bunnies.FirstOrDefault(x => x.Name == name);
        }

        // •	Removes a bunny from the collection. Returns true if the deletion was sucessful, otherwise - false.

        public bool Remove(IBunny model)
        {
            return this.bunnies.Remove(model);
        }
    }
}
