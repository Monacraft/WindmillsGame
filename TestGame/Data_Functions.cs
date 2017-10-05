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
	}
}
