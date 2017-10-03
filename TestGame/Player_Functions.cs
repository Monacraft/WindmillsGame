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
			double r = Math.Atan2((towardsthis.Y - rotatethis.Y) , (towardsthis.X - rotatethis.X));
			if (r < 0)
				r = (Math.PI * 2) - r + Math.PI/2;
			/*if (towardsthis.X < rotatethis.X)
			{
				if (towardsthis.Y <= rotatethis.Y)
				{
					// Quadrant 2
					r += Math.PI ; 
				}
				else
				{
					// Quadrant 3
					r = Math.PI / 2 - r;
				}
			}
			else
			{
				if (towardsthis.Y <= rotatethis.Y)
				{
					// Quadrant 1
					r = -r;
				}
				else
				{
					// Quadrant 4
					r = Math.PI/2 - r;
				}
			}		*/	
			return r;
		}
	}
}
