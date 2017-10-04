using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public partial class Player
	{
		public void Activate(string N, int ID)
		{
			Name = N;
			active = true;
			this.ID = ID;
			Console.WriteLine(this.ID.ToString());
			if (this.ID >= 0)
			{
				colorID = this.ID;
			}
		}
		public string ClientSend()
		{
			return ID.ToString() + " " + this.String();
		}
		public string String()
		{
			// ID[covered-in-data] .....
			return BooltoNum(this.active) + " " + Name + " " + Pos.X.ToString() + " " + Pos.Y.ToString()
																+ " " + colorID.ToString() + " " + rotation.ToString();
		}
		public static string BooltoNum(bool b)
		{
			if (b)
				return "1";
			return "0";
		}
		public static double RotateTo(Vector2 rotatethis, Vector2 towardsthis)
		{
			float ydiff = towardsthis.Y - rotatethis.Y;
			float xdiff = towardsthis.X - rotatethis.X;
			float ya = Math.Abs(ydiff);
			float xa = Math.Abs(xdiff);
			double r = Math.Atan2(ya, xa);
			if (ydiff > 0)
			{
				if (xdiff > 0)
				{
					// Bottom Right
					r = Math.PI / 2 + r;
				}
				else
				{
					r = Math.PI * 3 / 2 - r;
				}
			}
			else
			{
				if (xdiff > 0 )
				{
					// Top Right
					r = Math.PI/2 - r ;
				}
				else
				{
					r = Math.PI * 3 /2 + r;
				}
			}
			return r;
		}
		public void DrawLine(ref SpriteBatch s, Vector2 To)
		{
			Vector2 edge = To - Pos;
			float angle = (float)Math.Atan2(edge.Y, edge.X);
	       	s.Draw(Game1.pixel, new Rectangle((int) Pos.X, (int) Pos.Y, (int)edge.Length(), 1), 
			       null, Color.Black, angle, Vector2.Zero,
			              SpriteEffects.None, 1);
		}
	}
}
