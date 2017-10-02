using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Player
	{
		public float x;
		public float y;
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
			x = 0;
			y = 0;
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
			this.x = float.Parse(d[3], CultureInfo.InvariantCulture);
			this.y = float.Parse(d[4], CultureInfo.InvariantCulture);
			this.Name = d[2];
			this.colorID = Int32.Parse(d[5]);
			this.rotation = Double.Parse(d[6]);
		}
		public void Update(int id, ref KeyboardState k, ref MouseState m, ref KeyboardState ok, ref MouseState o)
		{
			// Only Called For the Client's Player
			// Only Use To Update Player Specific Stuff (not involving not-this-player variables)
			// Params: ID = Client's ID, k/m = Keyboard and Mouse States, ok/om = States 1-tick ago

			if (colorID == colorDefault)
				colorID = ID; // Remove this when added picking colour at start-up

			// Controls
			if (k.IsKeyDown(Keys.W))
				this.y -= 3;
			if (k.IsKeyDown(Keys.S))
				this.y += 3;
			if (k.IsKeyDown(Keys.A))
				this.x -= 3;
			if (k.IsKeyDown(Keys.D))
				this.x += 3;
			
			//Mouse Rotation
			if (m.X == x)
			{
				if (m.Y > this.y)
					this.rotation = Math.PI;
				else
					this.rotation = 0;
			}
			else
			{
				// ToDo: Fix This Code To Calculate Rotation Angle (I need to use Pi's)
				rotation = Math.Atan2((m.Y - y) , (m.X - x));
			}
		}
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			// Draw Command = Texture, DrawPos, SourceRec, DestinationRec, Center, rotation, scale, Color...
			s.Draw(t, new Vector2(x, y), null, null, new Vector2(t.Width / 2, t.Height / 2)
				   , (float)rotation, new Vector2(0.1f), Colours[this.ID], SpriteEffects.None, 1);
		}
	}
}
