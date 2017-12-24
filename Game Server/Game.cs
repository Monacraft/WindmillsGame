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
			foreach (var i in Lock(ref TurnOrder))
			{
				if (Players[i].active == 1)
				{
					alldead = false;
				}
			}
			if (alldead && !creatingLevel)
			{
				NewLevel(); // New map
				foreach (var i in Lock(ref TurnOrder))
				{
					// Reset Player Variables
					Players[i].DownloadedLevel = false;
					Players[i].Revive = true;
				}
			}
			else
			{
				ArrowUpdate();
			}
		}

		public int currentTurn;
		public void PlayerUpdate()
		{
			
		}
	}
}
