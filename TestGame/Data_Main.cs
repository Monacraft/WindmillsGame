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
		public Vector2 bgPos;
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
			oldGrab = -1;
			bgPos = Game1.screenCenter;
			cbg = 0;
		}
		public double c;
		public int oldGrab;
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Use For General Game Stuff (Not stuff specific to main player only)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			Grab(id);

			Players[id].Update(id, ref k, ref m, ref ok, ref om);

			// Locked Screen:
			for (int i = 0; i<playercount; i++)
			{
				if (i != id)
				{
					Players[i].drawPos = Players[i].Pos - Players[id].Pos + Game1.screenCenter;
				}
			}

			PlayerInput(id, ref k, ref m, ref ok, ref om);

			//bgPos.X = Players[id].Pos.X % 640;  
			//bgPos.Y = Players[id].Pos.Y % 640;
			bgPos = Game1.screenCenter - Players[id].Pos;
		}
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			// Reference for center = Players[id].Pos

			DrawBG(ref s, 3, 3, 1);

			for (int i = 0; i < playercount; i++)
			{
				Players[i].Draw(ref s, ref t);
			}
			for (int i = 0; i < playercount; i++)
			{
				if (Players[i].Grabbing != -1)
				{
					// .Pos for unlocked
					Players[i].DrawLine(ref s, Players[Players[i].Grabbing].drawPos, Player.Colours[i]);
				}
			}
		}
	}
}
