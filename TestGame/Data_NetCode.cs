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
			// State|MenuInfo|MyID|Players|[P1=P2=P3=P4]|ID=X=Y=Rotation
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
				//MenuInfo = d[1]
				if (d[1] != "")
				{
					Console.WriteLine(";{0};", d[1]);
					DownloadedLevel = false;
					while (drawing)
					{
					}
					Planet.currPlanets = 0;

					foreach (var planetdat in d[1].Split('='))
					{
						Planets[Planet.currPlanets] = Planet.Read(planetdat);
						Planet.currPlanets++;
						Console.WriteLine(planetdat);
					}
					for (int i = Planet.currPlanets; i < Planet.maxPlanets; i++)
					{
						Planets[i] = new Planet();
					}
					Console.WriteLine("Finished Download");
					DownloadedLevel = true;
				}
				playercount = Int32.Parse(d[3]);
				//Console.WriteLine(P);
				foreach (var playerdat in d[4].Split('='))
				{
					this.ParsePlayers(playerdat);
				}
				if (d[5] == "")
				{
					foreach (var a in Arrows)
					{
						a.Fired = false;
					}
				}
				else 
				{
					int c = 0;
					foreach (var f in d[5].Split('='))
					{
						Arrows[c].ArrowRead(f);
					}
				}
			}
			return id;
		}

		public int ParsePlayers(string pdat)
		{
			// Only Called For Players Not Client
			// ID Active Name xPos yPos Planet Bearing
			string[] ds = pdat.Split(' ');
			//Console.WriteLine(pdat + " " + ds[0]);
			int id = Int32.Parse(ds[0]);
			if (id == -1)
			{
				id = playercount;
				playercount++;
			}
			//Console.WriteLine("{0} {1}", id, Players.Length);
			if (Players[id].active == 0)
			{
				if (ds[1] == "1")
				{
					Players[id].Activate(ds[2], id);
				}
			}
			if (Players[id].active > 0)
			{
				Players[id].active = int.Parse(ds[1]);
				if (Players[id].active == 1)
				{
					if (Players[id].Kill)
					{
						Players[id].active = 2;
					}
				}
				else
				{
					if (Players[id].active == 2)
						Players[id].Kill = false;
				}
				if (Players[id].active == 0)
				{
					// Lol you got fucked mate
				}
				else
				{
					if (id != Game1.MyID)
					{
						Players[id].UpdateOthers(ds);
					}
				}
			}
			return id;
		}
	}
}
