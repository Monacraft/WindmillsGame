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
		public bool active; // Used to decide if they've sorted out they're details
		public string Name;
		public int ID;
		public int colorID;
		public int colorDefault;    // Black = They haven't chosen color
		public int Grabbing;        // ID's
		// Local Variables
		public bool canGrab;        // LOCALLY DEFINED for now
		public Vector2 drawPos;
		public static Color[] Colours = {
			Color.Red, Color.Blue, Color.Green, Color.Orange, 
			Color.Yellow, Color.Purple, Color.Brown, Color.Pink,
			Color.DimGray, Color.Ivory, Color.LightBlue, Color.LimeGreen,
			Color.Black};
		public double rotation;

		public Player()
		{
			Pos = new Vector2();
			active = false;
			ID = -1;
			rotation = 0;
			colorDefault = 6;
			colorID = Colours.Length - 1;
			Grabbing = -1;
			drawPos = Game1.screenCenter;
		}
		public void UpdateOthers(string[] d)
		{
			if (active)
			{
				/// **************
				/// Only Called for OTHER Players
				/// Protocol for Player Information:
				/// 
				///          0  1      2    3    4    5      6        7        8
				/// Planned: ID Active Name xPos yPos Colour Rotation Grabbing
				/// Current: ID Active Name xPos yPos Colour Rotation
				/// 
				/// **************
				Pos.X = float.Parse(d[3], CultureInfo.InvariantCulture);
				Pos.Y = float.Parse(d[4], CultureInfo.InvariantCulture);
				this.Name = d[2];
				this.colorID = Int32.Parse(d[5]);
				this.rotation = Double.Parse(d[6]);
				this.Grabbing = Int32.Parse(d[7]);
			}

		}
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Called For the Client's Player
			// Only Use To Update Player Specific Stuff (not involving not-this-player variables)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			if (colorID == colorDefault)
				colorID = ID; // Remove this when added picking colour at start-up

			// Controls
			/*
			if (k.IsKeyDown(Keys.W))
				Pos.Y -= 3;
			if (k.IsKeyDown(Keys.S))
				Pos.Y += 3;
			if (k.IsKeyDown(Keys.A))
				Pos.X -= 3;
			if (k.IsKeyDown(Keys.D))
				Pos.X += 3;
				*/
			if (k.IsKeyDown(Keys.P))
			{
				if (canGrab)
				{
					Pos.Y = 320;
					Pos.X = 80 + 120 * id;
					Grabbing = -1;
				}
			}
			//Mouse Rotation
			// Unlocked Screen: rotation = RotateTo(Pos, new Vector2(m.X, m.Y));
			rotation = RotateTo(Game1.screenCenter, new Vector2(m.X, m.Y));

		}
		public Vector2 drawTo;
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			if (active)
			{
				// Draw Command = Texture, DrawPos, SourceRec, DestinationRec, Center, rotation, scale, Color...
				// Pos for unlocked
				s.Draw(t, drawPos, null, null, new Vector2(t.Width / 2, t.Height / 2)
					   , (float)rotation, new Vector2(0.1f), Colours[this.ID], SpriteEffects.None, 1);
				//if (drawLine)
				//	this.DrawLine(ref s, drawTo, Color.Black);
			}
		}
	}
}
