﻿using System.Collections.Generic;

namespace DragonsLair
{
    class PlayerRepo
    {
        private List<Player> players = new List<Player>();

        public void RegisterPlayer(Player player)
        {
            players.Add(player);
        }

        public void RegisterPlayer(string name, string address = null, string email = null, string telephone = null)
        {
            Player newPlayer = new Player(name, address, email, telephone);
            RegisterPlayer(newPlayer);
        }

        public Player GetPlayer(string name)
        {
            Player foundPlayer = null;
            int idx = 0;
            while ((foundPlayer == null) && (idx < players.Count))
            {
                if (players[idx].Name.Equals(name))
                {
                    foundPlayer = true;
                    foundPlayer = players[idx];
                }
                idx++;
            }
            return foundPlayer;
        }
    }
}