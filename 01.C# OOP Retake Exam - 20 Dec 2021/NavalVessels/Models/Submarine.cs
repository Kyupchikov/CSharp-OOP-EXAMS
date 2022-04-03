using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private bool submergeMode;

        // Has 200 initial armor thickness.
        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, 200)
        {
            SubmergeMode = false;
        }


        public bool SubmergeMode
        {
            get => submergeMode;
            private set
            {
                submergeMode = value;
            }
        }

        public override void RepairVessel()
        {
            this.ArmorThickness = 200;
        }

        public void ToggleSubmergeMode()
        {
            if (this.SubmergeMode == false)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
                this.SubmergeMode = true;
            }
            else
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
                this.SubmergeMode = false;
            }
        }

        public override string ToString()
        {
            string mode;

            if (this.SubmergeMode == true)
            {
                mode = "ON";
            }
            else
            {
                mode = "OFF";
            }
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($" *Submerge mode: {mode}");

            return sb.ToString().TrimEnd();
        }
    }
}
