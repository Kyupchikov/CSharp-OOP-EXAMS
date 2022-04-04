using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;


namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
           // IRacer racerOne = null;

            //if (racerOne.RacingBehavior == "strict")
            //{
            //    racer1 = new ProfessionalRacer(racerOne.Username, racerOne.Car);
            //}
            //else if (racerOne.RacingBehavior == "aggressive")
            //{
            //    racer1 = new StreetRacer(racerOne.Username, racerOne.Car);
            //}

          //  IRacer racerTwo = null;

            //if (racerTwo.RacingBehavior == "strict")
            //{
            //    racer2 = new ProfessionalRacer(racerTwo.Username, racerTwo.Car);
            //}
            //else if (racerTwo.RacingBehavior == "aggressive")
            //{
            //    racer2 = new StreetRacer(racerTwo.Username, racerTwo.Car);
            //}

            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return $"Race cannot be completed because both racers are not available!";
            }

            if (!racerOne.IsAvailable())
            {
                return $"{racerTwo.Username} wins the race! {racerOne.Username} was not available to race!";
            }
            else if (!racerTwo.IsAvailable())
            {
                return $"{racerOne.Username} wins the race! {racerTwo.Username} was not available to race!";
            }


            racerOne.Race();
            racerTwo.Race();

            // •	chanceOfWinning = horsePower * drivingExperience * racingBehaviorMultiplier

            double racer1Chance = racerOne.Car.HorsePower * racerOne.DrivingExperience;
            double racer2Chance = racerTwo.Car.HorsePower * racerTwo.DrivingExperience;

            if (racerOne.RacingBehavior == "strict")
            {
                racer1Chance *= 1.2;
            }
            else if (racerOne.RacingBehavior == "aggressive")
            {
                racer1Chance *= 1.1;
            }

            if (racerTwo.RacingBehavior == "strict")
            {
                racer2Chance *= 1.2;
            }
            else if (racerTwo.RacingBehavior == "aggressive")
            {
                racer2Chance *= 1.1;
            }

            string winnerName;
            if (racer1Chance > racer2Chance)
            {
                winnerName = racerOne.Username;
            }
            else
            {
                winnerName = racerTwo.Username;
            }

            return $"{racerOne.Username} has just raced against {racerTwo.Username}! {winnerName} is the winner!";
        }
    }
}
