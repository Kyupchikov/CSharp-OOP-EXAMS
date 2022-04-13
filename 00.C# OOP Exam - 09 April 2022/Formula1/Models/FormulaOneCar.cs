using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models.Contracts
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsepower;
        private double engineDisplacement;

        //                      string model, int horsepower, double engineDisplacement
        protected FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;
        }

        //o If the model is null, white space, or the length is less than 3 symbols, throw an ArgumentException with a message: "Invalid car model: { model }."

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException($"Invalid car model: {value}.");
                }

                this.model = value;
            }
        }

        // o	If the horsepower is less than 900, or more than 1050, throw an ArgumentException with a message: "Invalid car horsepower: { horsepower }."

        public int Horsepower
        {
            get => horsepower;
            private set
            {
                if (value < 900 || value > 1050)
                {
                    throw new ArgumentException($"Invalid car horsepower: {value}.");
                }

                this.horsepower = value;
            }
        }

        // o	If the engine displacement  is less than 1.6, or more than 2.00,
        // throw an ArgumentException with a message: "Invalid car engine displacement: { engine displacement }."

        public double EngineDisplacement
        {
            get => engineDisplacement;
            private set
            {
                if (value < 1.6 || value > 2.0)
                {
                    throw new ArgumentException($"Invalid car engine displacement: {value}.");
                }

                engineDisplacement = value;
            }
        }

        // The RaceScoreCalculator calculates the race points in the concrete race with this formula:
        //engine displacement / horsepower* laps

        public double RaceScoreCalculator(int laps)
        {
            return this.EngineDisplacement / this.Horsepower * laps;
        }
    }
}
