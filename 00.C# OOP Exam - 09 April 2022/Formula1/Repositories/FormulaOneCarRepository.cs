using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private readonly List<IFormulaOneCar> cars;

        public FormulaOneCarRepository()
        {
            this.cars = new List<IFormulaOneCar>();
        }

        // •	Models - a collection of formula one cars(unmodifiable)

        public IReadOnlyCollection<IFormulaOneCar> Models
            => this.cars.AsReadOnly();

        // •	Adds a formula one car to the collection.

        public void Add(IFormulaOneCar model)
        {
            this.cars.Add(model);
        }

        // •	Returns the first car of a given model. Otherwise, returns null.

        public IFormulaOneCar FindByName(string name)
        {
            return this.Models.FirstOrDefault(x => x.Model == name);
        }

        // •	Removes a formula one car from the collection. Returns true if the deletion was successful, otherwise - false.

        public bool Remove(IFormulaOneCar model)
        {
            return this.cars.Remove(model);
        }
    }
}
