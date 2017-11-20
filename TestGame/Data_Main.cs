using System;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Data
	{
		public int state;           // 0 = not connect, 1 = alive, 2 = dead!!!!!!!
		public int playercount;
		public Player[] Players;    // All Player Data
		public List<Planet> Planets;
		public bool DownloadedLevel;
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
			cbg = 0;
			Planets = new List<Planet>();
			DownloadedLevel = false;
		}
		public double c;
		public int oldGrab;
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Use For General Game Stuff (Not stuff specific to main player only)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			Players[id].Update(id, ref k, ref m, ref ok, ref om);
			PlayerInput(id, ref k, ref m, ref ok, ref om);
		}
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			// Reference for center = Players[id].Pos

			//DrawBG(ref s);
			DrawPlanets(ref s);

			for (int i = 0; i < playercount; i++)
			{
				Players[i].Draw(ref s, ref t);
			}

			// Players[i].DrawLine(ref s, Players[Players[i].Grabbing].Pos, Player.Colours[i]);
		}
	}
}
