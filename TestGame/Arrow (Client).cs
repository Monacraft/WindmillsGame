using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Arrow
	{
		public bool Fired;
		public float x;
		public float y;
		public int ID;
		public float rotation;
		public static Texture2D tex;

		public Arrow()
		{
			x = 0;
			y = 0;
			rotation = 0;
			ID = -1;
			Fired = false;
		}
		public void ArrowRead(string pdat)
		{
			string[] d = pdat.Split(' ');
			ID = int.Parse(d[0]);
			x = (float)double.Parse(d[1]);
			y = (float)double.Parse(d[2]);
			rotation = (float)double.Parse(d[3]);

			Fired = true;
			Console.WriteLine("Arrow {0} at {1}, {2}", ID, x, y);
		}
		public void Draw(ref SpriteBatch s)
		{
			if (ID != -1)
			{
				s.Draw(tex, new Vector2(x, y), null, Color.White, (float)(rotation + Math.PI),
					   new Vector2(350f, 350f), new Vector2(128f / 700f), SpriteEffects.None, 1);
			}
		}
	}
}
