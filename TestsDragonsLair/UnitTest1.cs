using Microsoft.VisualStudio.TestTools.UnitTesting;
using DragonsLair;

namespace TestsDragonsLair
{
    [TestClass]
    public class DragonsLairTests
    {
        Tournament currentTournament;

        [TestInitialize]
        public void SetupForTest()
        {
            currentTournament = new Tournament("Vinter Turnering");
        }

        [TestMethod]
        public void TournamentHasEvenNumberOfTeams()
        {
            int numberOfTeams = currentTournament.Teams.Count;
            Assert.AreEqual(0, numberOfTeams % 2);
        }

        [TestMethod]
        public void EqualNumberOfWinnersAndLosersPerRound()
        {
            int numberOfRounds = currentTournament.GetNumberOfRounds();
            for (int round = 0; round < numberOfRounds - 1; round++)
            {
                Round currentRound = currentTournament.GetRound(round);
                int numberOfWinningTeams = currentRound.WinningTeams.Count;
                int numberOfLosingTeams = currentRound.LosingTeams.Count;
                Assert.AreEqual(numberOfWinningTeams, numberOfLosingTeams);
            }
        }

        [TestMethod]
        public void OneWinnerInLastRound()
        {
            // Verifies there is exactly one winner in last round
            int numberOfRounds = currentTournament.GetNumberOfRounds();
            Round currentRound = currentTournament.GetRound(numberOfRounds - 1);
            int numberOfWinningTeams = currentRound.WinningTeams.Count;
            Assert.AreEqual(1, numberOfWinningTeams);
        }

        [TestMethod]
        public void AllMatchesInPreviousRoundsFinished()
        {
            bool matchesFinished = true;
            int numberOfRounds = currentTournament.GetNumberOfRounds();
            for (int round = 0; round < numberOfRounds - 1; round++)
            {
                Round currentRound = currentTournament.GetRound(round);
                if (currentRound.IsRoundFinished() == false)
                    matchesFinished = false;
            }
            Assert.AreEqual(true, matchesFinished);
        }


    }
}
