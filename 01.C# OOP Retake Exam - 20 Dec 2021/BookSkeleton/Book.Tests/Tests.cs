namespace Book.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class Tests
    {
        private Book book;
        private Dictionary<int, string> note;

        [SetUp]
        public void SetUp()
        {
            book = new Book("I Love Bees", "Dimitar Kyupchikov");
            note = new Dictionary<int, string>();
        }

        [Test]
        public void ConsructorCheck()
        {
            //  Book book1 = new Book("I Love Bees", "Dimitar Kyupchikov");
            Assert.AreEqual("I Love Bees", book.BookName);
            Assert.AreEqual("Dimitar Kyupchikov", book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }

        [TestCase("")]
        [TestCase(null)]
        public void nameCheck(string name)
        {
            Assert.Throws<ArgumentException>(() => new Book(name, "Dimitar Kyupchikov"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void AuthorCheck(string name)
        {
            Assert.Throws<ArgumentException>(() => new Book("I Love Bees", name));
        }

        [Test]
        public void AddFootnoteCheck1()
        {
            book.AddFootnote(1, ".");
            book.AddFootnote(2, "..");
            book.AddFootnote(3, "...");

            Assert.AreEqual(3, book.FootnoteCount);

            Assert.Throws<InvalidOperationException>(() => book.AddFootnote(2, ".."));
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(0)]
        [TestCase(-1)]
        public void FindFootnoteCheck1(int number)
        {
            book.AddFootnote(1, ".");
            book.AddFootnote(2, "..");
            book.AddFootnote(3, "...");

            Assert.Throws<InvalidOperationException>(() => book.FindFootnote(number));
        }

        [Test]
        public void FindFootnoteCheck2()
        {
            book.AddFootnote(1, ".");
            book.AddFootnote(2, "..");
            book.AddFootnote(3, "...");

            Assert.AreEqual($"Footnote #2: ..", book.FindFootnote(2));
        }

        [Test]
        public void AlterFootnoteCheck1()
        {
            book.AddFootnote(1, ".");
            book.AddFootnote(2, "..");
            book.AddFootnote(3, "...");

            book.AlterFootnote(2, "aa");

            Assert.AreEqual($"Footnote #2: aa", book.FindFootnote(2));
        }

        [Test]
        public void AlterFootnoteCheck2()
        {
            book.AddFootnote(1, ".");
            book.AddFootnote(2, "..");
            book.AddFootnote(3, "...");

            Assert.Throws<InvalidOperationException>(() => book.AlterFootnote(4, "aa"));
        }
    }
}