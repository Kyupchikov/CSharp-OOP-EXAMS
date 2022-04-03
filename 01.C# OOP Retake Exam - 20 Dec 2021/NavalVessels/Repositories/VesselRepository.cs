using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private  List<IVessel> models;

        //•	Models – a collection of vessels (unmodifiable)
        public VesselRepository()
        {
            this.models = new List<IVessel>(); 
        }

        public IReadOnlyCollection<IVessel> Models 
               => models;

        //•	Adds a vessel in the vessel’s collection.
        //•	    Every vessel is unique and it is guaranteed that there will not be a vessel with the same name.

        public void Add(IVessel model)
        {
            models.Add(model);
        }

        // •	Returns a vessel with that name if he exists. If he doesn't, returns null.

        public IVessel FindByName(string name)
        {
            return models.FirstOrDefault(x => x.Name == name);
        }

        // •	Removes a vessel from the collection. Returns true if the deletion was successful.

        public bool Remove(IVessel model)
        {
            return models.Remove(model);
        }
    }
}
