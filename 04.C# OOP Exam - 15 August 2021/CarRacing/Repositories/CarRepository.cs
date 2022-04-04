using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{

    public class CarRepository : IRepository<ICar>
    {

        private List<ICar> cars = new List<ICar>();

        public IReadOnlyCollection<ICar> Models => this.cars;

        // •	If the car is null, throw an ArgumentException with message: "Cannot add null in Car Repository".
        // •	Adds a car in the collection.

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException("Cannot add null in Car Repository");
            }

            cars.Add(model);
        }

        // •	Returns the car with the given VIN, if there is such a car. Otherwise, returns null.

        public ICar FindBy(string property)
        {
            return cars.FirstOrDefault(x => x.VIN == property);
        }

        // •	Removes a car from the collection. Returns true if the removal was sucessful, otherwise - false

        public bool Remove(ICar model)
        {
            return cars.Remove(model);
        }
    }
}
