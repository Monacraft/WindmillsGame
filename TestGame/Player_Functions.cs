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
			this.ID = ID;
			planet = ID;
			Console.WriteLine(this.ID.ToString());
			active = 1;
			Kill = false;
		}
		public string ClientSend()
		{
			string s = ID.ToString() + " " + this.String();
			if (FireArrow == 1)
				FireArrow = 0;
			return s;
		}
		public string String()
		{
			// ID[covered-in-data] .....
			return active.ToString() + " " + Name + " " + Pos.X.ToString() + " " + Pos.Y.ToString()
				         + " " + planet.ToString() + " " + bearing.ToString() + " " + FireArrow.ToString();
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
		public void DrawLine(ref SpriteBatch s, Vector2 To, Color c)
		{
			Vector2 usePos = Pos;
			Vector2 edge = To - usePos;
			float angle = (float)Math.Atan2(edge.Y, edge.X);
	       	s.Draw(Game1.pixel, new Rectangle((int) usePos.X, (int) usePos.Y, (int)edge.Length(), 2), 
			       null, c, angle, Vector2.Zero,
			              SpriteEffects.None, 1);
		}
		public Vector2 PlanetPos(int PlanetID)
		{
			float angle = (float)(bearing+Math.PI/2);
			return new Vector2(Game1.GameData.Planets[PlanetID].x + (float)(Math.Cos(angle) * (Game1.GameData.Planets[PlanetID].size/2)),
			                   Game1.GameData.Planets[PlanetID].y + (float)(Math.Sin(angle) * (Game1.GameData.Planets[PlanetID].size/2)));
		}
	}
}
