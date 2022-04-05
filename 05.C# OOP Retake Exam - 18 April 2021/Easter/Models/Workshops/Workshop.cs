using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;


namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {

        //        Here is how the Color method works:
        //•	The bunny starts coloring the egg.This is only possible, if the bunny has energy and an dye that isn't finished.
        //•	At the same time the egg is getting colored, so call the GetColored() method for the egg. 
        //•	Keep working until the egg is done or the bunny has energy and dyes to use.
        //•	If at some point the power of the current dye reaches or drops below 0, meaning it is finished,
        //then the Bunny should take the next Dye from its collection, if it has any left.

        public void Color(IEgg egg, IBunny bunny)
        {
            Bunny bunny1 = bunny as Bunny;
            IDye dye = bunny1.dyes[0];

            while (true)
            {

                if (bunny1.Energy == 0)
                {
                    break;
                }

                if (bunny1.Dyes.Count == 0)
                {
                    break;
                }

                bunny.Work();
                egg.GetColored();
                dye.Use();

                if (egg.IsDone())
                {
                    break;
                }
                if (dye.IsFinished())
                {
                    bunny1.dyes.Remove(dye);

                    if (bunny1.Dyes.Count > 0)
                    {
                        dye = bunny1.dyes[0];
                    }
                }
            }
        }
    }
}
