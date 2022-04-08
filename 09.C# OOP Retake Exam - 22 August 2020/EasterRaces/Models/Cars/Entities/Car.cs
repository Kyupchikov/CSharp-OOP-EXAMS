using EasterRaces.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private double cubicCentimeters;
        private int minHorsePower;
        private int maxHorsePower;

        //            string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower

        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.MinHorsePower = minHorsePower;
            this.MaxHorsePower = maxHorsePower;
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
        }

        // o	If the model is null, whitespace or less than 4 symbols,
        // throw an ArgumentException with message "Model {model} cannot be less than 4 symbols."

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException($"Model {value} cannot be less than 4 symbols.");
                }

                model = value;
            }
        }

        // o	Every type of car has different range of valid horsepower. If the horsepower is not in the valid range,
        // throw an ArgumentException with message "Invalid horse power: {horsepower}."

        public int HorsePower
        {
            get => this.horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException($"Invalid horse power: {value}.");
                }

                horsePower = value;
            }
        }

        public double CubicCentimeters
        {
            get => this.cubicCentimeters;
            private set
            {
                this.cubicCentimeters = value;
            }
        }

        public int MinHorsePower
        {
            get => this.minHorsePower;
            private set
            {
                minHorsePower = value;
            }
        }

        public int MaxHorsePower
        {
            get => this.maxHorsePower;
            private set
            {
                maxHorsePower = value;
            }
        }

        // cubic centimeters / horsepower * laps

        public double CalculateRacePoints(int laps)
        {
            return this.cubicCentimeters / this.horsePower * laps;
        }
    }
}
