using System;
namespace Server
{
	public class GameState
	{
		// State|MenuInfo|MyID|Players|[P1=P2=P3=P4]|[ServerName=ServerMaps,,,]
		// ID Active Name xPos yPos
		public int state; // 0 = Connecting-Players, 1 = In-Menu, 2 = In-Game
		public int MenuInfo; // What Menu Option is currently selected
		public int playercount;
		public string[] Players;
		public int[] PlayerState;  // 0 = EmptySlot, 1 = First Connect, 2 = Connected
		string ServerName;
		string ServerMaps;
		public GameState(string Name)
		{
			state = 0;
			MenuInfo = 0;
			playercount = 0;
			Players = new string[4];
			PlayerState = new int[4];
			ServerName = Name;
			ServerMaps = "None1,None2";
		}
		public int Recieve(string data)
		{
			// ID Active Name xPos yPos			
			string[] d = data.Split(' ');
			string tail = "";
			for (int i = 1; i < d.Length; i++)
			{
				tail += " " + d[i];
			}
			//Console.WriteLine(data);
			int id = Int32.Parse(d[0]);
			if (id == -1)
			{
				id = playercount;
				playercount++;
				PlayerState[id] = 1;
			}
			else
			{
				if (PlayerState[id] == 1)
					PlayerState[id] = 2;
			}
			char[] spl = new char[1];
			spl[0] = ' ';
			//Players[id] = id.ToString() + " " + d[1] + " " + d[2] + " " + d[3] + " " + d[4];
			Players[id] = id.ToString() + tail;
			return id;
		}
		public string Send(int PlayerTo)
		{
			string s = "";
			s = state.ToString() + "|" + MenuInfo.ToString() + "|" + PlayerTo.ToString() + "|" + playercount.ToString() + "|";
			for (int i = 0; i < playercount; i++)
			{
				s += Players[i] + "=";
			}
			string sr = s.Substring(0, (s.Length - 1));
			if (PlayerState[PlayerTo] == 1)
				sr += "|" + ServerName + "=" + ServerMaps;
			return sr;
		}
		public void Gravity()
		{
			for (int i = 0; i<playercount; i++)
			{
				Players[i] += "0";
			}
		}
	}
}
