using Gym.Models.Athletes.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Athletes
{
    public abstract class Athlete : IAthlete
    {
        private string fullName;
        private string motivation;
        private int numberOfMedals;
        private int stamina;
        private int increase = 0;

        protected Athlete(string fullName, string motivation, int numberOfMedals, int stamina)
        {
            FullName = fullName;
            Motivation = motivation;
            Stamina = stamina;
            NumberOfMedals = numberOfMedals;
        }

        // o	If the full name is null or empty, throw an ArgumentException with a message: "Athlete name cannot be null or empty."

        public string FullName
        {
            get => fullName; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Athlete name cannot be null or empty.");
                }

                fullName = value;
            }
        }

        // o	If the motivation is null or empty, throw an ArgumentException with a message: "The motivation cannot be null or empty."

        public string Motivation
        {
            get => motivation; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The motivation cannot be null or empty.");
                } 

                motivation = value; 
            }
        }

        public int Stamina
        {
            get => stamina; 
            protected set
            {
                // takes checks from the method???
                if (value > 100)
                {
                    stamina = 100;
                    throw new ArgumentException("Stamina cannot exceed 100 points.");
                }
                stamina = value; 
            }
        }

        // o	The number of medals which an athlete has earned
        // o    If the number of medals is below 0, throw an ArgumentException with a message:
        //      "Athlete's number of medals cannot be below 0."


        public int NumberOfMedals
        {
            get=> numberOfMedals; 
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Athlete's number of medals cannot be below 0.");
                }
                numberOfMedals = value; 
            }
        }

        // •	The method increases the boxer’s stamina by 15.
        // o    If total stamina exceeds 100, set the stamina to 100 and throw an ArgumentException with a message: "Stamina cannot exceed 100 points."


        public abstract void Exercise();
      // {
      //     Stamina += increase;
      // }
    }
}
