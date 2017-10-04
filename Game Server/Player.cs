using System;
namespace Server
{
	public class Player
	{
		public float x;
		public float y;
		public bool active;
		public string Name;
		public int ID;
		// Other variables like colour, rotation

		public Player()
		{
			x = 0;
			y = 0;
			active = false;
			ID = -1;
		}
		public void Activate(string N, int ID)
		{
			Name = N;
			active = true;
			this.ID = ID;
		}
		public void UpdateThis(float px, float py, string pName)
		{
			this.x = px;
			this.y = py;
			this.Name = pName;
			/*if (this.active && !pactive)
				Console.WriteLine(this.Name + "  disconnected");
			if (!this.active && pactive)
				Console.WriteLine(this.Name + "  reconnected");
			this.active = pactive;*/
		}
		public string ClientSend()
		{
			return ID.ToString() + " " + this.String();
		}
		public string String()
		{
			// ID[covered-in-data] Active Name xPos yPos
			return BooltoNum(this.active) + " " + Name + " " + x.ToString() + " " + y.ToString();
		}
		public static string BooltoNum(bool b)
		{
			if (b)
				return "1";
			return "0";
		}
	}
}
