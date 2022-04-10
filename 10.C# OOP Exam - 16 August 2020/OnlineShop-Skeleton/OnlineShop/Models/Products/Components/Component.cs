using OnlineShop.Models.Products.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models
{
    public abstract class Component : Product, IComponent
    {
        private int generation;

        //                  int id, string manufacturer, string model, decimal price, double overallPerformance, int generation
        protected Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.Generation = generation;
        }

        public int Generation
        {
            get => this.generation;
            private set
            {
                this.generation = value;
            }
        }

        // "Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id})"
        // "Overall Performance: {overall performance}. Price: {price} - {product type}: {manufacturer} {model} (Id: {id}) Generation: {generation}"

        public override string ToString()
        {
            return $"{base.ToString()} Generation: {this.generation}";
        }
    }
}
