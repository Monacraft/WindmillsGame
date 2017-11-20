using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Data
	{
		// ToDo: Add Functions for loading levels and Other Game Mechanics
		public Rectangle mouseBorder()
		{
			return new Rectangle(
				(int)Game1.mouse.X - 20,
				(int)Game1.mouse.Y - 20,
				40,
				40);
		}
		public void PlayerInput(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{

			if (m.LeftButton == ButtonState.Pressed)
			{

			}
			if (m.RightButton == ButtonState.Pressed && om.RightButton == ButtonState.Released)
			{
				Players[id].Kill = true;
				Players[id].active = 2; // Kill Player

			}
			if (om.X != m.X)
			{
				//Console.WriteLine("{0},{1}", om.X.ToString(), om.Y.ToString());
			}
			if (k.IsKeyDown(Keys.B) && ok.IsKeyUp(Keys.B))
			{
				cbg++;
				if (cbg == Game1.bg.Length)
					cbg = 0;
			}
		}
		public int cbg;
		public void DrawBG(ref SpriteBatch s)
		{
			s.Draw(Game1.bg[cbg], Vector2.Zero, null,
					 Color.White, 0f, Vector2.Zero, new Vector2(2), SpriteEffects.None, 1);
		}
		public Vector2 PlanetPos(Planet P)
		{

			return Vector2.Zero;
		}
		public void DrawPlanets(ref SpriteBatch s)
		{
			int count = 0;
			if (DownloadedLevel)
				if (Game1.MyID != -1)
				{
					for (int i = 0; i < Planets.Count; i++)
					{
						//Console.WriteLine("Draw {0}, {1}, {2}, {3}", Planets[i].x, Planets[i].y, Player.Colours[count], Planets[i].size / 128f);
						s.Draw(Game1.circle, new Vector2(Planets[i].x, Planets[i].y), null,
							   Player.Colours[count], 0f, new Vector2(128, 128), new Vector2(Planets[i].size / 256f), SpriteEffects.None, 0);
						count++;
					}
				}
		}
	}
}

