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
		public void Grab(int id)
		{
			if (oldGrab != -1)
			{
				if (Players[oldGrab].Grabbing != id)
				{
					oldGrab = -1;
				}
			}
			for (int i = 0; i<playercount; i++)
			{
				if(Players[i].active)
					Players[i].canGrab = true;
			}
			for (int i = 0; i<playercount; i++)
			{
				if (Players[i].active)
				{
					if (Players[i].Grabbing != -1)
					{
						if (Players[i].Grabbing == id)
						{
							Players[i].canGrab = false; //Can't grab players grabbing you
							if (oldGrab != i)
							{
								c = Math.Sqrt(
									Math.Pow((Players[id].Pos.X - Players[i].Pos.X), 2) +
									Math.Pow((Players[id].Pos.Y - Players[i].Pos.Y), 2)
									);
								oldGrab = i;
							}
							Players[id].Pos.X = Players[i].Pos.X + (float)(c * Math.Cos(Players[i].rotation - Math.PI / 2));
							Players[id].Pos.Y = Players[i].Pos.Y + (float)(c * Math.Sin(Players[i].rotation - Math.PI / 2));
						}
						Players[Players[i].Grabbing].canGrab = false; // Can't grab grabbed players
					}
				}
			}
		}
		public void PlayerInput(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{

			if (m.LeftButton == ButtonState.Pressed)
			{
				if (om.LeftButton == ButtonState.Released && Players[id].Grabbing != -1)
				{
					Players[id].Grabbing = -1;
				}
				for (int i = 0; i<playercount; i++)
				{
					if (i != id && Players[i].active && Players[i].canGrab)
					if ((mouseBorder().Contains(Players[i].drawPos))) 			// .Pos for unlocked
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
			if (k.IsKeyDown(Keys.B) && ok.IsKeyUp(Keys.B))
			{
				cbg++;
				if (cbg == Game1.bg.Length)
					cbg = 0;
			}
		}
		public int cbg;
		public void DrawBG(ref SpriteBatch s, int width, int height, int scale)
		{
			// BgPos gives center of grid width*height
			Vector2 Bgcenter = new Vector2((width * Game1.bg[cbg].Width * scale) / 2, (height * Game1.bg[cbg].Height * scale) / 2);
			Vector2 drawat = bgPos - Bgcenter; // Top Left Item

			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					s.Draw(Game1.bg[cbg], drawat, null,
							 Color.White, 0f, Vector2.Zero, new Vector2(scale), SpriteEffects.None, 1);
					drawat.Y += Game1.bg[cbg].Height * scale;
				}
				drawat.X += Game1.bg[cbg].Width * scale;
				drawat.Y = bgPos.Y - Bgcenter.Y;
			}
		}
	}
}
