using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonsLair
{
    public class Controller
    {
        private TournamentRepo tournamentRepository = null;

        public Controller()
        {
            tournamentRepository = new TournamentRepo();
            Init();
        }

        private void Init()
        {
            Console.WriteLine("Initialiserer...");
            TeamRepo teamRepository = new TeamRepo();
            PlayerRepo playerRepository = new PlayerRepo();
            string tournamentName = "default";
            Tournament tournament = new Tournament(tournamentName);

            Console.WriteLine("Registrerer spillere...");
            playerRepository.RegisterPlayer("Laust Ulriksen");
            playerRepository.RegisterPlayer("Matthias Therkelsen", null, "matthias@therkelsen.dk", "+45 47002155");
            playerRepository.RegisterPlayer("Martin Bertelsen", "Nyborgvej 10, Odense", null, "+45 22521112");
            playerRepository.RegisterPlayer("Line Madsen", "Kochsgade 21, Odense", "linem@msn.dk", "+45 00142563");
            playerRepository.RegisterPlayer(new Player("Jette Detlevsen"));

            Console.WriteLine("Registrerer teams...");
            // initialize with a default set of teams
            teamRepository.RegisterTeam("The Valyrians");
            teamRepository.RegisterTeam("The Spartans");

            Console.WriteLine("Tilføjer spillere til teams...");
            // Add players to team valyrians
            Team teamValyrians = teamRepository.GetTeam("The Valyrians");
            teamValyrians.AddPlayer(playerRepository.GetPlayer("Laust Ulriksen"));
            teamValyrians.AddPlayer(playerRepository.GetPlayer("Matthias Therkelsen"));

            // Add players to team spartans
            Team teamSpartans = teamRepository.GetTeam("The Spartans");
            teamSpartans.AddPlayer(playerRepository.GetPlayer("Martin Bertelsen"));
            teamSpartans.AddPlayer(playerRepository.GetPlayer("Line Madsen"));
            teamSpartans.AddPlayer(playerRepository.GetPlayer("Jette Detlevsen"));

            Console.WriteLine("Registrerer Liga...");
            // initialize with a default tournament
            tournamentRepository.RegisterTournament(tournament);

            Console.WriteLine("Tilføjer teams til Liga...");
            // Add teams to tournament
            tournament = tournamentRepository.GetTournament(tournamentName);
            tournament.AddTeam(teamValyrians);
            tournament.AddTeam(teamSpartans);

            Console.WriteLine("Planlægger 1. runde...");
            // Initialize first round (this also initializes matches)
            ScheduleNewRound(tournamentName);
            Console.WriteLine("All set!");
            for (int i = 0; i < 100000; i++) ;
        }

        private string PadSpaceToName(string name, int length)
        {
            for (int i = name.Length; i < length; i++)
            {
                name = name + " ";
            }
            return name;
        }

        private void PrintHeader()
        {
            Console.WriteLine("  #####");
            Console.WriteLine(" #     # ##### # #      #      # #    #  ####  ");
            Console.WriteLine(" #         #   # #      #      # ##   # #    # ");
            Console.WriteLine("  #####    #   # #      #      # # #  # #      ");
            Console.WriteLine("       #   #   # #      #      # #  # # #  ### ");
            Console.WriteLine(" #     #   #   # #      #      # #   ## #    # ");
            Console.WriteLine("  #####    #   # ###### ###### # #    #  #### ");
            Console.WriteLine("0--------------------------------------------------0");
        }

        private void PrintFooter()
        {
            Console.WriteLine("0--------------------------------------------------0\n");
        }

        private int GetHighestScore(Dictionary<string, int> teamNameToScore)
        {
            int highestScore = 0;
            foreach (KeyValuePair<string, int> element in teamNameToScore)
            {
                if (highestScore < element.Value)
                {
                    highestScore = element.Value;
                }
            }
            return highestScore;
        }

        public void ShowScore(string tournamentName)
        {
            Dictionary<string, int> teamNameToScore = new Dictionary<string, int>();

            Tournament selectedTournament = tournamentRepository.GetTournament(tournamentName);
            if (selectedTournament == null)
            {
                Console.WriteLine("Turnering findes ikke");
            }
            else
            {
                int numberOfRounds = selectedTournament.GetNumberOfRounds();
                int numberOfMatches = 0;
                for (int i = 0; i < numberOfRounds; i++)
                {
                    Round currentRound = selectedTournament.GetRound(i);
                    numberOfMatches = numberOfMatches + currentRound.GetNumberOfMatches();
                    foreach (Team winningTeam in currentRound.WinningTeams)
                    {
                        if (!teamNameToScore.ContainsKey(winningTeam.Name))
                            teamNameToScore.Add(winningTeam.Name, 0);
                        teamNameToScore[winningTeam.Name] = teamNameToScore[winningTeam.Name] + 1;
                        //note way to update value using key as index
                    }
                }

                //output in sorted order starting with highest score - find highest score
                int highestScore = GetHighestScore(teamNameToScore);

                PrintHeader();
                Console.WriteLine($"|      Liga : {tournamentName}                     |");
                Console.WriteLine($"|      Spillede runder : {numberOfRounds}                         |");
                Console.WriteLine($"|      Spillede kamper : {numberOfMatches}                         |");
                Console.WriteLine("-----------------------------------| Vundne kampe  |");

                for (int i = highestScore; i >= 0; i--)
                {
                    foreach (KeyValuePair<string, int> element in teamNameToScore)
                    {
                        if (element.Value == i)
                        {
                            int score = highestScore - i + 1;
                            string name = PadSpaceToName(element.Key, 18);
                            Console.WriteLine($"|         {score}. {name}    |      {element.Value}        |");
                        }
                    }
                }
                PrintFooter();
            }
        }

        private List<int> GetRandomIndices(int count)
        {
            Random r = new Random();
            List<int> result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                result.Add(i);
            }
            for (int i = 0; i < 100; i++)
            {
                int idx1 = r.Next(count);
                int idx2 = r.Next(count);
                int temp = result[idx1];
                result[idx1] = result[idx2];
                result[idx2] = temp;
            }
            return result;
        }

        public void ShowRound(Round round)
        {
            int i = 1;
            foreach (Match m in round.GetAllMatches())
            {
                Console.WriteLine($"{i}. {m.FirstOpponent}  -  {m.SecondOpponent}");
                i++;
            }
            Console.ReadKey();
        }

        public void ScheduleNewRound(string tournamentName)
        {
            Tournament selectedTournament = tournamentRepository.GetTournament(tournamentName);
            List<Team> teams = null;
            Round currentRound = null;

            int numberOfRounds = selectedTournament.GetNumberOfRounds();
            if (numberOfRounds == 0)
            {
                // SPECIAL CASE: Initialize first round. Handled by adding all teams from tournament 
                teams = selectedTournament.Teams();
            }
            else
            {
                // We are scheduling round > 1
                currentRound = selectedTournament.GetRound(numberOfRounds - 1);
                if (currentRound.IsRoundFinished())
                {
                    teams = currentRound.WinningTeams;
                    if (teams.Count > 1)
                    {
                        Round newRound = new Round();
                        List<int> indices = GetRandomIndices(teams.Count);
                        if ((teams.Count % 2) != 0)//A team should have a free ride
                        {
                            if (currentRound.FreeRider != null) //There was a freeRider in last Round
                            {
                                //Must not be the same freeRider this time
                                if (currentRound.FreeRider.Equals(teams[0]))
                                {
                                    newRound.FreeRider = teams[1];
                                    teams.Remove(teams[1]);
                                }
                                else
                                {
                                    newRound.FreeRider = teams[0];
                                    teams.Remove(teams[0]);
                                }
                            }
                        }
                        int noOfMatches = teams.Count / 2;
                        for (int i = 0; i < noOfMatches; i++)
                        {
                            Match newMatch = new Match();
                            newMatch.FirstOpponent = teams[2 * i];
                            newMatch.SecondOpponent = teams[2 * i + 1];
                            newRound.AddMatch(newMatch);
                        }
                        selectedTournament.AddRound(newRound);
                        ShowRound(newRound);
                    }
                    else
                    {
                        //Either 0 or 1 members in teams
                        selectedTournament.Status = Tournament.State.FINISHED;
                    }
                }
                else
                {
                    throw new Exception("Round is not finished");
                }
            }
        }
        
        public void SaveMatch(string tournamentName, int roundNumber, string team1, string team2, string winningTeam)
        {
            Tournament tournament = tournamentRepository.GetTournament(tournamentName);
            Round selectedRound = tournament.GetRound(roundNumber - 1);
            Team winner = tournament.GetTeam(winningTeam);
            Match m = selectedRound.GetMatch(team1, team2);
            m.Winner = tournament.GetTeam(winningTeam);
        }
    }
}
