using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
        // TODO: Implement the rest of the class.


        private string name;
        private double baseHealth;
        private double health;
        private double baseArmor;
        private double armor;
        private double abilityPoints;
        protected Bag satchel;
        private bool isAlive;

        //                  string name, double health, double armor, double abilityPoints, Bag bag
        protected Character(string name, double baseHealth, double baseArmor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = baseHealth;
            BaseArmor = baseArmor;
            Health = baseHealth;
            Armor = baseArmor;
            AbilityPoints = abilityPoints;
            Bag = bag;
            IsAlive = true;
        }

        //        •	Name – a string (cannot be null or whitespace).
        //o Throw an ArgumentException with the message "Name cannot be null or whitespace!"

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }

                name = value;
            }
        }

        //•	BaseHealth – a floating-point number – the starting and also the maximum health a character can have

        public double BaseHealth
        {
            get => this.baseHealth;
            private set
            {
                this.baseHealth = value;
            }
        }

        // •	Health – a floating-point number (current health).
        // o Health(current health) should never be more than the BaseHealth or less than 0. 

        public double Health
        {
            get => this.health;
            internal set
            {
                this.health = value;

                if (this.health > this.baseHealth)
                {
                    this.health = this.baseHealth;
                }

                if (this.health < 0)
                {
                    this.health = 0;
                }
            }
        }

        // •	BaseArmor – a floating-point number – the starting armor a character has

        public double BaseArmor
        {
            get => this.baseArmor;
            private set
            {
                this.baseArmor = value;
            }
        }

        // •	Armor – a floating-point number (current armor) 
        // o	Armor – the current amount of armor left – can not be less than 0.

        public double Armor
        {
            get => this.armor;
            private set
            {
                this.armor =  value;

                if (this.armor < 0)
                {
                    this.armor = 0;
                }
            }
        }

        // •	AbilityPoints – a floating-point number

        public double AbilityPoints
        {
            get => this.abilityPoints;
            private set
            {
                abilityPoints = value;
            }
        }

        // •	Bag – a parameter of type Bag

        public virtual Bag Bag
        {
            get => this.satchel;
            protected set
            {
                this.satchel = value;
            }
        }

        // •	IsAlive – boolean value (default value: True)

        public bool IsAlive
        {
            get => this.isAlive;
            internal set
            {
                this.isAlive = value;
            }
        }

        protected void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }

        //        void TakeDamage(double hitPoints)
        //For a character to take damage, they need to be alive.
        //The character takes damage equal to the hit points. Taking damage works like so:
        //The character’s armor is reduced by the hit point amount, then if there are still hit points left, they take that amount of health damage.
        //If the character’s health drops to zero, the character dies (IsAlive become false)
        //Example: Health: 100, Armor: 30, Hit Points: 40  Health: 90, Armor: 0

        public void TakeDamage(double hitPoints)
        {
            if (this.IsAlive)
            {
                if (this.Armor >= hitPoints)
                {
                    this.Armor -= hitPoints;
                }
                else
                {
                    this.Health -= (hitPoints - this.Armor);
                    this.Armor = 0;
                }

                if (this.Health <= 0 )
                {
                    this.IsAlive = false;
                }
            }
        }

        //        void UseItem(Item item)
        //For a character to use an item, they need to be alive.
        //The item affects the character with the item effect.

        public void UseItem(Item item)
        {
            if (this.IsAlive)
            {
                item.AffectCharacter(this);
            }
        }
    }
}