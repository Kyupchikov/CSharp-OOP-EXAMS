using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private bool tookPlace;
        private readonly List<IPilot> pilots;

        //          string raceName, int numberOfLaps
        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            this.pilots = new List<IPilot>();
            this.TookPlace = false;
        }

        // o	If the race name is null, white space or the length is less than 5 symbols,
        // throw an ArgumentException with a message: "Invalid race name: { race name }."

        public string RaceName
        {
            get => raceName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Invalid race name: {value}.");
                }

                raceName = value;
            }
        }

        // o	If the number of laps is less than 1, throw an ArgumentException with a message: "Invalid lap numbers: { number of laps }."

        public int NumberOfLaps
        {
            get => numberOfLaps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException($"Invalid lap numbers: {value}.");
                }

                numberOfLaps = value;
            }
        }

        //o	Should be set to false as default

        public bool TookPlace
        {
            get => tookPlace;
            set
            {
                tookPlace = value;
            }
        }

        public ICollection<IPilot> Pilots
            => this.pilots.AsReadOnly();

        // Adds a pilot to the race.

        public void AddPilot(IPilot pilot)
        {
           Pilot pilot1 =  pilot as Pilot;

            this.pilots.Add(pilot1);
          //  pilot1.CanRace = false;
        }

        //        Returns a string with information about the race in the format below:
        //"The { race name } race has:
        //Participants: { number of participants}
        //    Number of laps: { number of laps}
        //Took place: { Yes / No }"

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"The {this.RaceName} race has:");
            sb.AppendLine($"Participants: {this.Pilots.Count}");
            sb.AppendLine($"Number of laps: {this.NumberOfLaps}");
            sb.AppendLine($"Took place: {(this.TookPlace ? "Yes" : "No")}");

            return sb.ToString().TrimEnd();
        }
    }
}
