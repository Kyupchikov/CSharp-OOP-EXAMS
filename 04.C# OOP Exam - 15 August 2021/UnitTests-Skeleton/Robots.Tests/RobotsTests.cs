namespace Robots.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class RobotsTests
    {
        [SetUp]
        public void InitializeCollection()
        {

        }

        [Test]
        public void CheckConstrucorAndCapacity()
        {
            RobotManager robotManager = new RobotManager(8);

            Assert.AreEqual(8, robotManager.Capacity);
            Assert.AreEqual(0, robotManager.Count);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void TestCapacityValue(int count)
        {
            Assert.Throws<ArgumentException>(() => new RobotManager(count));
        }

        [Test]
        public void AddingExistingRobot()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot5));
        }

        [Test]
        public void CheckingCapacityUpperBorder()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(5);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot6));
        }

        [Test]
        public void RemoveNonExistingRobot()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            Assert.Throws<InvalidOperationException>(() => robotManager.Remove("Kiro"));
        }

        [Test]
        public void RemoveExistingRobot()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            robotManager.Remove("Smesho");

            Assert.AreEqual(5, robotManager.Count);
        }

        [Test]
        public void NonExistingRobotOnWorkShouldThrowAnException()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Kiro", "....", 50));
        }


        [Test]
        public void RobotWithLowerBatteryThanBatteryUsageShouldThrowException()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            Assert.Throws<InvalidOperationException>(() => robotManager.Work("Smesho", "....", 150));
        }

        [Test]
        public void IfRobotWorkTheBatteryShouldDying()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            robotManager.Work("Smesho", "....", 60);

            Assert.AreEqual(40, robot4.Battery);
        }

        [Test]
        public void ChargingNonExistingRobotThrowsAnException()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            Assert.Throws<InvalidOperationException>(() => robotManager.Charge("Kiro"));
        }

        [Test]
        public void ChargingCheck()
        {
            Robot robot1 = new Robot("Gosho", 100);
            Robot robot2 = new Robot("Tosho", 100);
            Robot robot3 = new Robot("Pesho", 100);
            Robot robot4 = new Robot("Smesho", 100);
            Robot robot5 = new Robot("Joko", 100);
            Robot robot6 = new Robot("Roko", 100);

            RobotManager robotManager = new RobotManager(8);

            robotManager.Add(robot1);
            robotManager.Add(robot2);
            robotManager.Add(robot3);
            robotManager.Add(robot4);
            robotManager.Add(robot5);
            robotManager.Add(robot6);

            robotManager.Work("Smesho", "....", 60);

            Assert.AreEqual(40, robot4.Battery);

            robotManager.Charge("Smesho");

            Assert.AreEqual(100, robot4.Battery);
        }
    }
}
