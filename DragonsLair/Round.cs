using System.Collections.Generic;

namespace DragonsLair
{
    public class Round
    {
        private List<Match> matches = new List<Match>();
        public Team FreeRider { get; set; }
        public List<Team> WinningTeams {
            get {
                List<Team> winningTeams = new List<Team>();
                foreach (Match m in matches)
                {
                    if (m.Winner != null)
                        winningTeams.Add(m.Winner);
                }
                if (FreeRider != null)
                {
                    winningTeams.Add(FreeRider);//A freeRider is a winner pr definition
                }
                return winningTeams;
            }
        }
        public List<Team> LosingTeams {
            get {
                List<Team> losingTeams = new List<Team>();
                foreach (Match m in matches)
                {
                    if (m.Winner != null)
                    {
                        if (m.Winner == m.FirstOpponent)
                            losingTeams.Add(m.SecondOpponent);
                        else
                            losingTeams.Add(m.FirstOpponent);
                    }
                }
                return losingTeams;
            }
        }

        public void AddMatch(Match m)
        {
            matches.Add(m);
        }

        public int GetNumberOfMatches()
        {
            return matches.Count;
        }

        public Match GetMatch(Team team1, Team team2)
        {
            return GetMatch(team1.Name, team2.Name);
        }

        public Match GetMatch(string teamName1, string teamName2)
        {
            Match foundMatch = null;
            bool found = false;
            int i = 0;
            while (!found && (i < matches.Count))
            {
                Match current = matches[i];
                if (current.FirstOpponent.Name.Equals(teamName1) && current.SecondOpponent.Name.Equals(teamName2))
                {
                    foundMatch = current;
                    found = true;
                }
            }

            return foundMatch;
        }

        public List<Match> GetAllMatches()
        {
            return matches;
        }

        public bool IsRoundFinished()
        {
            bool allMatchesGotWinner = true;
            int idx = 0;
            while((idx < matches.Count) && allMatchesGotWinner)
            {
                allMatchesGotWinner = (matches[idx].Winner != null);
                idx++;
            }
            return allMatchesGotWinner;
        }
    }
}
