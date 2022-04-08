using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;
        private UnitCar unitCar;
        private UnitDriver unitDriver;

        [SetUp]
        public void Setup()
        {
            raceEntry = new RaceEntry();
            unitCar = new UnitCar("CR-V", 188, 2.4);
            unitDriver = new UnitDriver("Tosho", unitCar);
        }

        [Test]
        public void ConsryctorCheck()
        {
            Assert.IsNotNull(raceEntry);
        }

        [Test]
        public void CounterCheck()
        {
            Assert.AreEqual(0, raceEntry.Counter);
            raceEntry.AddDriver(unitDriver);

            Assert.AreEqual(1, raceEntry.Counter);
        }

        [Test]
        public void AddingNonExistingDriver()
        {
            raceEntry.AddDriver(unitDriver);
            UnitDriver unitDriver1 = null;

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver1));
        }

        [Test]
        public void AddingExistingDriver()
        {
            raceEntry.AddDriver(unitDriver);

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(unitDriver));
        }

        [Test]
        public void CalculateAverageHorsePowerExceptionCheck()
        {
            raceEntry.AddDriver(unitDriver);
          
            Assert.Throws< InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePowerExceptionCheck2()
        {
            UnitCar unitCar1 = new UnitCar("Panda", 62, 1.2);
            UnitDriver unitDriver1 = new UnitDriver("Pesho", unitCar1);
            raceEntry.AddDriver(unitDriver);
            raceEntry.AddDriver(unitDriver1);

            Assert.AreEqual(125, raceEntry.CalculateAverageHorsePower());
        }
    }
}