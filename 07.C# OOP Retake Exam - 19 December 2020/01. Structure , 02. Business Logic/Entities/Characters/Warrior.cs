using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Inventory.Contracts;

namespace WarCroft.Entities.Characters
{
    public class Warrior : Character, IAttacker
    {
       

        // The Warrior class always has 100 Base Health, 50 Base Armor, 40 Ability Points, and a Satchel as a bag.

        public Warrior(string name)
            : base(name, 100, 50, 40, new Satchel())
        {
          
        }

        

        //        For a character to attack another character, both of them need to be alive.
        //If the character they are trying to attack is the same character, throw an InvalidOperationException with the message "Cannot attack self!"
        //If all of those checks pass, the receiving character takes damage equal to the attacking character’s ability points.The damage is subtracted
        //from the armor points first and once there is no more armor points, from the health points of the receiver.

        public void Attack(Character character)
        {
            if (this == character)
            {
                throw new InvalidOperationException("Cannot attack self!");
            }

            if (!this.IsAlive || !character.IsAlive)
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }

            if (this.IsAlive && character.IsAlive)
            {
                character.TakeDamage(this.AbilityPoints);
            }
        }
    }
}
