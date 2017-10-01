using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
	public class Player
	{
		public float x;
		public float y;
		public bool active;
		public string Name;
		public int ID;
		public int colID;
		public static Color[] Colours = {
			Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Yellow, Color.Purple, Color.Black};
		public double rotation;
		// Other variables like colour, rotation

		public Player()
		{
			x = 0;
			y = 0;
			active = false;
			ID = -1;
			rotation = 0;
			colID = 6;
		}
		public void Activate(string N, int ID)
		{
			Name = N;
			active = true;
			this.ID = ID;
			if (this.ID >= 0)
			{
				colID = ID;
			}
		}
		public void UpdateThis(string[] d) // Old Param: float px, float py, string pName)
		{
			/// **************
			/// Protocol for Player Information:
			/// 
			/// Planned: ID Active Name xPos yPos Colour Rotation Grabbing GrabbedBy 
			/// Current: ID Active Name xPos yPos Colour Rotation
			/// 
			/// **************
            this.x = float.Parse(d[3], CultureInfo.InvariantCulture);
            this.y = float.Parse(d[4], CultureInfo.InvariantCulture);
			this.Name = d[2];
			this.colID = Int32.Parse(d[5]);
			this.rotation = Double.Parse(d[6]);
		}
		public string ClientSend()
		{
			return ID.ToString() + " " + this.String();
		}
		public string String()
		{
			// ID[covered-in-data] .....
			return BooltoNum(this.active) + " " + Name + " " + x.ToString() + " " + y.ToString()
				                                                + " " + colID.ToString() + " " + rotation.ToString();
		}
		public static string BooltoNum(bool b)
		{
			if (b)
				return "1";
			return "0";
		}
		public void Update()
		{
			
		}
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			s.Draw(t, new Vector2(x, y),null, null, new Vector2(t.Width/2, t.Height/2)
			       , (float)rotation, new Vector2(0.1f), Colours[colID],SpriteEffects.None, 1);
		}
	}
}
