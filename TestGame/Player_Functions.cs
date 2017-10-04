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
			double r;
			if (ydiff > 0)
			{
				if (xdiff > 0)
				{
					//Console.WriteLine("Bottom Right, {0} from {1},{2}", Math.Atan2(ya, xa) * 180 / Math.PI, xa, ya);
					r = Math.Atan2(ya, xa) + Math.PI/2;
				}
				else
				{
					//Console.WriteLine("Bottom Left, {0} from {1},{2}", Math.Atan2(ya, xa) * 180 / Math.PI, xa, ya);
					r = 3 * Math.PI / 2 - Math.Atan2(ya, xa);
				}
			}
			else
			{
				if (towardsthis.X > rotatethis.X)
				{
					//Console.WriteLine("Top Right, {0} from {1},{2}", Math.Atan2(ya, xa) * 180 / Math.PI, xa, ya);
					r = Math.PI /2 - Math.Atan2(ya, xa);
				}
				else
				{
					//Console.WriteLine("Top Left, {0} from {1},{2}", Math.Atan2(ya, xa) * 180 / Math.PI, xa, ya);
					r = 3 * Math.PI / 2 + Math.Atan2(ya, xa);
				}
			}
			Console.WriteLine((r * (180/Math.PI)).ToString());
			return r;
		}
	}
}
