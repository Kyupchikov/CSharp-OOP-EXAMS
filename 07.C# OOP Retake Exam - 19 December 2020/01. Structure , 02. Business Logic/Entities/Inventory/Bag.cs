using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private int capacity;
        private readonly List<Item> items;

        protected Bag(int capacity)
        {
            Capacity = capacity;
            this.items = new List<Item>();
            this.Capacity = 100;
        }

        //        Capacity – an integer number.Default value: 100
        //Load – Calculated property.The sum of the weights of the items in the bag.
        //Items – Read-only collection of type Item

        public int Capacity
        {
            get => this.capacity;
            set
            {
                this.capacity = value;
            }
        }

        public int Load
            => this.Items.Sum(x => x.Weight);

        public IReadOnlyCollection<Item> Items
            => this.items.AsReadOnly();

        //        If the current load + the weight of the item attempted to be added is greater than the bag’s capacity,
        //        throw an InvalidOperationException with the message "Bag is full!"
        //        If the check passes, the item is added to the bag.

        public void AddItem(Item item)
        {
            if (this.Load + item.Weight > this.capacity)
            {
                throw new InvalidOperationException("Bag is full!");
            }

            this.items.Add(item);
        }

        //        If no items exist in the bag, throw an InvalidOperationException with the message "Bag is empty!"
        //If an item with that name doesn’t exist in the bag, throw an ArgumentException with the message "No item with name {name} in bag!"
        //If both checks pass, the item is removed from the bag and returned to the caller

        public Item GetItem(string name)
        {
            if (this.Items.Count == 0)
            {
                throw new InvalidOperationException("Bag is empty!");
            }

            if (!this.Items.Any(x => x.GetType().Name == name))
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }

            var result = this.items.First(x => x.GetType().Name == name);
            this.items.Remove(result);

            return result;
        }
    }
}
