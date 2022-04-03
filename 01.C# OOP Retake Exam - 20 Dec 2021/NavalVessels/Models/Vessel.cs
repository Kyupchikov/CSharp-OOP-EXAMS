using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private double mainWeaponCaliber;
        private double speed;
        private double armorThickness;
        private ICaptain captain;
        private readonly List<string> targets;

        //            string name, double mainWeaponCaliber, double speed, double armorThickness
        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            targets = new List<string>();
        }

        // •	Name - string, if the name is null or whitespace throws an ArgumentNullException with a message "Vessel name cannot be null or empty."

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(name), "Vessel name cannot be null or empty.");
                }

                name = value;
            }
        }

        // •	Captain – the vessel’s captain, if it is null throw NullReferenceException with a message "Captain cannot be null."

        public ICaptain Captain
        {
            get => captain;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Captain cannot be null.");
                }

                captain = value;
            }
        }

        public double ArmorThickness
        {
            get => armorThickness;
            set
            {
                armorThickness = value;
            }
        }

        public double MainWeaponCaliber
        {
            get => mainWeaponCaliber;
            protected set
            {
                mainWeaponCaliber = value;
            }
        }

        public double Speed
        {
            get => speed;
            protected set
            {
                speed = value;
            }
        }

        public ICollection<string> Targets
        {
            get => targets;
        }

        //        If the target(defending vessel) is null throw NullReferenceException with a message "Target cannot be null."
        //        When the attacking vessel attacks the target vessel, the target's armor thickness points are reduced by the attacking vessel's main weapon caliber points.
        //        Keep in mind that the target's armor thickness points can not go below zero. If the target's armor thickness points become a negative number,
        //        set it to zero.Add the name of the target vessel to the attacker's list of targets.

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null.");
            }

            target.ArmorThickness -= this.MainWeaponCaliber;
            targets.Add(target.Name);

            if (target.ArmorThickness <= 0)
            {
                target.ArmorThickness = 0;
            }
        }

        // Set the vessel’s initial armor thickness to the default value based on the vessel type.

        public abstract void RepairVessel();

        //        Returns a string with information about each vessel.The returned string must be in the following format:
        //"- {vessel name}
        // *Type: { vessel type name}
        // *Armor thickness: {vessel armor thickness points}
        // * Main weapon caliber: {vessel main weapon caliber points}
        //*Speed: { vessel speed points} knots
        //* Targets: " – if there are no targets "None " Otherwise print "{ target1}, { target2}, { target3}, { targetN}"

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"- {this.name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Armor thickness: {this.ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {this.MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {this.Speed} knots");
            if (this.targets.Count > 0)
            {
                sb.Append(" *Targets: ");
                sb.AppendLine(string.Join(", ", this.targets));
            }
            else
            {
                sb.AppendLine(" *Targets: None");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
