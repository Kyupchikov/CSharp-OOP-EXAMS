using System;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{

    HeroRepository heroRepository;

    [SetUp]
    public void SetUp()
    {
        this.heroRepository = new HeroRepository();
    }

    [Test]
    public void CtorCheck()
    {
        Assert.IsNotNull(this.heroRepository);
        Assert.AreEqual(0, this.heroRepository.Heroes.Count);
    }

    [Test]
    public void HeroCreateCheck1()
    {
        Hero hero = null;
        Assert.Throws<ArgumentNullException>(() => this.heroRepository.Create(hero));
    }

    [Test]
    public void HeroCreateCheck2()
    {
        Hero hero = new Hero("Pesho", 10);
        this.heroRepository.Create(hero);

        Assert.Throws<InvalidOperationException>(() => this.heroRepository.Create(hero));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void HeroRemoveCheck1(string name)
    {
        Hero hero = new Hero("Pesho", 10);
        this.heroRepository.Create(hero);

        Assert.AreEqual(1, this.heroRepository.Heroes.Count);
        Assert.Throws<ArgumentNullException>(() => this.heroRepository.Remove(name));
    }

    [Test]
    public void HeroRemoveCheck2()
    {
        Hero hero = new Hero("Pesho", 10);
        this.heroRepository.Create(hero);

        Assert.AreEqual(1, this.heroRepository.Heroes.Count);
        Assert.IsTrue(this.heroRepository.Remove("Pesho"));
        Assert.IsFalse(this.heroRepository.Remove("Gosho"));
    }

    [Test]
    public void GetHeroWithHighestLevelCheck()
    {
        Hero hero1 = new Hero("Valio", 23);
        Hero hero2 = new Hero("Gosho", 9);
        Hero hero3 = new Hero("Pesho", 10);
        this.heroRepository.Create(hero1);
        this.heroRepository.Create(hero2);
        this.heroRepository.Create(hero3);

        Assert.AreEqual(hero1, heroRepository.GetHeroWithHighestLevel());
    }

    [Test]
    public void GetHeroCheck()
    {

        Hero hero1 = new Hero("Valio", 23);
        Hero hero2 = new Hero("Gosho", 9);
        Hero hero3 = new Hero("Pesho", 10);
        this.heroRepository.Create(hero1);
        this.heroRepository.Create(hero2);
        this.heroRepository.Create(hero3);

        Assert.AreEqual(hero1, heroRepository.GetHero("Valio")); ;
        Assert.AreEqual(hero3, heroRepository.GetHero("Pesho")); ;
    }
}