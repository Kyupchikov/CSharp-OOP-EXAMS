using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private readonly List<IDecoration> decorations;

        public DecorationRepository()
        {
            this.decorations = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models
                => this.decorations;

        // •	Adds a decoration in the collection.

        public void Add(IDecoration model)
        {
            this.decorations.Add(model);
        }

        // •	Returns the first decoration of the given type, if there is. Otherwise, returns null.

        public IDecoration FindByType(string type)
        {
            return this.decorations.FirstOrDefault(x => x.GetType().Name == type);
        }

        // Removes a decoration from the collection. Returns true if the deletion was sucessful, otherwise - false

        public bool Remove(IDecoration model)
        {
            return this.decorations.Remove(model);
        }
    }
}
