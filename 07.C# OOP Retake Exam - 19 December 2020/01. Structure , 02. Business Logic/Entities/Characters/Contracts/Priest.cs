using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Inventory.Contracts;

namespace WarCroft.Entities.Characters.Contracts
{
    public class Priest : Character, IHealer
    {
      

        // The Priest class always has 50 Base Health, 25 Base Armor, 40 Ability Points, and a Backpack as a bag.
        public Priest(string name)
            : base(name, 50, 25, 40, new Backpack())
        {
          
        }

        //        For a character to heal another character, both of them need to be alive.
        //If this is true, the receiving character’s health increases by the healer’s ability points.

        public void Heal(Character character)
        {
            if (!this.IsAlive || !character.IsAlive)
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }

            if (this.IsAlive && character.IsAlive)
            {
                character.Health += this.AbilityPoints;
            }
        }
    }
}
