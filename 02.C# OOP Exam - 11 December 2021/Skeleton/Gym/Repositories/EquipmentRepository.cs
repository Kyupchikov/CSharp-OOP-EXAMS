using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipment;

        public EquipmentRepository()
        {
            this.equipment = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models 
                => (IReadOnlyCollection<IEquipment>)this.equipment;

        // •	Added equipment to the collection.

        public void Add(IEquipment model)
        {
            equipment.Add(model);
        }

       // •	Returns the first equipment of the given type, if there is. Otherwise, returns null.

        public IEquipment FindByType(string type)
        {

            foreach (var item in equipment)
            {
                if (item .GetType().Name == type)
                {
                    return (IEquipment)item;
                }
            }
                return null;
        }

        // •	Removes a piece of equipment from the collection. Returns true if the deletion was successful, otherwise - false

        public bool Remove(IEquipment model)
        {
           return this.equipment.Remove(model);
        }
    }
}
