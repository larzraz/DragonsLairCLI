using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DragonsLair
{
    public class TournamentRepo
    {
        public List<Tournament> tournaments = new List<Tournament>();

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
            int idx = 0;
            while((tournament == null) && (idx < tournaments.Count))
            {
                if (tournaments[idx].Name.Equals(name))
                {
                    tournament = tournaments[idx];
                }
                idx++;
            }
            return tournament;
        }
        public void ListAllTournaments()
        {
            foreach (Tournament tournament in tournaments)
            {
                
            }
        }
     
    }
}
