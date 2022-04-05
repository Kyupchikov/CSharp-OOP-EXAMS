namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AquariumsTests
    {
        private Aquarium aquarium;
        private Fish fish;

        [SetUp]
        public void SetUp()
        {
            aquarium = new Aquarium("Underworld", 25);
            fish = new Fish("Nemo");
        }

        [Test]
        public void CtorCheck()
        {
            Assert.IsNotNull(aquarium);
            Assert.AreEqual("Underworld", aquarium.Name);
            Assert.AreEqual(25, aquarium.Capacity);
        }

        [TestCase("")]
        [TestCase(null)]
        public void NameNullCheck(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(name, 10));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void CapacityCheck1(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Aquarium("Riverworld ", capacity));
        }

        [Test]
        public void CapacityCheck2()
        {
            Aquarium aquarium1 = new Aquarium("Riverworld ", 0);

            Assert.Throws<InvalidOperationException>(() => aquarium1.Add(fish));
        }

        [Test]
        public void CountCheck()
        {
            Assert.AreEqual(0, aquarium.Count);

            aquarium.Add(fish);

            Assert.AreEqual(1, aquarium.Count);
        }

        [TestCase("Nemo")]
        [TestCase("Pesho")]
        public void RemoveFishCheck1(string name)
        {
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(name));
        }

        [TestCase("Nemo")]
       
        public void RemoveFishCheck2(string name)
        {
            aquarium.Add(fish);

            Assert.AreEqual(1, aquarium.Count);

            aquarium.RemoveFish("Nemo");

            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void SellFishCheck1()
        {
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("Nemo"));
        }

        [Test]
        public void SellFishCheck2()
        {
            aquarium.Add(fish);
            Assert.AreEqual(fish, aquarium.SellFish("Nemo"));
        }

        [Test]
        public void ReportCheck1()
        {
            aquarium.Add(fish);

            Assert.AreEqual("Fish available at Underworld: Nemo", aquarium.Report());
        }

        [Test]
        public void ReportCheck2()
        {

            Assert.AreEqual("Fish available at Underworld: ", aquarium.Report());
        }
    }
}


