using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {

        //      Can only live in FreshwaterAquarium!

        public FreshwaterFish(string name, string species, decimal price)
            : base(name, species, price)
        {

        }

        // •	The method increases the fish’s size by 3.

        public override void Eat()
        {
            this.Size += 3;
        }
    }
}
