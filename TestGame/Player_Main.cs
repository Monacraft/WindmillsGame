using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Player
	{
		//public float x;
		//public float y;
		public Vector2 Pos;
		public int active; // Used to decide if they're alive or dead
		public string Name;
		public int ID;
		public static Color[] Colours = {
			Color.Red, Color.Blue, Color.Green, Color.Orange, 
			Color.Yellow, Color.Purple, Color.Brown, Color.Pink,
			Color.DimGray, Color.Ivory, Color.LightBlue, Color.LimeGreen};
		public int planet;
		public double bearing;
		public bool Kill;

		public Player()
		{
			Pos = new Vector2();
			active = 0;
			ID = -1;
		}
		public void UpdateOthers(string[] d)
		{
			if (active > 0)
			{
				/// **************
				/// Only Called for OTHER Players
				/// Protocol for Player Information:
				/// 
				///          0  1      2    3    4    5       6       7        8
				/// Planned: ID Active Name xPos yPos Planet Bearing
				/// Current: ID Active Name xPos yPos Planet Bearing
				/// 
				/// **************

				Pos.X = float.Parse(d[3], CultureInfo.InvariantCulture);
				Pos.Y = float.Parse(d[4], CultureInfo.InvariantCulture);
				this.Name = d[2];
				bearing = double.Parse(d[6]);
				//this.rotation = Double.Parse(d[6]);
			}
		}
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Called For the Client's Player
			// Only Use To Update Player Specific Stuff (not involving not-this-player variables)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			// Controls

			if (k.IsKeyDown(Keys.A))
				bearing -= 0.1f;
			if (k.IsKeyDown(Keys.D))
				bearing += 0.1f;
			Pos.X = m.X;
			Pos.Y = m.Y;

		}
		public Vector2 drawTo;
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			if (active == 1)
			{
				// Crosshair
				s.Draw(t, Pos, new Rectangle(108, 108, 40, 40),
				       Colours[this.ID], 0, new Vector2(20, 20), new Vector2(1f), SpriteEffects.None, 1);

				// TODO: Replace ID with planet (below)
				s.Draw(Game1.triangle, PlanetPos(ID), null, Colours[ID], (float)(bearing+Math.PI),
				       new Vector2(128f, 256f), new Vector2(32f / 256f), SpriteEffects.None, 1);
			}
		}
	}
}
