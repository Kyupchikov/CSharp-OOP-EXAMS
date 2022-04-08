using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const int minPower = 250;
        private const int maxPower = 450;

        public SportsCar(string model, int horsePower)
            : base(model, horsePower, 3000, 250, 450)
        {

        }
    }
}
