using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Data
	{
		public int ClientRecieve(string dat)
		{
			// Recieve Server Info as String
			// Parse Game-data, Pass on Player Stuff to ParsePlayers()
			// State|MenuInfo|MyID|Players|[P1=P2=P3=P4]
			string[] d = dat.Split('|');
			Int32.TryParse(d[0], out state);
			int id = Int32.Parse(d[2]);
			if (state == 0)
			{
				// Server State Should Only Be 0 if it wants to force close all clients
				Console.WriteLine("Server State 0");
				// ToDo: Add Code to Close Game Safley (currently crashes)
				return -1;
			}
			if (state == 1)
			{
				// Menu
				MenuInfo = Int32.Parse(d[1]);
				int P = Int32.Parse(d[3]);
				playercount = P;
				//Console.WriteLine(P);
				foreach (var playerdat in d[4].Split('='))
				{
					this.ParsePlayers(playerdat);
				}
			}
			return id;
		}
		public int ParsePlayers(string pdat)
		{
			// Only Called For Players Not Client
			// ID Active Name xPos yPos [Other Information]
			string[] ds = pdat.Split(' ');
			int id = Int32.Parse(ds[0]);
			if (id == -1)
			{
				id = playercount;
				playercount++;
			}
			if (!Players[id].active)
			{
				if (ds[1] == "1")
				{
					Players[id].Activate(ds[2], id);
				}
			}
			if (Players[id].active)
			{
				if (id != Game1.MyID)
				{
					Players[id].UpdateOthers(ds);				
				}
			}
			return id;
		}
	}
}
