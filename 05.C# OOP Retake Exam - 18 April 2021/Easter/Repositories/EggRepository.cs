using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private readonly List<IEgg> eggs;

        public EggRepository()
        {
            this.eggs = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models
            => this.eggs.AsReadOnly();

        //        •	Adds an egg in the collection.
        //•	Every egg is unique and it is guaranteed that there will not be a egg with the same name

        public void Add(IEgg model)
        {
            this.eggs.Add(model);
        }

        // •	Returns the first egg with the given name, if such exists. Otherwise, returns null.

        public IEgg FindByName(string name)
        {
            return this.eggs.FirstOrDefault(x => x.Name == name);
        }

        // •	Removes a egg from the collection. Returns true if the deletion was sucessful, otherwise - false.

        public bool Remove(IEgg model)
        {
            return this.eggs.Remove(model);
        }
    }
}
