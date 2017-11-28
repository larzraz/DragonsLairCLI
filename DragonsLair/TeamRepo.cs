using System.Collections.Generic;

namespace DragonsLair
{
    public class TeamRepo
    {
        private List<Team> teams = new List<Team>();
        
        public void RegisterTeam(Team team)
        {
            teams.Add(team);
        }

        public void RegisterTeam(string name)
        {
            Team newTeam = new Team(name);
            RegisterTeam(newTeam);
        }

        public Team GetTeam(string name)
        {
            Team team = null;
            bool found = false;
            int idx = 0;
            while (!found && (idx < teams.Count))
            {
                if (teams[idx].Name.Equals(name))
                {
                    found = true;
                    team = teams[idx];
                }
                idx++;
            }
            return team;
        }

        public List<Team> Teams()
        {
            return teams;
        }
    }
}
