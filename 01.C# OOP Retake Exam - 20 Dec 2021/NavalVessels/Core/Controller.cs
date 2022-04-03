using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {

        //•	vessels - VesselRepository
        //•	captains - a collection of ICaptain

        private VesselRepository vessels;
        private readonly List<ICaptain> captains;

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }

        //Searches for a captain and vessel by given names.
        //As a result, the command returns one of the following messages: 
        //•	If the captain does not exist return: "Captain {selectedCaptainName} could not be found."
        //•	If the vessel does not exist return: "Vessel {selectedVesselName} could not be found."
        //•	If the vessel has a captain return: "Vessel {selectedVesselName} is already occupied."
        //•	If the captain is successfully assigned to the vessel return: "Captain {selectedCaptainName} command vessel {selectedVesselName}."
        //and add the vessel to the captain's list of vessels and set the vessel's captain to the selectedCaptainFullName
        //NOTE: Follow the exact order of messages.

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            ICaptain captain = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);
            IVessel vessel = vessels.FindByName(selectedVesselName);

            if (captain == null)
            {
                return $"Captain {selectedCaptainName} could not be found.";
            }
            else if(vessel == null)
            {
                return $"Vessel {selectedVesselName} could not be found.";
            }
            else if (vessel.Captain != null)
            {
                return $"Vessel {selectedVesselName} is already occupied.";
            }

            captain.Vessels.Add(vessel);
            vessel.Captain = captain;

            return $"Captain {selectedCaptainName} command vessel {selectedVesselName}.";
        }

        //        Searches for two vessels by given names and the first one attacks the second one.As a result, the command returns one of the following messages:
        //•	If one of the vessels doesn't exist, the attacking vessel is with priority return: "Vessel {name} could not be found." 
        //•	If one of the vessels has armor thickness equal to zero, the attacking vessel is with priority return: "Unarmored vessel {name} cannot attack or be attacked."
        //•	If all the criteria are matched invoke the attacking vessel Attack() method, increase combat experience of both vessel’s captains and return:
        // "Vessel {defendingVessleName} was attacked by vessel {attackVessleName} - current armor thickness: {defenderArmorThinckness}."
        //NOTE: Both the attacking vessel and the defending vessel will always have captains.

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var attackingVessel = vessels.FindByName(attackingVesselName);
            var defendingVessel = vessels.FindByName(defendingVesselName);

            if (attackingVessel == null)
            {
                return $"Vessel {attackingVesselName} could not be found.";
            }
            else if (defendingVessel == null)
            {
                return $"Vessel {defendingVesselName} could not be found.";
            }

            if (attackingVessel.ArmorThickness == 0)
            {
                return $"Unarmored vessel {attackingVesselName} cannot attack or be attacked.";
            }
            else if (defendingVessel.ArmorThickness == 0)
            {
                return $"Unarmored vessel {defendingVesselName} cannot attack or be attacked.";
            }

            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();

            return $"Vessel {defendingVessel.Name} was attacked by vessel {attackingVessel.Name} - current armor thickness: {defendingVessel.ArmorThickness}.";
        }

        //          Functionality
        //  Searches for an assigned captain with a given name and returns the ICaptain.Report() method result.

        public string CaptainReport(string captainFullName)
        {
            var capitan = captains.FirstOrDefault(x => x.FullName == captainFullName);

            return capitan.Report();
        }

        //        Creates a captain with the provided full name and adds him/her to the collection of captains.The method should return one of the following messages:
        //•	If the captain is hired successfully return: "Captain {fullName} is hired." and add him/her to the collection of captains.
        //•	If a captain with the given name already exists return: "Captain {fullName} is already hired.", and the given captain should not be hired.

        public string HireCaptain(string fullName)
        {
            if (captains.Any(x => x.FullName == fullName))
            {
                return $"Captain {fullName} is already hired.";
            }

            ICaptain newCaptain = new Captain(fullName);

            captains.Add(newCaptain);
            return $"Captain {fullName} is hired.";
        }

        //public string HireCaptain(string fullName)
        //{
        //    if (this.captains.Any(p => p.FullName == fullName))
        //    {
        //        return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
        //    }

        //    Captain captain = new Captain(fullName);
        //    this.captains.Add(captain);

        //    return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        //}

        //  Creates a Vessel of the given type(Submarine or Battleship) with a given name, main weapon caliber, and speed points.
        //  The method should return one of the following messages:
        //•	If the vessel with the given name exists return: "{typeVessel} vessel {name} is already manufactured."
        //•	If the vesselType is invalid return: "Invalid vessel type."
        //•	If the vessel is successfully produced return: "{typeVessel} {name} is manufactured with the main weapon caliber of {mainWeapon}
        //  inches and a maximum speed of {speed} knots." and adds the vessel to the VesselRepository.

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            IVessel vessel;
            IVessel targetVessel = vessels.FindByName(name);

            if (targetVessel != null)
            {
                return $"{targetVessel.GetType().Name} vessel {name} is already manufactured.";
            }

            if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                return "Invalid vessel type.";
            }

            vessels.Add(vessel);

            return $"{vessel.GetType().Name} {name} is manufactured with the main weapon caliber of {mainWeaponCaliber} inches and a maximum speed of {speed} knots.";
        }

        //        Functionality
        //Search for a vessel with the given name and invoke its RepairVessel() method.As a result, the command returns one of the following messages:
        //•	If the vessel is successfully repaired return:  "Vessel {name} was repaired."
        //•	If the vessel does not exist return: "Vessel {name} could not be found."

        public string ServiceVessel(string vesselName)
        {
            var targetVessel = vessels.FindByName(vesselName);

            if (targetVessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            targetVessel.RepairVessel();

            return $"Vessel {vesselName} was repaired.";
        }

        //        Searches for a vessel with a given name and toggles its special mode.As a result, the command returns one of the following messages:
        //•	If the vessel is battleship and does exist, execute ToggleSonarMode() and return: "Battleship {name} toggled sonar mode."
        //•	If the vessel is submarine and does exist, execute ToggleSubmergeMode() and return:  "Submarine {name} toggled submerge mode."
        //•	If the vessel does not exist return: "Vessel {name} could not be found."

        public string ToggleSpecialMode(string vesselName)
        {
            var targetVessel = vessels.FindByName(vesselName);

            if (targetVessel == null)
            {
                return $"Vessel {vesselName} could not be found.";
            }

            if (targetVessel.GetType().Name.ToLower() == "battleship")
            {
               var target1Vessel = targetVessel as IBattleship;
                target1Vessel.ToggleSonarMode();

                return $"Battleship {target1Vessel.Name} toggled sonar mode.";
            }
            else if (targetVessel.GetType().Name.ToLower() == "submarine")
            {
                var target1Vessel = targetVessel as ISubmarine;
                target1Vessel.ToggleSubmergeMode();

                return $"Submarine {target1Vessel.Name} toggled submerge mode.";
            }

            return $"Vessel {vesselName} could not be found.";
        }

        //  Functionality
        //  Searches for an existing vessel with a given name and returns ToString() method result.

        public string VesselReport(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            return vessel.ToString();
        }
    }
}
