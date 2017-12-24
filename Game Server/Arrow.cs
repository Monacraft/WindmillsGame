using System;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;

namespace Server
{
	//x,y,vx,vy
	public class Arrow
	{
		public bool fired;
		public int id;
		public double x, y, vx, vy;

		public Arrow(int id, float px, float py, float pvx, float pvy)
		{
			fired = true; // don't really need in new implementation;
			x = px;
			y = py;
			vx = pvx;
			vy = pvy;
		}

		//Change this value to change how fast things fall to all planets
		public double PLANET_DENSITY = 0.00001; //Technically also includes gravitational constant as a factor

		public void Update()
		{
			double ax = 0;
			double ay = 0;

			foreach (var p in MainClass.gameData.Planets)
			{
				double radius = Math.Sqrt(Math.Pow(x - p.x, 2) + Math.Pow(x - p.x, 2));

				//Assumes planets have a mass distribution like a sphere:
				double mass = PLANET_DENSITY  * 4.0 / 3 * Math.PI * Math.Pow(p.size / 2.0, 3);
				//The final 3 here determines how much being bigger means you are more attractive.

				//The 2's here determine how much being further away means you are less attracted.
				//Having a 2 is realistic.
				ax += mass * 1 / Math.Pow(radius, 2) * (p.x - x) / radius;
				ay += mass * 1 / Math.Pow(radius, 2) * (p.y - y) / radius;
			}

			vx += ax;
			vy += ay;

			x += vx;
			y += vy;
			Console.WriteLine("{0} {1} {2} {3}", x, y, vx, vy);
		}
		//Call the following on each player each tick. Use (x,y) as player position and (radius) as the radius of the player.
		public int Collision(double radius)
		{
			foreach (Player p in MainClass.gameData.Players)
			{
				Vector2 v = PlanetPos(p.ID);
				float px = v.x;
				float py = v.y;

				//Checks arrow collision with a circle center (x,y) radius (radius)
				if (Math.Sqrt(Math.Pow(x - px, 2) + Math.Pow(y - py, 2)) < radius)
				{
					if (id != p.ID)
					{
						return p.ID;
					}
				}
			}
			return -1;
		}
		public static Vector2 PlanetPos(int PlanetID)
		{
			float angle = (float)(MainClass.gameData.Players[PlanetID].bearing + Math.PI / 2);
			return new Vector2(MainClass.gameData.Planets[PlanetID].x + (float)(Math.Cos(angle) * (MainClass.gameData.Planets[PlanetID].size / 2)),
							   MainClass.gameData.Planets[PlanetID].y + (float)(Math.Sin(angle) * (MainClass.gameData.Planets[PlanetID].size / 2)));
		}
		public string Write()
		{
			string s = "";
			s += id.ToString() + " " + x.ToString() + " " +  y.ToString() + " ";
			double r = Math.Atan2(vy, vx);
			s += r;
			return s;
		}
	}
	public partial class Data
	{
		public List<Arrow> FiredArrows;

		public void ArrowUpdate()
		{
			foreach (var arrow in FiredArrows)
			{
				arrow.Update();
				int killplayer = arrow.Collision(10);
				if (killplayer > -1)
				{
					Players[killplayer].active = 2; // Will this work??? WHO KNOWS :O
				}

			}
		}
		public void FireArrow(int ID)
		{
			Vector2 pos = Arrow.PlanetPos(ID);
			Console.WriteLine("{0} {1} {2} {3} {4}", ID, pos.x, pos.y, (Players[ID].x - pos.x)/20, (Players[ID].y - pos.y)/20);
			FiredArrows.Add(new Arrow(ID, pos.x, pos.y, (Players[ID].x - pos.x)/20, (Players[ID].y - pos.y)/20));
		}
	}
	public struct Vector2
	{
		public float x;
		public float y;
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	}
}

