using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Computers.Tests
{
    public class Tests
    {
        private ComputerManager computerManager;
        private Computer computer;

        [SetUp]
        public void Setup()
        {
             computerManager = new ComputerManager();computer = new Computer("Xiaomi", "Mi Pro X", 2399);
        }

        [Test]
        public void CtporCheck()
        {
            Assert.IsNotNull(this.computerManager.Computers);
        }

        [Test]
        public void CountCheck()
        {
            Assert.AreEqual(0, this.computerManager.Count);

            this.computerManager.AddComputer(computer);

            Assert.AreEqual(1, this.computerManager.Count);
        }

        [Test]
        public void AddComputerCheck2()
        {
            Assert.AreEqual(0, this.computerManager.Count);

            this.computerManager.AddComputer(computer);

            Assert.AreEqual(1, this.computerManager.Count);
        }

        [Test]
        public void AddComputerCheck1()
        {
            this.computerManager.AddComputer(computer);

            Assert.Throws<ArgumentException>(() => this.computerManager.AddComputer(computer));
        }

        [Test]
        public void GetComputerCheck1()
        {
            this.computerManager.AddComputer(computer);

            Assert.AreEqual(computer, this.computerManager.GetComputer("Xiaomi", "Mi Pro X"));
        }

        [Test]
        public void GetComputerCheck2()
        {
            Assert.Throws<ArgumentException>(() => this.computerManager.GetComputer("Xiaomi", "Mi Pro X"));
        }

        [Test]
        public void GetComputerCheck3()
        {
            this.computerManager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() => this.computerManager.GetComputer(null, ""));
            Assert.Throws<ArgumentNullException>(() => this.computerManager.RemoveComputer(null, ""));
            Assert.Throws<ArgumentNullException>(() => this.computerManager.GetComputer("",null));
            Assert.Throws<ArgumentNullException>(() => this.computerManager.RemoveComputer("",null));
            Assert.Throws<ArgumentNullException>(() => this.computerManager.GetComputer(null,null));
            Assert.Throws<ArgumentNullException>(() => this.computerManager.RemoveComputer(null,null));
        }

        [Test]
        public void RemoveComputerCheck1()
        {
            this.computerManager.AddComputer(computer);

            Assert.AreEqual(computer, this.computerManager.RemoveComputer("Xiaomi", "Mi Pro X"));
        }

        [Test]
        public void RemoveComputerCheck2()
        {
            this.computerManager.AddComputer(computer);
            Assert.AreEqual(1, this.computerManager.Computers.Count);
            this.computerManager.RemoveComputer("Xiaomi", "Mi Pro X");
            Assert.AreEqual(0,this.computerManager.Computers.Count);
        }

        [Test]
        public void GetComputersByManufacturerCheck()
        {
            Computer computer1 = new Computer("LENOVO", "ThinkPad T590", 15.29m);
            Computer computer2 = new Computer("APPLE","MacBook Air 13",27.34m);
            Computer computer3 = new Computer("ASUS", "M509DA", 16.28m); ;
            Computer computer4 = new Computer("ASUS", "ZenBook 13 UX333FAC",18.97m);

            this.computerManager.AddComputer(computer);
            this.computerManager.AddComputer(computer1);
            this.computerManager.AddComputer(computer2);
            this.computerManager.AddComputer(computer3);
            this.computerManager.AddComputer(computer4);

            var myCheck = new List<Computer>();
            myCheck.Add(computer3);
            myCheck.Add(computer4);
            var check = this.computerManager.GetComputersByManufacturer("ASUS");

            CollectionAssert.AreEqual(check, this.computerManager.GetComputersByManufacturer("ASUS"));
            CollectionAssert.AreEqual(myCheck, this.computerManager.GetComputersByManufacturer("ASUS"));
            Assert.AreEqual(2, check.Count);
        }

        [Test]
        public void ValidateNullValueCheck1()
        {
            Computer computer3 = null;

            Assert.Throws<ArgumentNullException>(() => this.computerManager.AddComputer(computer3));
        }

        [Test]
        public void ValidateNullValueCheck2()
        {
            Computer computer1 = new Computer("LENOVO", "ThinkPad T590", 15.29m);
            Computer computer2 = new Computer("APPLE", "MacBook Air 13", 27.34m);
            Computer computer3 = new Computer("ASUS", "M509DA", 16.28m); ;
            Computer computer4 = new Computer("ASUS", "ZenBook 13 UX333FAC", 18.97m);

            this.computerManager.AddComputer(computer);
            this.computerManager.AddComputer(computer1);
            this.computerManager.AddComputer(computer2);
            this.computerManager.AddComputer(computer3);
            this.computerManager.AddComputer(computer4);

            var myCheck = new List<Computer>();
            myCheck.Add(computer3);
            myCheck.Add(computer4);
            var check = this.computerManager.GetComputersByManufacturer("ASUS");
            
            Assert.Throws<ArgumentNullException>(() => this.computerManager.GetComputersByManufacturer(null));
        }
    }
}