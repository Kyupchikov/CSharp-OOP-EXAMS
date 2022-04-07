using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core.Contracts
{
    public class Controller : IController
    {
        private decimal totalIncome = 0;

        // •	bakedFoods – List of foods –  foods offered by the restaurant

        public List<IBakedFood> bakedFoods;

        // •	drinks – List of drinks – the drinks the restaurant offers

        public List<IDrink> drinks;

        // •	tables – List of tables – all tables in the restaurant

        public List<ITable> tables;

        public Controller()
        {
            this.bakedFoods = new List<IBakedFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<ITable>();
        }

        // Creates a drink with the correct type. If the drink is created successful, returns:
        //"Added {drink name} ({drink brand}) to the drink menu"

        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink = null;

            if (type == DrinkType.Tea.ToString())
            {
                drink = new Tea(name, portion, brand);
            }
            else if (type == DrinkType.Water.ToString())
            {
                drink = new Water(name, portion, brand);
            }

            this.drinks.Add(drink);

            return $"Added {name} ({brand}) to the drink menu";
        }

        // Creates a food with the correct type. If the food is created successful, returns:
        //"Added {baked food name} ({baked food type}) to the menu" 

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food = null;

            if (type == BakedFoodType.Cake.ToString())
            {
                food = new Cake(name, price);
            }
            else if (type == BakedFoodType.Bread.ToString())
            {
                food = new Bread(name, price);
            }

            this.bakedFoods.Add(food);

            return $"Added {name} ({type}) to the menu";
        }

        // Creates a table with the correct type and returns:
        //"Added table number {table number} in the bakery"

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = null;

            if (type == TableType.InsideTable.ToString())
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else if (type == TableType.OutsideTable.ToString())
            {
                table = new OutsideTable(tableNumber, capacity);
            }

            this.tables.Add(table);

            return $"Added table number {tableNumber} in the bakery";
        }

        // Finds all not reserved tables and for each table returns the table info.

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var table in this.tables)
            {
                sb.AppendLine($"{table.GetFreeTableInfo()}");
            }

            return sb.ToString().TrimEnd();
        }

        // Returns the total income for the restaurant for all orders.
        //"Total income: {income:f2}lv"

        public string GetTotalIncome()
        {
            return $"Total income: {totalIncome:f2}lv";
        }

        //        Finds the table with the same table number.Gets the bill for that table and clears it.Finally returns:
        //"Table: {tableNumber}"
        //"Bill: {table bill:f2}"

        public string LeaveTable(int tableNumber)
        {
            ITable table = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            table.GetBill();
            decimal currentBill = table.GetBill();
            totalIncome += table.GetBill();

            table.Clear();

            return $"Table: {tableNumber}{Environment.NewLine}Bill: {currentBill:f2}";
        }

        //        Finds the table with that number and finds the drink with that name and brand.If there is no such table, it returns:
        //"Could not find table {tableNumber}"
        //If there isn’t such drink, it returns:
        //"There is no {drinkName} {drinkBrand} available"
        //In other case, it orders the drink for that table and returns:
        //"Table {tableNumber} ordered {drinkName} {drinkBrand}"

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            if (table == null)
            {
                return $"Could not find table {tableNumber}";
            }

            IDrink drink = this.drinks.Where(x => x.Name == drinkName && x.Brand == drinkBrand).FirstOrDefault();

            if (drink == null)
            {
                return $"There is no {drinkName} {drinkBrand} available";
            }

            table.OrderDrink(drink);

            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        //        Finds the table with that number and the food with that name in the menu.If there is no such table returns:
        //"Could not find table {tableNumber}"
        //If there is no such food returns:
        //"No {bakedFoodName} in the menu"
        //In other case orders the food for that table and returns:
        //"Table {tableNumber} ordered {bakedFoodName}"

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable table = this.tables.FirstOrDefault(x => x.TableNumber == tableNumber);

            if (table == null)
            {
                return $"Could not find table {tableNumber}";
            }

            IBakedFood bakedFood = this.bakedFoods.FirstOrDefault(x => x.Name == foodName);

            if (bakedFood == null)
            {
                return $"No {foodName} in the menu";
            }

            table.OrderFood(bakedFood);

            return $"Table {tableNumber} ordered {foodName}";
        }

        //        Finds a table which is not reserved, and its capacity is enough for the number of people provided.If there is no such table returns:
        //"No available table for {numberOfPeople} people"
        //In the other case reserves the table and returns:
        //"Table {table number} has been reserved for {numberOfPeople} people"

        public string ReserveTable(int numberOfPeople)
        {
            ITable table = this.tables.Where(x => x.IsReserved == false && x.Capacity >= numberOfPeople).FirstOrDefault();

            if (table == null)
            {
                return $"No available table for {numberOfPeople} people";
            }

            table.Reserve(numberOfPeople);

            return $"Table {table.TableNumber} has been reserved for {numberOfPeople} people";
        }
    }
}
