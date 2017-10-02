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
			return BooltoNum(this.active) + " " + Name + " " + x.ToString() + " " + y.ToString()
																+ " " + colorID.ToString() + " " + rotation.ToString();
		}
		public static string BooltoNum(bool b)
		{
			if (b)
				return "1";
			return "0";
		}
	}
}
