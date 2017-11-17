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

		// All for level generation
		public static int screenh = 640;
		public static int screenw = 640;
		public static float radius = 128f; // Original Texture
		public static int minsize = 32;
		public static int maxsize = 128; // Final drawing
		public static int gridsize = 4; // 4x4

		public Planet(float xp, float yp)
		{
			x = xp;
			y = yp;
			Random r = new Random();
			size = r.Next(minsize, maxsize);
			// Colour = player ids for now
		}
		public Planet(float xp, float yp, int size)
		{
			x = xp;
			y = yp;
			this.size = size;
		}
		public static Planet[] GenerateLevel(int number)
		{
			Planet[] p = new Planet[number];			
			Random r = new Random();
			int[] planets = new int[number];
			List<int> places = new List<int>();
			// if number > 16 well fuck
			for (int i = 0; i < gridsize*gridsize; i++)
			{
				places.Add(i);
			}
			for (int i = 0; i < number; i++)
			{
				int j = r.Next(0, places.Count);
				planets[i] = places[j];
				places.RemoveAt(j);
			}

			for (int i = 0; i < number; i++)
			{
				int xP = planets[i] % gridsize;
				int yP = (planets[i] - (planets[i] % gridsize)) / gridsize;
				int xR = xP * (screenw / gridsize) + maxsize; // LOL WORKS PERFECTLY WITH 640!!!!
				int yR = yP * (screenh / gridsize) + maxsize;
				p[i] = new Planet(xR, yR);
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
