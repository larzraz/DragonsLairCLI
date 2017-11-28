using System.Collections.Generic;

namespace DragonsLair
{
    class TournamentRepo
    {
        private List<Tournament> tournaments = new List<Tournament>();

        public void RegisterTournament(string name)
        {
            Tournament newTournament = new Tournament(name);
            RegisterTournament(newTournament);
        }

        public void RegisterTournament(Tournament tournament)
        {
            tournaments.Add(tournament);
        }

        public Tournament GetTournament(string name)
        {
            Tournament tournament = null;
            bool found = false;
            int idx = 0;
            while(!found && (idx < tournaments.Count))
            {
                if (tournaments[idx].Name.Equals(name))
                {
                    found = true;
                    tournament = tournaments[idx];
                }
                idx++;
            }
            return tournament;
        }
    }
}
