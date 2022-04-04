using SpaceStation.Models.Bags.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Bags
{
    public class Backpack : IBag
    {
        protected readonly List<string> items;

        public Backpack()
        {
            this.items = new List<string>();
        }

        public ICollection<string> Items
                => this.items;
       
    }
}
