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
		public List<int> TurnOrder;
		public List<int> Disconnect;
		string servername;
		string serversettings;

		public Data(string Name, string settings)
		{
			state = 0;
			playercount = 0;
			Players = new Player[16];
			for (int i = 0; i < 16; i++)
			{
				Players[i] = new Player(i);
				Players[i].active = 0;
			}
			Slots = new List<int>();
			for (int i = 0; i < 16; i++)
			{
				Slots.Add(i);
			}
			Planets = new List<Planet>();
			FiredArrows = new List<Arrow>();
			Disconnect = new List<int>();
			LockRead = false;
			TurnOrder = new List<int>();
			currentTurn = 0;
			PlanetData = "";
			servername = Name;
			serversettings = settings;
			creatingLevel = false;

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
				LockRead = true;
				TurnOrder.Add(id);
				LockRead = false;
				Players[id] = new Player(id);
				playercount++;
			}
			if (Players[id].active == 1) // becomes 1 after they recieve an ID
			{
				Players[id].UpdateThis(pdat);
				if (Players[id].FireArrow)
				{
					if (currentTurn >= TurnOrder.Count)
						currentTurn = 0;
					if (TurnOrder[currentTurn] == id)
					{
						Console.WriteLine("{0} fired an Arrow", id);
						FireArrow(id);
						currentTurn++;
					}
					Players[id].FireArrow = false;
				}
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
			LockRead = true;
			TurnOrder.Remove(id);
			LockRead = false;
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
			foreach (var i in Players)
			{
				s += i.ID.ToString() + " " + i.String() + "=";

			}
			if(TurnOrder.Count > 0)
				s = s.Substring(0, (s.Length - 1));
			s += "|";
			foreach (var f in FiredArrows)
			{
				s += f.Write() + "=";
			}
			if(FiredArrows.Count > 0)
				s = s.Substring(0, (s.Length - 1));
			return s;
		}
		public int Recieve(string dat)
		{
			return ParsePlayer(dat);
		}
		public bool creatingLevel;
		public void NewLevel()
		{
			creatingLevel = true;
			Planets.Clear();
			Planets.AddRange(Planet.GenerateLevel(7));
			PlanetData = "";
			foreach (var p in Planets)
			{
				PlanetData += p.Write() + "=";
			}
			PlanetData = PlanetData.Substring(0, PlanetData.Length - 1);
			foreach (var i in Lock(ref TurnOrder))
			{
				if (Players[i].active == 2)
				{
					// Respawn Them
					Players[i].active = 1;
				}
				Players[i].DownloadedLevel = false;
			}
			creatingLevel = false;
		}
		public bool LockRead;
		public List<int> Lock(ref List<int> T)
		{
			while (LockRead)
			{
			}
			return T;
		}
	}
}