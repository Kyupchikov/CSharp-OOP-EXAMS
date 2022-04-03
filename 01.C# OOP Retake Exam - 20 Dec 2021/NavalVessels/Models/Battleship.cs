using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private bool sonarMode;

        //          Has 300 initial armor thickness.
        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, 300)
        {
            SonarMode = false;
        }

        public bool SonarMode
        {
            get => sonarMode;
            private set
            {
                sonarMode = value;
            }
        }

        // Set the vessel’s initial armor thickness to the default value based on the vessel type.

        public override void RepairVessel()
        {
            this.ArmorThickness = 300;
        }

        //        Flips SonarMode(false -> true or true -> false). 
        //        When SonarMode is activated(false -> true) :
        //     •  The main weapon caliber is increased by 40 points
        //     •  Speed is decreased by 5 points
        //        When SonarMode is deactivated(true -> false) :
        //     •  The main weapon caliber is decreased by 40 points
        //     •  Speed is increased by 5 points


        public void ToggleSonarMode()
        {
            if (this.sonarMode == false)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
                this.SonarMode = true;
            }
            else
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
                this.SonarMode = false;
            }
        }

        public override string ToString()
        {
            string mode;

            if (this.SonarMode == true)
            {
                 mode = "ON";
            }
            else
            {
                mode = "OFF";
            }
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($" *Sonar mode: {mode}");

            return sb.ToString().TrimEnd();
        }
    }
}
