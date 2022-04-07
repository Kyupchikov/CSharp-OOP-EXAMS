using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private BankVault BankVault;

        [SetUp]
        public void Setup()
        {
            this.BankVault = new BankVault();
        }

        [Test]
        public void Check()
        {
            Item item = new Item("aa", "bb");
            Assert.AreEqual(12, this.BankVault.VaultCells.Count);
            Assert.Throws<ArgumentException>(() => this.BankVault.AddItem("A11", new Item("aa", "bb")));
            this.BankVault.AddItem("A1", item);
            Assert.AreEqual("Remove item:bb successfully!", this.BankVault.RemoveItem("A1", item));
            Assert.AreEqual("Item:bba saved successfully!", this.BankVault.AddItem("A4", new Item("aab", "bba")));
            this.BankVault.AddItem("A1", item);
            Assert.Throws<ArgumentException>(() => this.BankVault.AddItem("A1", new Item("aaa", "bbb")));
            Assert.Throws<InvalidOperationException>(() => this.BankVault.AddItem("A2", new Item("aa", "bb")));
            Assert.Throws<ArgumentException>(() => this.BankVault.RemoveItem("A1", new Item("aaa", "bbb")));
            Assert.Throws<ArgumentException>(() => this.BankVault.RemoveItem("A11", new Item("aaabbb", "bbb")));
        }
    }
}