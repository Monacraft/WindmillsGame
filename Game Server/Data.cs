using System;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;

namespace Server
{
	public partial class Data
	{
		public int state; // 0 = Not connected, 1 = Menu, 2 = Game
		public int playercount;
		public string PlanetData;
		public Player[] Players;
		public List<Planet> Planets;
		public List<int> Slots;
		string servername;
		string serversettings;

		public Data(string Name, string settings)
		{
			state = 0;
			playercount = 0;
			Players = new Player[16];
			Slots = new List<int>();
			for (int i = 0; i < 16; i++)
			{
				Slots.Add(i);
			}
			Planets = new List<Planet>();
			PlanetData = "";
			servername = Name;
			serversettings = settings;

			NewLevel();
			StartGame();
		}
		public int ParsePlayer(string pdat)
		{
			// ID Active(state) Name xPos yPos Colour 
			string[] ds = pdat.Split(' ');
			int id = Int32.Parse(ds[0]);
			if (id == -1)
			{
				id = Slots[0];
				Slots.RemoveAt(0);
				Players[id] = new Player();
				playercount++;
			}
			if (Players[id].active == 1) // becomes 1 after they recieve an ID
			{
				Players[id].UpdateThis(pdat);
			}
			if (Players[id].active == 2)
			{
				// Players is dead
			}
			return id;
		}
		public void DisconnectRecieve(string data)
		{
			string[] d = data.Split(' ');

			int id = Int32.Parse(d[1]);
			Console.WriteLine(id);
			Players[id].active = 0;
			playercount--;
			Slots.Insert(0, id);
		}
		public string ServerSend(int PlayerTo)
		{
			string s = "";
			string LevelInfo = "";
			if (!Players[PlayerTo].DownloadedLevel)
			{
				LevelInfo = PlanetData;
				Players[PlayerTo].DownloadedLevel = true;
				Console.WriteLine("Sending Level");
			}
			s = state.ToString() + "|" + LevelInfo + "|" + PlayerTo.ToString() + "|" + this.playercount.ToString() + "|";
			for (int i = 0; i < playercount; i++)
			{
				s += i.ToString() + " " + Players[i].String() + "=";
			}
			return s.Substring(0, (s.Length - 1));
		}
		public int Recieve(string dat)
		{
			return ParsePlayer(dat);
		}
		public void NewLevel()
		{
			Planets.Clear();
			Planets.AddRange(Planet.GenerateLevel(7));
			PlanetData = "";
			foreach (var p in Planets)
			{
				PlanetData += p.Write() + "=";
			}
			PlanetData = PlanetData.Substring(0, PlanetData.Length - 1);
			for (int i = 0; i < playercount; i++)
			{
				if (Players[i].active == 2)
				{
					// Respawn Them
					Players[i].active = 1;
				}
				Players[i].DownloadedLevel = false;
			}
		}
	}
}
