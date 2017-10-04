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
		public static Color[] Colours = {
			Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Yellow, Color.Purple, Color.Black};
		public double rotation;

		public Player()
		{
			Pos = new Vector2();
			active = false;
			ID = -1;
			rotation = 0;
			colorDefault = 6;
			colorID = 6;
		}
		public void UpdateOthers(string[] d) 
		{
			/// **************
			/// Only Called for OTHER Players
			/// Protocol for Player Information:
			/// 
			/// Planned: ID Active Name xPos yPos Colour Rotation Grabbing GrabbedBy 
			/// Current: ID Active Name xPos yPos Colour Rotation
			/// 
			/// **************
			Pos.X = float.Parse(d[3], CultureInfo.InvariantCulture);
			Pos.Y = float.Parse(d[4], CultureInfo.InvariantCulture);
			this.Name = d[2];
			this.colorID = Int32.Parse(d[5]);
			this.rotation = Double.Parse(d[6]);
		}
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState om)
		{
			// Only Called For the Client's Player
			// Only Use To Update Player Specific Stuff (not involving not-this-player variables)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			if (colorID == colorDefault)
				colorID = ID; // Remove this when added picking colour at start-up

			// Controls
			if (k.IsKeyDown(Keys.W))
				Pos.Y -= 3;
			if (k.IsKeyDown(Keys.S))
				Pos.Y += 3;
			if (k.IsKeyDown(Keys.A))
				Pos.X -= 3;
			if (k.IsKeyDown(Keys.D))
				Pos.X += 3;

			//Mouse Rotation
			rotation = RotateTo(Pos, new Vector2(m.X, m.Y));

			drawTo = new Vector2(m.X, m.Y);
			if (m.LeftButton == ButtonState.Pressed)
				drawLine = true;
			else
				drawLine = false;
		}
		public bool drawLine;
		public Vector2 drawTo;
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			// Draw Command = Texture, DrawPos, SourceRec, DestinationRec, Center, rotation, scale, Color...
			s.Draw(t, Pos, null, null, new Vector2(t.Width / 2, t.Height / 2)
				   , (float)rotation, new Vector2(0.1f), Colours[this.ID], SpriteEffects.None, 1);
			if (drawLine)
				this.DrawLine(ref s, drawTo);
		}
	}
}
