using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private ICollection<IEquipment> equipments;
        private ICollection<IAthlete> athletes;

        private string name;
        private int capacity;

        protected Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;

            this.equipments = new List<IEquipment>();
            this.athletes = new List<IAthlete>();
        }

        public double EquipmentWeight
                => this.equipments.Sum(x => x.Weight);

        // o	If the name is null or empty, throw an ArgumentException with a message: "Gym name cannot be null or empty."

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gym name cannot be null or empty.");
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                capacity = value;
            }
        }



        public ICollection<IEquipment> Equipment
                => this.equipments;

        public ICollection<IAthlete> Athletes
                => this.athletes;

        // Adds an athlete in the gym if there is space left for him/her, otherwise throw an InvalidOperationException with a message "Not enough space in the gym.".

        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count == Capacity)
            {
                throw new InvalidOperationException("Not enough space in the gym.");
            }

            athletes.Add(athlete);
        }

        // Adds a piece of equipment in the gym.

        public void AddEquipment(IEquipment equipment)
        {
            equipments.Add(equipment);
        }

        // The Exercise() method trains all athletes, by calling their Exercise() method.

        public void Exercise()
        {
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        //        Returns a string with information about the gym in the format below:
        //"{gymName} is a {gymType}:
        //Athletes: {athleteName1}, {athleteName2}, { athleteName3} (…) / No athletes
        //Equipment total count: { equipmentCount}
        //Equipment total weight: { equipmentWeight} grams "


        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:");
            var result = this.Athletes.ToList();

            if (result.Count > 0)
            {
                    sb.Append($"Athletes: ");
                for (int i = 0; i < this.Athletes.Count; i++)
                {
                    if (i == this.Athletes.Count - 1)
                    {
                        sb.AppendLine($"{result[i].FullName}");
                        break;
                    }
                    sb.Append($"{result[i].FullName}, ");
                }
            }
            else
            {
                sb.AppendLine("Athletes: No athletes");
            }

            sb.AppendLine($"Equipment total count: { this.Equipment.Count}");
            sb.AppendLine($"Equipment total weight: { this.EquipmentWeight:f2} grams");
            
            return sb.ToString();
        }

        // Removes an athlete from the gym. Returns true if the athlete is removed successfully, otherwise - false.

        public bool RemoveAthlete(IAthlete athlete)
        {
            return this.RemoveAthlete(athlete);
        }
    }
}
