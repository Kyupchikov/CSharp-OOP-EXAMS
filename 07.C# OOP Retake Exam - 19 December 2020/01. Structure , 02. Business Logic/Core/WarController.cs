using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
    public class WarController
    {
        private readonly List<Character> characters;
        private readonly List<Item> items;

        public WarController()
        {
            this.characters = new List<Character>();
            this.items = new List<Item>();
        }

        public IReadOnlyCollection<Character> Characters
            => this.characters.AsReadOnly();

        public IReadOnlyCollection<Item> Items
            => this.items.AsReadOnly();

        //		Creates a character and adds them to the party.
        //If the character type is invalid, throw an ArgumentException with the message "Invalid character type "{characterType}"!"
        //Returns the string "{name} joined the party!"

        public string JoinParty(string[] args)
        {

            Character character;

            if (args[0] == "Warrior")
            {
                character = new Warrior(args[1]);
            }
            else if (args[0] == "Priest")
            {
                character = new Priest(args[1]);
            }
            else
            {
                throw new ArgumentException($"Invalid character type \"{args[0]}\"!");
            }

            this.characters.Add(character);

            return $"{args[1]} joined the party!";
        }

        //        Creates an item and adds it to the item pool.
        //If the item type is invalid, throw an ArgumentException with the message "Invalid item "{ name}"!"
        //Returns the string "{itemName} added to pool."

        public string AddItemToPool(string[] args)
        {
            Item item;

            if (args[0] == "FirePotion")
            {
                item = new FirePotion();
            }
            else if (args[0] == "HealthPotion")
            {
                item = new HealthPotion();
            }
            else
            {
                throw new ArgumentException($"Invalid item \"{ args[0]}\"!");
            }

            this.items.Add(item);

            return $"{args[0]} added to pool.";
        }

        //        Makes the character with the specified name pick up the last item in the item pool and add it to his/her bag.
        //If the character doesn’t exist in the party, throw an ArgumentException with the message "Character {name} not found!"
        //If there’s no items left in the pool, throw an InvalidOperationException with the message "No items left in pool!"
        //Returns the string "{characterName} picked up {itemName}!"

        public string PickUpItem(string[] args)
        {
            var characterWanted = this.characters.FirstOrDefault(x => x.Name == args[0]);

            if (characterWanted == null)
            {
                throw new ArgumentException($"Character {args[0]} not found!");
            }

            if (characterWanted.GetType().Name == "Warrior")
            {
                characterWanted = characterWanted as Warrior;
            }
            else if (characterWanted.GetType().Name == "Priest")
            {
                characterWanted = characterWanted as Priest;
            }

            if (items.Count == 0)
            {
                throw new InvalidOperationException($"No items left in pool!");
            }

            var itemWanted = this.items[items.Count - 1];
            characterWanted.Bag.AddItem(itemWanted);
            this.items.Remove(itemWanted);

            return $"{characterWanted.Name} picked up {itemWanted.GetType().Name}!";
        }

        //        Parameters
        //•	characterName – a string
        //•	itemName – string
        //Functionality
        //Makes the character with that name use an item with that name from their bag.
        //If the character doesn’t exist in the party, throw an ArgumentException with the message "Character {name} not found!"
        //The rest of the exceptions should be processed by the called functionality(empty bag, etc.)
        //Returns the string "{character.Name} used {itemName}."

        public string UseItem(string[] args)
        {
            Character characterWanted = this.Characters.FirstOrDefault(x => x.Name == args[0]);
            Item itemWanted = characterWanted.Bag.GetItem(args[1]);

            if (characterWanted == null)
            {
                throw new ArgumentException($"Character {args[0]} not found!");
            }

            characterWanted.UseItem(itemWanted);

            return $"{args[0]} used {args[1]}.";
        }

        //        Returns info about all characters, sorted by whether they are alive(descending), then by their health(descending)
        //The format of a single character is:
        //"{name} - HP: {health}/{baseHealth}, AP: {armor}/{baseArmor}, Status: {Alive/Dead}"
        //Returns the formatted character info for each character, separated by new lines.

        public string GetStats()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.characters.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health))
            {
                string result = "";

                if (item.IsAlive == true)
                {
                    result = "Alive";
                }
                else
                {
                    result = "Dead";
                }

                sb.AppendLine($"{item.Name} - HP: {item.Health}/{item.BaseHealth}, AP: {item.Armor}/{item.BaseArmor}, Status: {result}");
            }

            sb.AppendLine();

            return sb.ToString().TrimEnd();
        }

        //        •	attackerName – string
        //•	receiverName – string
        //Functionality
        //Makes the attacker attack the receiver.
        //If any character doesn’t exist in the party, throw an ArgumentException with the message "Character {name} not found!"
        //Check the Attacker first and then the receiver. 

        //If the attacker cannot attack, throw an ArgumentException with the message "{attacker.Name} cannot attack!"
        //The command output is in the following format:
        //"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points!
        //{receiverName} has {receiverHealth}/{receiverBaseHealth} HP and {receiverArmor}/{receiverBaseArmor} AP left!"
        //If the attacker ends up killing the receiver, add a new line, plus "{receiver.Name} is dead!" to the output.
        //Returns the formatted string

        public string Attack(string[] args)
        {
            Character attacker = this.characters.FirstOrDefault(x => x.Name == args[0]);
            Character defender = this.characters.FirstOrDefault(x => x.Name == args[1]);

            if (attacker == null)
            {
                throw new ArgumentException($"Character {args[0]} not found!");
            }
            else if (defender == null)
            {
                throw new ArgumentException($"Character {args[1]} not found!");
            }

            if (attacker.GetType().Name == "Priest")
            {
                throw new ArgumentException($"{attacker.Name} cannot attack!");
            }

            var attackerAsWarrior = attacker as Warrior;

            attackerAsWarrior.Attack(defender);

            if (!defender.IsAlive)
            {
                return $"{attackerAsWarrior.Name} attacks {defender.Name} for {attackerAsWarrior.AbilityPoints} hit points! {defender.Name} has {defender.Health}/{defender.BaseHealth} HP and {defender.Armor}/{defender.BaseArmor} AP left!{Environment.NewLine}{defender.Name} is dead!";
            }

            return $"{attackerAsWarrior.Name} attacks {defender.Name} for {attackerAsWarrior.AbilityPoints} hit points! {defender.Name} has {defender.Health}/{defender.BaseHealth} HP and {defender.Armor}/{defender.BaseArmor} AP left!";
        }

        //        •	healerName – a string
        //•	healingReceiverName – string
        //Functionality
        //Makes the healer heal the healing receiver.
        //If any character doesn’t exist in the party, throw an ArgumentException with the message "Character {name} not found!". Check the Healer first and then the receiver.
        //If the healer cannot heal, throw an ArgumentException with the message "{healerName} cannot heal!"
        //The command output is in the following format:
        //"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!"
        //Returns the formatted string

        public string Heal(string[] args)
        {
            Character healer = this.Characters.FirstOrDefault(x => x.Name == args[0]);
            Character receiver = this.Characters.FirstOrDefault(x => x.Name == args[1]);

            if (healer == null)
            {
                throw new ArgumentException($"Character {args[0]} not found!");
            }
            else if (receiver == null)
            {
                throw new ArgumentException($"Character {args[1]} not found!");
            }

            if (!healer.IsAlive || !receiver.IsAlive)
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }

            if (healer.GetType().Name != "Priest")
            {
                throw new ArgumentException($"{args[0]} cannot heal!");
            }

            var healerAsPriest = healer as Priest;

            healerAsPriest.Heal(receiver);

            return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";
        }
    }
}
