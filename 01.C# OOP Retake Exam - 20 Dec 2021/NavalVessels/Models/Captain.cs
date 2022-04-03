using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private string fullName;
        private int combatExperience;
        private readonly List<IVessel> vessels;

        public Captain(string fullName)
        {
            FullName = fullName;
            CombatExperience = 0;
            vessels = new List<IVessel>();
        }

        // •	FullName – string, if the captain’s name is null or whitespace throw ArgumentNullException
        //      with a message "Captain full name cannot be null or empty string."

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Captain full name cannot be null or empty string.");
                }

                fullName = value;
            }
        }

        // •	CombatExperience – int, with the initial value of 0, could be increased by 10.

        public int CombatExperience
        {
            get => combatExperience;
            private set
            {
                combatExperience = value;
            }
        }

        public ICollection<IVessel> Vessels
            => this.vessels;

      //  Adds the provided vessel to the captain’s vessels.
      //      If the provided vessel is null throw NullReferenceException with a message: "Null vessel cannot be added to the captain."

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException("Null vessel cannot be added to the captain.");
            }

            this.vessels.Add(vessel);
        }

        //Increase captain’s combat experience by 10 when a vessel that he commands attack or defend.
        //There will be no case where the attacking vessel and defend vessel will have the same captain. 

        public void IncreaseCombatExperience()
        {
            this.CombatExperience += 10;
        }

        //        Returns the message in format:
        //"{FullName} has {CombatExperience} combat experience and commands {vessels count} vessels."
        //If the captain commands any vessel, return:
        //"- {vessel name}
        // *Type: {vessel type name
        //    }
        // * Armor thickness: {vessel armor thickness points}
        //*Main weapon caliber: { vessel main weapon caliber points}
        //*Speed: { vessel speed points}
        //knots
        //* Targets: None /{ targets}
        //*Sonar / Submerge mode: ON / OFF" 

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {this.vessels.Count} vessels.");

            if (this.vessels.Count > 0)
            {
                foreach (var item in this.vessels)
                {
                    sb.AppendLine($"{item}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
