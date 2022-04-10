using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models
{
    public abstract class Computer : Product, IComputer
    {
        private readonly List<IComponent> components;
        private readonly List<IPeripheral> peripherals;

        //              int id, string manufacturer, string model, decimal price, double overallPerformance
        public Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        // •	Components – IReadOnlyCollection

        public IReadOnlyCollection<IComponent> Components
            => this.components.AsReadOnly();

        // •	Peripherals – IReadOnlyCollection

        public IReadOnlyCollection<IPeripheral> Peripherals
                => this.peripherals.AsReadOnly();

        //  •	OverallPerformance – override the base functionality(if the components collection is empty,
        //  it should return only the computer overall performance, otherwise return the sum of the computer
        //  overall performance and the average overall performance from all components)

        public override double OverallPerformance
        {
            get
            {
                if (!this.components.Any())
                {
                    return base.OverallPerformance;
                }

                return base.OverallPerformance + this.Components.Average(x => x.OverallPerformance);
            }
        }

        // •	Price – override the base functionality (The price is equal to the total sum of the computer price with the sum
        //      of all component prices and the sum of all peripheral prices)

        public override decimal Price
        {

            get
            {

                return this.Components.Sum(x => x.Price) + this.Peripherals.Sum(x => x.Price) + base.Price;

            }
        }

        //        If the components collection contains a component with the same component type, throw an ArgumentException with the message
        //        "Component {component type} already exists in {computer type} with Id {id}."
        //        Otherwise add the component in the components collection.

        public void AddComponent(IComponent component)
        {
            if (this.Components.Any(x => x.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException($"Component {component.GetType().Name} already exists in {this.GetType().Name} with Id {this.Id}.");
            }

            this.components.Add(component);
        }

        //If the components collection is empty or does not have a component of that type, throw an ArgumentException with the message
        //"Component {component type} does not exist in {computer type} with Id {id}."
        //Otherwise remove the component of that type and return it.

        public IComponent RemoveComponent(string componentType)
        {
            var componentToRemove = this.Components.FirstOrDefault(x => x.GetType().Name == componentType);

            if (componentToRemove == null)
            {
                throw new ArgumentException($"Component {componentType} does not exist in {this.GetType().Name} with Id {this.Id}.");
            }

            this.components.Remove(componentToRemove);

            return componentToRemove;
        }

        //        If the peripherals collection contains a peripheral with the same peripheral type,
        //        throw an ArgumentException with the message "Peripheral {peripheral type} already exists in {computer type} with Id {id}."
        //        Otherwise add the peripheral in peripherals collection

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.Peripherals.Any(x => x.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException($"Peripheral {peripheral.GetType().Name} already exists in {this.GetType().Name} with Id {this.Id}.");
            }

            this.peripherals.Add(peripheral);
        }

        //If the peripherals collection is empty or does not have a peripheral of that type, throw an ArgumentException with the message
        //"Peripheral {peripheral type} does not exist in {computer type} with Id {id}."
        //Otherwise remove the peripheral of that type and return it.

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var peripheralToRemove = this.Peripherals.FirstOrDefault(x => x.GetType().Name == peripheralType);

            if (peripheralToRemove == null)
            {
                throw new ArgumentException($"Peripheral {peripheralType} does not exist in {this.GetType().Name} with Id {this.Id}.");
            }

            this.peripherals.Remove(peripheralToRemove);

            return peripheralToRemove;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{base.ToString()}");
            sb.AppendLine($" Components ({this.Components.Count}):");

            foreach (var component in this.Components)
            {
                sb.AppendLine($"  {component}");
            }

            var average = this.Peripherals.Any() ? this.Peripherals.Average(x => x.OverallPerformance)   :0;
            sb.AppendLine($" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({average:f2}):");

            foreach (var item in this.Peripherals)
            {
                sb.AppendLine($"  {item}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
//"Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id})"
//"Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id})"
//" Components ({components count}):"
//"  {component one}"
//"  {component two}"
//"  {component n}"
//" Peripherals ({peripherals count}); Average Overall Performance ({average overall performance peripherals}):"
//"  {peripheral one}"
//"  {peripheral two}"
//"  {peripheral n}"
