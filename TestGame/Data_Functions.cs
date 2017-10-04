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
		public bool IsInside(Rectangle r, Vector2 pos)
		{
			//if (pos.X < r.X + r.Width && pos.X > r.X)
			//	if (pos.Y < r.Y + r.Height && pos.Y > r.Y)
			//		return true;
			return r.Contains(pos);
		}
		public void Grab()
		{
			
		}
	}
}
