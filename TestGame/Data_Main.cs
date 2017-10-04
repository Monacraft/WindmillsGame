using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Data
	{
		public int state; 			// 0 = Not connected, 1 = Menu, 2 = Game
		public int playercount;
		public int MenuInfo;		// Not useful rn, later for selecting level/settings
		public Player[] Players;	// All Player Data
		public Data()
		{
			state = 0;
			playercount = 0;
			Players = new Player[4];
			for (int i = 0; i < 4; i++)
			{
				this.Players[i] = new Player();
			}
		}
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Use For General Game Stuff (Not stuff specific to main player only)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			Players[id].Update(id, ref k, ref m, ref ok, ref om);

			for (int i = 0; i < playercount; i++)
			{
				if (Players[i].Grabbing != -1)
				{
					Players[Players[i].Grabbing].GrabbedBy = i;
					//Console.WriteLine("{0} Grabbed By {1}", Players[i].Grabbing, i);
				}
			}

			if (m.LeftButton == ButtonState.Pressed)
			{
				if (om.LeftButton == ButtonState.Released && Players[id].Grabbing != -1)
				{
					Players[Players[id].Grabbing].GrabbedBy = -1;
					Players[id].Grabbing = -1;
				}
				for (int i = 0; i < playercount; i++)
				{
					if (i != id)
						if (IsInside(mouseBorder(), Players[i].Pos))
						{
							Players[id].Grabbing = i;
							Console.WriteLine("You Grabbed P{0}", i);
						}
				}
			}

		}
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			for (int i = 0; i < playercount; i++)
			{
				Players[i].Draw(ref s, ref t);
			}
			for (int i = 0; i < playercount; i++)
			{
				if (Players[i].GrabbedBy != -1)
				{
					Players[i].DrawLine(ref s, Players[Players[i].GrabbedBy].Pos, Player.Colours[Players[i].GrabbedBy]);
				}
			}
		}
	}
}
