// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
	public class StageTests
    {
		private Stage stage;
		private Performer performer;

		[SetUp]
		public void SetUp()
        {
			this.stage = new Stage();
			this.performer = new Performer("Pesho","Toshov",21);
        } 

		[Test]
	    public void CtorCheck()
	    {
			Assert.IsNotNull(stage.Performers);
		}

		[TestCase(17)]
		[TestCase(10)]
		[TestCase(3)]
		[TestCase(0)]
		[TestCase(-3)]
		public void AddPerformerCheck1(int age)
        {
			Performer performer1 = new Performer("Tosho","Peshov",age);
			Assert.Throws<ArgumentException>(() => this.stage.AddPerformer(performer1));
        }

		[Test]
		public void AddPerformerCheck2()
		{
			this.stage.AddPerformer(performer);
			var perf = this.stage.Performers.FirstOrDefault();

			Assert.AreEqual(perf, performer);
			Assert.AreEqual(1, this.stage.Performers.Count);
		}

		[Test]
		public void AddSongCheck1()
        {
			Song song = new Song("Song", new TimeSpan(0,0,59));

			Assert.Throws<ArgumentException>(() => this.stage.AddSong(song));
        }

		[Test]
		public void AddSongCheck2()
		{
			Song song = new Song("Song", new TimeSpan(0, 1, 59));
			this.stage.AddPerformer(performer);
			this.stage.AddSong(song);

			Assert.AreEqual($"Song (01:59) will be performed by Pesho Toshov", this.stage.AddSongToPerformer(song.Name, this.performer.FullName));
		}

		[Test]
		public void AddSongToPerformer()
        {
            var song = new Song("Song", new TimeSpan(0, 3, 30));
			this.stage.AddPerformer(performer);
			this.stage.AddSong(song);
			this.stage.AddSongToPerformer("Song", this.performer.FullName);

			Assert.AreEqual("Pesho Toshov", this.performer.FullName);
			Assert.AreEqual(song, this.performer.SongList[0]);
			Assert.AreEqual(1, this.performer.SongList.Count);
        }

		[Test]
		public void PlayCheck()
        {
			Song song1 = new Song("Song1", new TimeSpan(0, 3, 33));
			Song song2 = new Song("Song2", new TimeSpan(0, 3, 33));
			Song song3 = new Song("Song3", new TimeSpan(0, 3, 33));
			
			this.stage.AddSong(song1);
			this.stage.AddSong(song2);
			this.stage.AddSong(song3);

			this.stage.AddPerformer(performer);
			this.stage.AddSongToPerformer("Song1", this.performer.FullName);
			this.stage.AddSongToPerformer("Song2", this.performer.FullName);
			this.stage.AddSongToPerformer("Song3", this.performer.FullName);

			Assert.AreEqual($"{this.stage.Performers.Count} performers played 3 songs", this.stage.Play());
		}

		[Test]
		public void ValidateNullValueCheck()
        {
			Song song = null;
			Performer performer2 = null;
			Assert.Throws<ArgumentNullException>(() => this.stage.AddPerformer(performer2));
			Assert.Throws<ArgumentNullException>(() => this.stage.AddSong(song));
		}

		[Test]
		public void GetPerformerCheck()
        {
			var song = new Song("Song", new TimeSpan(0, 3, 30));
			this.stage.AddPerformer(performer);
			this.stage.AddSong(song);

			Assert.Throws<ArgumentException>(() => this.stage.AddSongToPerformer("Song", "Shisho Bakshisho"));
		}

		[Test]
		public void GetSong()
		{
			var song = new Song("Song", new TimeSpan(0, 3, 30));
			this.stage.AddPerformer(performer);
			this.stage.AddSong(song);

			Assert.Throws<ArgumentException>(() => this.stage.AddSongToPerformer("SongSong", this.performer.FullName));
		}
	}
}