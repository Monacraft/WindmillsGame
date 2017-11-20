using System;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;

namespace Server
{
	public partial class Data
	{
		Timer Game;

		public void StartGame()
		{
			Game = new Timer(1000f / 128f);
			Game.Elapsed += Update;
			Game.AutoReset = true;
			Game.Enabled = true;
		}
		public void Update(object sender, ElapsedEventArgs e)
		{
			bool alldead = true;
			if (playercount == 0)
			{
				alldead = false;
			}
			for (int i = 0; i < playercount; i++)
			{
				if (Players[i].active == 1)
				{
					alldead = false;
				}
			}
			if (alldead)
			{
				NewLevel(); // New map
				for (int i = 0; i < playercount; i++)
				{
					// Reset Player Variables
					Players[i].DownloadedLevel = false;
					Players[i].Revive = true;
				}
			}
		}
		public void Arrow(int id)
		{
			// id = Who fired the arrow
			Players[id]

		}
	}

}
