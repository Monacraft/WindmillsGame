using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Data
	{
		public int state;           // 0 = Not connected, 1 = Menu, 2 = Game
		public int playercount;
		public int MenuInfo;        // Not useful rn, later for selecting level/settings
		public Player[] Players;    // All Player Data
		public Data()
		{
			state = 0;
			playercount = 0;
			Players = new Player[16];  			// Max connections to a single-server is 16.
												// Need to add recycling player slots server side
			for (int i = 0; i < 16; i++)
			{
				this.Players[i] = new Player();
			}
		}
		public double c;
		//public int oldGrab;
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Use For General Game Stuff (Not stuff specific to main player only)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago
			for (int i = 0; i < playercount; i++)
			{
				Players[i].canGrab = true;
			}
			for (int i = 0; i < playercount; i++)
			{
				if (Players[i].Grabbing != -1)
				{
					if (Players[i].Grabbing == id)
					{
						Players[i].canGrab = false; //Can't grab players grabbing you
						c = Math.Sqrt(
							Math.Pow((Players[id].Pos.X - Players[i].Pos.X), 2) +
							Math.Pow((Players[id].Pos.Y - Players[i].Pos.Y), 2)
							);
						Players[id].Pos.X = Players[i].Pos.X + (float)(c * Math.Cos(Players[i].rotation - Math.PI / 2));
						Players[id].Pos.Y = Players[i].Pos.Y + (float)(c * Math.Sin(Players[i].rotation - Math.PI / 2));
						if (true)
						{
							Console.WriteLine("{0}, {1}: ({2}, {3})",
											 c, Players[i].rotation,
											 Players[id].Pos.X,
											 Players[id].Pos.Y);
						}
					}
					Players[Players[i].Grabbing].canGrab = false; // Can't grab grabbed players
				}
			}

			Players[id].Update(id, ref k, ref m, ref ok, ref om);

			if (m.LeftButton == ButtonState.Pressed)
			{
				if (om.LeftButton == ButtonState.Released && Players[id].Grabbing != -1)
				{
					Players[id].Grabbing = -1;
				}
				for (int i = 0; i < playercount; i++)
				{
					if (i != id && Players[i].active && Players[i].canGrab)
						if (IsInside(mouseBorder(), Players[i].Pos))
						{
							Players[id].Grabbing = i;
							Console.WriteLine("You Grabbed P{0}", i);
						}
				}
			}
			if (m.RightButton == ButtonState.Pressed)
			{
				Players[id].Grabbing = -1;
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
				if (Players[i].Grabbing != -1)
				{
					Players[i].DrawLine(ref s, Players[Players[i].Grabbing].Pos, Player.Colours[i]);
				}
			}
		}
	}
}
