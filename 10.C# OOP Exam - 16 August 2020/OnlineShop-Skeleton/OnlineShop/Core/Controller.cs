using OnlineShop.Common.Enums;
using OnlineShop.Models;
using OnlineShop.Models.Products;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly List<IComputer> computers;
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IPeripheral> Peripherals
            => this.peripherals.AsReadOnly();

        public IReadOnlyCollection<IComponent> Components
            => this.components.AsReadOnly();

        public IReadOnlyCollection<IComputer> Computers
            => this.computers.AsReadOnly();

        //  NOTE: For each command, except for "AddComputer" and "BuyBest", you must check if a computer, with that id, exists in the computers collection.
        //  If it doesn't, throw an ArgumentException with the message "Computer with this id does not exist.".
        //        Creates a component with the correct type and adds it to the computer with that id, then adds it to the collection of components in the controller.
        //  If a component, with the same id, already exists in the components collection, throws an ArgumentException with the message
        //  "Component with this id already exists."
        //If the component type is invalid, throws an ArgumentException with the message "Component type is invalid."
        //If it's successful, returns "Component {component type} with id {component id} added successfully in computer with id {computer id}.".

        public string AddComponent(int computerId, int id, string componentType, string manufacturer
                                    , string model, decimal price, double overallPerformance, int generation)
        {
            IComponent component;
            var computer = this.computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            if (componentType == "VideoCard")
            {
                component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "SolidStateDrive")
            {
                component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "RandomAccessMemory")
            {
                component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "PowerSupply")
            {
                component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "Motherboard")
            {
                component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == "CentralProcessingUnit")
            {
                component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
            }
            else
            {
                throw new ArgumentException("Component type is invalid.");
            }

            if (this.components.Any(x => x.Id == id))
            {
                throw new ArgumentException("Component with this id already exists.");
            }

            computer.AddComponent(component);
            this.components.Add(component);

            return $"Component {componentType} with id {component.Id} added successfully in computer with id {computer.Id}.";
        }

        //        Creates a computer with the correct type and adds it to the collection of computers.
        //If a computer, with the same id, already exists in the computers collection,
        //throw an ArgumentException with the message "Computer with this id already exists."
        //If the computer type is invalid, throw an ArgumentException with the message "Computer type is invalid."
        //If it's successful, returns "Computer with id {id} added successfully.".

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            IComputer computer;

            if (computerType == "Laptop")
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else if (computerType == "DesktopComputer")
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else
            {
                throw new ArgumentException("Computer type is invalid.");
            }

            if (this.computers.Any(x => x.Id == id))
            {
                throw new ArgumentException("Computer with this id already exists.");
            }

            this.computers.Add(computer);

            return $"Computer with id {id} added successfully.";
        }

        //        Creates a peripheral, with the correct type, and adds it to the computer with that id, then adds it to the collection of peripherals in the controller.
        //If a peripheral, with the same id, already exists in the peripherals collection, it throws an ArgumentException with the message
        //"Peripheral with this id already exists."
        //If the peripheral type is invalid, throws an ArgumentException with the message "Peripheral type is invalid."
        //If it's successful, it returns "Peripheral {peripheral type} with id {peripheral id} added successfully in computer with id {computer id}.".

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer,
            string model, decimal price, double overallPerformance, string connectionType)
        {
            IPeripheral peripheralToAdd = null;
            var computer = this.computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            if (this.Peripherals.Any(x => x.Id == id))
            {
                throw new ArgumentException("Peripheral with this id already exists.");
            }

            if (peripheralType == PeripheralType.Headset.ToString())
            {
                peripheralToAdd = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == PeripheralType.Keyboard.ToString())
            {
                peripheralToAdd = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == PeripheralType.Monitor.ToString())
            {
                peripheralToAdd = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == PeripheralType.Mouse.ToString())
            {
                peripheralToAdd = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else
            {
                throw new ArgumentException("Peripheral type is invalid.");
            }

            computer.AddPeripheral(peripheralToAdd);
            this.peripherals.Add(peripheralToAdd);

            return $"Peripheral {peripheralType} with id {id} added successfully in computer with id {computerId}.";
        }

        //        Removes the computer with the highest overall performance and with a price, less or equal to the budget, from the collection of computers.
        //If there are not any computers in the collection or the budget is insufficient for any computer, throws an ArgumentException with the message
        //"Can't buy a computer with a budget of ${budget}."
        //If it's successful, it returns ToString method on the removed computer.

        public string BuyBest(decimal budget)
        {
            IComputer bestComputer = this.computers.OrderByDescending(x => x.OverallPerformance).FirstOrDefault(x => x.Price <= budget);

            if (bestComputer == null)
            {
                throw new ArgumentException($"Can't buy a computer with a budget of ${budget}.");
            }

            this.computers.Remove(bestComputer);

            return bestComputer.ToString();
        }

        //        Removes a computer, with the given id, from the collection of computers.
        //If it's successful, it returns ToString method on the removed computer.

        public string BuyComputer(int id)
        {
            var computer = this.computers.FirstOrDefault(x => x.Id == id);

            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            this.computers.Remove(computer);

            return computer.ToString();
        }

        // If it's successful, it returns ToString method on the computer with the given id.

        public string GetComputerData(int id)
        {
            var computer = this.computers.FirstOrDefault(x => x.Id == id);

            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            return computer.ToString();
        }

        // NOTE: For each command, except for "AddComputer" and "BuyBest", you must check if a computer, with that id, exists in the computers collection.
        // If it doesn't, throw an ArgumentException with the message "Computer with this id does not exist.".
        //       Removes a component, with the given type from the computer with that id, then removes component from the collection of components.
        //If it's successful, it returns "Successfully removed {component type} with id {component id}.".

        public string RemoveComponent(string componentType, int computerId)
        {
            IComponent component = null;
            var computer = this.computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            if (computer.Components.Any(x => x.GetType().Name == componentType))
            {
                component = this.Components.First(x => x.GetType().Name == componentType);
                computer.RemoveComponent(componentType);
                return $"Successfully removed {componentType} with id {component.Id}.";
            }

            return $"Component {componentType} does not exist in Laptop with Id {computer.Id}.";
        }
        

        //        Removes a peripheral, with the given type from the computer with that id, then removes the peripheral from the collection of peripherals.
        //If it's successful, it returns "Successfully removed {peripheral type} with id { peripheral id}.".

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IPeripheral component = null;
            var computer = this.computers.FirstOrDefault(x => x.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            if (computer.Peripherals.Any(x => x.GetType().Name == peripheralType))
            {
                component = this.Peripherals.First(x => x.GetType().Name == peripheralType);
                computer.RemovePeripheral(peripheralType);
                return $"Successfully removed {peripheralType} with id {component.Id}.";
            }

            return $"Peripheral {peripheralType} does not exist in Laptop with Id {computer.Id}.";
        }
    }
}

