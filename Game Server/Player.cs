using System;
using System.Globalization;

namespace Server
{
	public partial class Player
	{
		public float x;
		public float y;		// MOUSE POSITION
		public string Name;
		public int ID;
		public int active;  // 0 = DC, 1=Alive, 2=Dead
		public double bearing;
		public int plannet; // for now, plannet = id (similar to colour)
		public bool Revive;
		public bool DownloadedLevel;

		public Player()
		{
			ID = -1;
			bearing = 0;
			active = 1;
			DownloadedLevel = false;
		}
		public void UpdateThis(string pdat)
		{
			/// **************
			/// Protocol for Player Information:
			/// 
			///          0  1      2    3    4    5       6       7        8
			/// Planned: ID Active Name xPos yPos Plannet Bearing 
			/// Current: ID Active Name xPos yPos Plannet Bearing
			/// 
			/// **************
			string[] d = pdat.Split(' ');
			// x and y refer to mouse position (for aiming arrow)
			active = int.Parse(d[1]);
			if (active == 2)
			{
				if(Revive)
					active = 1;
			}
			else
			{
				if (active == 1)
				{
					Revive = false;
				}
			}
			x = float.Parse(d[3], CultureInfo.InvariantCulture);
			y = float.Parse(d[4], CultureInfo.InvariantCulture);
			this.Name = d[2];
			this.bearing = double.Parse(d[6]);
			//this.rotation = Double.Parse(d[6]);
		}
		public string ClientSend()
		{
			return ID.ToString() + " " + this.String();
		}
		public string String()
		{
			// ID[covered-in-data] Active Name xPos yPos
			return active + " " + Name + " " + x.ToString() + " " + y.ToString() + " " + ID.ToString() + " " + bearing.ToString();
		}
		public static string BooltoNum(bool b)
		{
			if (b)
				return "1";
			return "0";
		}
	}
}
