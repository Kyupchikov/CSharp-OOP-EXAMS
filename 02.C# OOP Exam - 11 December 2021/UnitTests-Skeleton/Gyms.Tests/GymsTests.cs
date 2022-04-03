namespace Gyms.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    
    public class GymsTests
    {
        private Gym gym;
        private Athlete athlete;

        [SetUp]
        public void SetUp()
        {
            gym = new Gym("Fantastic", 60);
            athlete = new Athlete("Tosho Goshov");
        }

        [Test]
        public void ConstructorCheck()
        {
            Assert.AreNotEqual(null, gym);
            Assert.AreEqual("Fantastic", gym.Name);
            Assert.AreEqual(60, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
            Assert.That(gym.GetType().GetProperties().Count() == 3);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameNullValueCheck(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Gym(name, 60));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-50)]
        public void CapacityCheck1(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Gym("Fantastic", capacity));
        }

        [Test]
        public void CapacityCheck2()
        {
            gym.AddAthlete(athlete);
            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void CapacityCheck3()
        {
            Gym gym1 = new Gym("Fantastic", 1);
            Athlete athlete1 = new Athlete("Shisho Bakshishov");
            gym1.AddAthlete(athlete);
            Assert.Throws<InvalidOperationException>(() => gym1.AddAthlete(athlete1));
        }

        [Test]
        public void RemoveCheck1()
        {
            gym.AddAthlete(athlete);

            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete("Shisho Bakshishov"));
        }

        [Test]
        public void RemoveCheck2()
        {
            gym.AddAthlete(athlete);
            gym.RemoveAthlete("Tosho Goshov");

            Assert.AreEqual(0, gym.Count);
        }

        [Test]
        public void InjureAthleteCheck1()
        {
            Assert.IsFalse(athlete.IsInjured);

            gym.AddAthlete(athlete);
            gym.InjureAthlete("Tosho Goshov");

            Assert.IsTrue(athlete.IsInjured);
            Assert.AreEqual(athlete, gym.InjureAthlete("Tosho Goshov"));
        }

        [Test]
        public void InjureAthleteCheck2()
        {
            Assert.IsFalse(athlete.IsInjured);

            gym.AddAthlete(athlete);

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete("Shisho Bakshishov"));
        }

        [Test]
        public void ReportCheck1()
        {
            gym.AddAthlete(athlete);

            Assert.AreEqual($"Active athletes at {gym.Name}: {athlete.FullName}", gym.Report());
        }

        [Test]
        public void ReportCheck2()
        {
            gym.AddAthlete(athlete);
            gym.InjureAthlete("Tosho Goshov");

            Assert.AreEqual($"Active athletes at {gym.Name}: ", gym.Report());
        }

        [Test]
        public void ReportCheck3()
        {
            Athlete athlete1 = new Athlete("Shisho Bakshishov");
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete);
            gym.InjureAthlete("Tosho Goshov");

            Assert.AreEqual($"Active athletes at {gym.Name}: Shisho Bakshishov", gym.Report());
        }
    }
}
