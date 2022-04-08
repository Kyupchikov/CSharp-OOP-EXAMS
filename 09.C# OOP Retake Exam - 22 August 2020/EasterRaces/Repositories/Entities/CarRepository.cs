using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars;

        public CarRepository()
        {
            this.cars = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models
             => this.cars;


        public void Add(ICar model)
        {
            cars.Add(model);
        }

        public IReadOnlyCollection<ICar> GetAll()
        {
            return this.Models;
        }

        public ICar GetByName(string name)
        {
            return this.cars.FirstOrDefault(x => x.Model == name);
        }

        public bool Remove(ICar model)
        {
           return cars.Remove(model);
        }
    }
}
