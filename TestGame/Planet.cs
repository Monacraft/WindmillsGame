using System;
using System.Collections.Generic;
using System.Globalization;

namespace TestGame
{
	public class Planet
	{
		public float x;
		public float y;
		public int Colour;
		public int size;
		public bool real;

		// All for level generation
		public static int screenh = 640;
		public static int screenw = 640;
		public static float radius = 128f; // Original Texture
		public static int minsize = 32;
		public static int maxsize = 128; // Final drawing
		public static int gridsize = 4; // 4x4

		// For client side Planet Parsing
		public static int maxPlanets = 16;
		public static int currPlanets;

		public Planet()
		{
			real = false; // RIP you will be remembered
		}
		public Planet(float xp, float yp)
		{
			x = xp;
			y = yp;
			size = maxsize;
			real = true;
			// Colour = player ids for now
		}
		public Planet(float xp, float yp, int size)
		{
			x = xp;
			y = yp;
			this.size = size;
			real = true;
		}
		public static Planet[] GenerateLevel(int number)
		{
			Planet[] p = new Planet[number];
			Random r = new Random();
			int[] planets = new int[number];
			List<int> places = new List<int>();
			// if number > 16 well fuck
			for (int i = 0; i < gridsize * gridsize; i++)
			{
				places.Add(i);
			}
			for (int i = 0; i < number; i++)
			{
				int j = r.Next(0, places.Count);
				planets[i] = places[j];
				places.RemoveAt(j);
			}

			Console.WriteLine("Level Generated:");
			int gridBLOCK = 160;
			for (int i = 0; i < number; i++)
			{
				int xP = planets[i] % gridsize;
				int yP = (planets[i] - (planets[i] % gridsize)) / gridsize;
				int xR = (xP * gridBLOCK) + 80; // Need to automate padding
				int yR = (yP * gridBLOCK) + 80;
				int size = r.Next(minsize, maxsize);
				float xM = r.Next(xR - (gridBLOCK / 2 - size / 2) + 16, xR + (gridBLOCK / 2 - size / 2) - 16);
				float yM = r.Next(yR - (gridBLOCK / 2 - size / 2) + 16, yR + (gridBLOCK / 2 - size / 2) - 16);
				// For above:
				// 11 is to account for player size
				// technically i should be 16, but current set up has too small gridBlocks
				Console.WriteLine("{0}, {1}", xM, yM);
				p[i] = new Planet(xM, yM, size);
			}
			return p;
		}
		public string Write()
		{
			return "" + x.ToString() + "," + y.ToString() + "," + size.ToString();
		}
		public static Planet Read(string s)
		{
			string[] p = s.Split(',');
			return new Planet(float.Parse(p[0], CultureInfo.InvariantCulture), float.Parse(p[1], CultureInfo.InvariantCulture), int.Parse(p[2]));
		}
	}
}
