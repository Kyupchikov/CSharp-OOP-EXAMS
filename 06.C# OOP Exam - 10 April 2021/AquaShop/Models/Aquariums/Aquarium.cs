using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private readonly List<IDecoration> decorations;
        private readonly List<IFish> fish;
        private int comfort;

        //                 string name, int capacity
        protected Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;

            this.decorations = new List<IDecoration>();
            this.fish = new List<IFish>();
        }

        // o	If the name is null or whitespace, throw an ArgumentException with message: "Aquarium name cannot be null or empty."

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Aquarium name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        // o	The number of Fish аn Aquarium can have

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                this.capacity = value;
            }
        }

        // •	Comfort - calculated property, which returns int
        //  o How is it calculated: The sum of each decoration’s comfort in the Aquarium

        public int Comfort
        {
            get => this.comfort = this.decorations.Sum(x => x.Comfort);
        }

        public ICollection<IDecoration> Decorations
                => this.decorations;

        public ICollection<IFish> Fish
                => this.fish;

        // Adds a Decoration in the Aquarium.

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        // Adds a Fish in the Aquarium if there is capacity for it, otherwise throw an InvalidOperationException with message "Not enough capacity.";

        public void AddFish(IFish fish)
        {
            if (this.Capacity == this.Fish.Count)
            {
                throw new InvalidOperationException("Not enough capacity.");
            }

            this.fish.Add(fish);
        }

        // The Feed() method feeds all fish, calls their Eat() method.

        public void Feed()
        {
            foreach (var smallFish in this.fish)
            {
                smallFish.Eat();
            }
        }

        //        Returns a string with information about the Aquarium in the format below.If the Aquarium doesn't have fish, print "none" instead.
        //"{aquariumName} ({aquariumType}):
        //Fish: {fishName1
        //    }, {fishName2
        //}, { fishName3} (…) / none
        //Decorations: { decorationsCount}
        //Comfort: { aquariumComfort}
        //"

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");

            if (this.fish.Count == 0)
            {
                sb.AppendLine("Fish: none");
            }
            else
            {
                sb.Append("Fish: ");




                sb.AppendLine(string.Join(", ", this.fish.Select(x => x.Name)));

                //int counter = 0;
                //foreach (var smallFish in this.fish)
                //{
                //    counter++;
                //    if (counter == this.fish.Count)
                //    {
                //        sb.Append($"{smallFish.Name}");
                //        //  sb.Append($"{smallFish.Name} :{smallFish.Size} size");
                //        break;
                //    }
                //    sb.Append($"{smallFish.Name}, ");
                //    //  sb.Append($"{smallFish.Name}: {smallFish.Size} size, ");
                //}
                //sb.AppendLine();


            }
            sb.AppendLine($"Decorations: {this.decorations.Count}");
            sb.Append($"Comfort: {this.Comfort}");

            return sb.ToString().TrimEnd();
        }

        // Removes a Fish from the Aquarium. Returns true if the Fish is removed successfully, otherwise - false.

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }
    }
}
