using System;

using WarCroft.Entities.Characters.Contracts;
using WarCroft.Constants;

namespace WarCroft.Entities.Items
{
    // Christmas came early this year - this class is already implemented for you!

    public abstract class Item
    {
        private int weight;

        protected Item(int weight)
        {
            this.Weight = weight;
        }

        public int Weight
        {
			get => this.weight;
            private set
            {
                this.weight = value;
            }
        }

        //		For an item to affect a character, the character needs to be alive.
        //The character’s health gets increased by 20 points.


        public virtual void AffectCharacter(Character character)
        {
            if (!character.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }

            character.Health += 20;
        }
    }
}
