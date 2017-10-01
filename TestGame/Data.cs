using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class Data
	{
		public int state; // 0 = Not connected, 1 = Menu, 2 = Game
		public int playercount;
		public int MenuInfo;
		public Player[] Players;
		public Color[] colours;
		public Data()
		{
			state = 0;
			playercount = 0;
			Players = new Player[4];
			for (int i = 0; i < 4; i++)
			{
				this.Players[i] = new Player();
			}
			colours = new Color[4];
			colours[0] = Color.Black;
			colours[1] = Color.White;
			colours[2] = Color.Red;
			colours[3] = Color.Green;
		}
		public int ClientRecieve(string dat)
		{
			// State|MenuInfo|MyID|Players|[P1=P2=P3=P4]
			string[] d = dat.Split('|');
			//Console.WriteLine(dat);
			Int32.TryParse(d[0], out state);
			int id = Int32.Parse(d[2]);
			if (state == 0)
			{
				Console.WriteLine("Server State 0");
				return -1;
			}
			if (state == 1)
			{
				// Menu
				MenuInfo = Int32.Parse(d[1]);
				int P = Int32.Parse(d[3]);
				playercount = P;
				foreach (var playerdat in d[4].Split('='))
				{
					this.ParsePlayer(playerdat);
				}
			}
			return id;
		}
		public int ParsePlayer(string pdat)
		{
			// ID Active Name xPos yPos [Other Information]
			string[] ds = pdat.Split(' ');
			int id = Int32.Parse(ds[0]);
			if (id == -1)
			{
				id = playercount;
				playercount++;
			}
			if (!Players[id].active)
			{
				if (ds[1] == "1")
				{
					Players[id].Activate(ds[2], id);
				}
			}
			if (Players[id].active)
			{
				if (id != Game1.MyID)
				{

					Players[id].UpdateThis(ds);
					// Old Param: float.Parse(ds[3], CultureInfo.InvariantCulture), float.Parse(ds[4], CultureInfo.InvariantCulture), ds[2]);
				}
			}
			return id;
		}
		public void Update(ref GameTime gametime, ref KeyboardState k, ref MouseState m, int id)
		{
			if (k.IsKeyDown(Keys.W))
				Players[id].y -= 3;
			if (k.IsKeyDown(Keys.S))
				Players[id].y += 3;
			if (k.IsKeyDown(Keys.A))
				Players[id].x -= 3;
			if (k.IsKeyDown(Keys.D))
				Players[id].x += 3;
			if (m.X == Players[id].x)
			{
				if (m.Y < Players[id].y)
					Players[id].rotation = Math.PI;
				else
					Players[id].rotation = 0;
			}
			else
			{
				Players[id].rotation = Math.Atan((m.Y - Players[id].y) / (m.X - Players[id].x));
			}
		}
		public void Draw(ref SpriteBatch s, ref Texture2D t)
		{
			for (int i = 0; i < playercount; i++)
			{
				Players[i].Draw(ref s, ref t);
			}
		}
	}
}
