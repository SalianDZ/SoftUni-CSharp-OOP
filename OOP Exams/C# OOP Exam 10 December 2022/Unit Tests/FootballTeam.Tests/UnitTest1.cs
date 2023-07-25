using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.TestRunAttachmentsProcessing;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FootballPlayerConstructorTest()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            Assert.IsNotNull(player);
            Assert.AreEqual(player.Name, "Messi");
            Assert.AreEqual(player.PlayerNumber, 10);
            Assert.AreEqual(player.Position, "Forward");
            Assert.AreEqual(0, player.ScoredGoals);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void FootballPlayerNameNegativeCase(string value)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballPlayer(value, 10, "Forward"));
            Assert.AreEqual(ex.Message, "Name cannot be null or empty!");
        }

        [Test]
        [TestCase(0)]
        [TestCase(22)]
        public void FootballPlayerNumberNegativeCase(int value)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballPlayer("Messi", value, "Forward"));
            Assert.AreEqual(ex.Message, "Player number must be in range [1,21]");
        }

        [Test]
        [TestCase("Mid")]
        [TestCase("Coach")]
        public void FootballPlayerPositionNegativeCase(string value)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballPlayer("Messi", 10, value));
            Assert.AreEqual(ex.Message, "Invalid Position");
        }

        [Test]
        public void FootballPlayerScoreMethod()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            Assert.IsNotNull(player);
            int expectedGoalsBeforeScoring = 0;
            int actualGoalsBeforeScoring = player.ScoredGoals;
            player.Score();
            player.Score();
            int expectedGoalsAfterScoring = 2;
            int actualGoalsAfterScoring = player.ScoredGoals;
            Assert.AreEqual(expectedGoalsBeforeScoring, actualGoalsBeforeScoring);
            Assert.AreEqual(expectedGoalsAfterScoring, actualGoalsAfterScoring);
        }

        [Test]
        public void FootballTeamConstructorTest()
        {
            FootballTeam team = new FootballTeam("Barcelona", 30);
            Assert.IsNotNull(team);
            Assert.AreEqual(team.Name, "Barcelona");
            Assert.AreEqual(team.Capacity, 30);
            Assert.AreEqual(team.Players, new List<FootballPlayer>());
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void FootballTeamNameNegativeCase(string value)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballTeam(value, 30));
            Assert.AreEqual(ex.Message, "Name cannot be null or empty!");

        }

        [Test]
        [TestCase(14)]
        [TestCase(-10)]
        public void FootballTeamCapacityNegativeCase(int value)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new FootballTeam("Barcelona", value));
            Assert.AreEqual(ex.Message, "Capacity min value = 15");
        }

        [Test]
        public void TestIfTeamAddPlayerMethodActuallyAddsAPlayer()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            FootballTeam team = new FootballTeam("Barcelona", 30);
            int expectedPlayersBeforeAdding = 0;
            int actualPlayersBeforeAdding = team.Players.Count;
            string result = team.AddNewPlayer(player);
            int expectedPlayersAfterAdding = 1;
            int actualPlayersAfterAdding = team.Players.Count;
            Assert.AreEqual(expectedPlayersBeforeAdding, actualPlayersBeforeAdding);
            Assert.AreEqual(expectedPlayersAfterAdding, actualPlayersAfterAdding);
            Assert.AreEqual(result, "Added player Messi in position Forward with number 10");
        }

        [Test]
        public void FootballTeamAddMethodNegativeCase()
        {
            FootballTeam team = new FootballTeam("Barcelona", 15);
            for (int i = 1; i <= 15; i++)
            {
                team.AddNewPlayer(new FootballPlayer(i.ToString(), i, "Forward"));
            }
            string result = team.AddNewPlayer(new FootballPlayer("Dembele",7, "Forward"));
            Assert.AreEqual(result, "No more positions available!");
            Assert.AreEqual(team.Players.Count, 15);
        }

        [Test]
        public void TestIfFootballTeamPickPlayerReturnsAPlayer()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            FootballPlayer player2 = new FootballPlayer("Dembele", 7, "Forward");
            FootballTeam team = new FootballTeam("Barcelona", 30);
            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);
            FootballPlayer wantedPlayer = team.PickPlayer("Messi");
            Assert.IsNotNull(wantedPlayer);
            Assert.AreEqual(wantedPlayer, player);
        }

        [Test]
        public void FootballTeamPickPlayerNegativeCase()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            FootballPlayer player2 = new FootballPlayer("Dembele", 7, "Forward");
            FootballTeam team = new FootballTeam("Barcelona", 30);
            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);
            FootballPlayer wantedPlayer = team.PickPlayer("Gavi");
            Assert.IsNull(wantedPlayer);
        }

        [Test]
        public void TestIfFootballTeamPlayerScoreMethodActuallyWorks()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            FootballTeam team = new FootballTeam("Barcelona", 30);
            team.AddNewPlayer(player);
            int expectedGoalsBeforeScoring = 0;
            int actualGoalsBeforeScoring = player.ScoredGoals;
            string result = team.PlayerScore(10);
            string result2 = team.PlayerScore(10);
            int expectedGoalsAfterScoring = 2;
            int actualGoalsAfterScoring = player.ScoredGoals;
            Assert.AreEqual(expectedGoalsBeforeScoring, actualGoalsBeforeScoring);
            Assert.AreEqual(expectedGoalsAfterScoring, actualGoalsAfterScoring);
            Assert.AreEqual(result, "Messi scored and now has 1 for this season!");
            Assert.AreEqual(result2, "Messi scored and now has 2 for this season!");

        }

        [Test]
        public void FootballTeamPlayerScoreMethodNegativeCase()
        {
            FootballPlayer player = new FootballPlayer("Messi", 10, "Forward");
            FootballTeam team = new FootballTeam("Barcelona", 30);
            team.AddNewPlayer(player);
            NullReferenceException ex = Assert.Throws<NullReferenceException>(() => team.PlayerScore(11));
        }
    }
}