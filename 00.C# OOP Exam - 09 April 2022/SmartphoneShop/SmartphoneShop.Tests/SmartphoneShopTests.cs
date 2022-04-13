using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        
        [Test]
        public void phonechek()
        {
            Assert.Throws<ArgumentException>(() => new Shop(-1));
            Shop shop1 = new Shop(5);
            Assert.AreEqual(5, shop1.Capacity);

            Smartphone smartphone1 = new Smartphone("nokia", 100);
            Smartphone smartphone2 = new Smartphone("nokia", 110);
            Smartphone smartphone3 = new Smartphone("nokiaa", 120);
            Smartphone smartphone4 = new Smartphone("nokiaaa", 120);

            Shop shop = new Shop(2);

            shop.Add(smartphone4);
            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone4));
            shop.Add(smartphone1);
            Assert.AreEqual(2, shop.Count);
            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone2));
            Assert.Throws<InvalidOperationException>(() => shop.Add(smartphone3));
            Assert.Throws<InvalidOperationException>(() => shop.Remove("abc"));
            shop.Remove("nokia");
            Assert.AreEqual(1, shop.Count);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("a", 100));
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("a"));
            shop.ChargePhone("nokiaaa");
            Assert.AreEqual(120,smartphone4.MaximumBatteryCharge);
            shop.TestPhone("nokiaaa", 30);
            Assert.AreEqual(90, smartphone4.CurrentBateryCharge);
            var result = smartphone4.CurrentBateryCharge;
            Assert.AreEqual(90, result);
        }

        [Test]
        public void CtorCheck123()
        {
            Smartphone smartphone1 = new Smartphone("nokia", 100);
            Smartphone smartphone2 = new Smartphone("nokia", 110);
            Smartphone smartphone3 = new Smartphone("nokiaa", 120);
            Smartphone smartphone4 = new Smartphone("nokiaaa", 120);

            Shop shop = new Shop(2);

            shop.Add(smartphone1);
            shop.Add(smartphone4);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("nokiaaa", 140));

            shop.TestPhone("nokiaaa", 20);
            Assert.AreEqual(100, smartphone4.CurrentBateryCharge);
            shop.ChargePhone("nokiaaa");
            Assert.AreEqual(120, smartphone4.CurrentBateryCharge);
        }
    }
}