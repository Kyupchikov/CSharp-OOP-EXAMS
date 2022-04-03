namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;
        private Present present;
        private Present present1;
        private List<Present> presents;

        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
            present = new Present("Ball",33.3);
        }

        [Test]
        public void CtorCheck()
        {
            Assert.IsNotNull(bag.GetPresents());
        }

        [Test]
        public void CreateCheck1()
        {
            Assert.Throws<ArgumentNullException>(() => bag.Create(present1));
        }

        [Test]
        public void CreateCheck2()
        {
            bag.Create(present);
            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }

        [Test]
        public void RemoveCheck()
        {
            bag.Create(present);

            Assert.IsTrue(bag.Remove(present));
            Assert.IsFalse(bag.Remove(present));
        }

        [Test]
        public void GetPresentWithLeastMagicCheck()
        {
            bag.Create(present);
            Present warrior = new Present("Warrior", 22.2);
            bag.Create(warrior);
            Present pony = new Present("Pony", 11.1);
            bag.Create(pony);

            Assert.AreEqual(pony, bag.GetPresentWithLeastMagic());
        }

        [Test]
        public void GetPresentCheck()
        {
            bag.Create(present);
            Present warrior = new Present("Warrior", 22.2);
            bag.Create(warrior);
            Present pony = new Present("Pony", 11.1);
            bag.Create(pony);

            Assert.AreEqual(present, bag.GetPresent("Ball"));
            Assert.IsNull(bag.GetPresent("Horse"));
        }
    }
}
