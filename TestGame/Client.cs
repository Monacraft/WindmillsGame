using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace TestGame
{
	class Client
	{
		private TcpClient client;

		private StreamReader sReader;
		private StreamWriter sWriter;
		public Boolean isConnected;

		public Client(String ipAddress, int portNum)
		{
			client = new TcpClient();
			client.Connect(ipAddress, portNum);
			Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
			t.Start(client);
		}
		public void HandleClient(object obj)
		{
			TcpClient server = (TcpClient)obj;
			sReader = new StreamReader(server.GetStream(), Encoding.ASCII);
			sWriter = new StreamWriter(server.GetStream(), Encoding.ASCII);
			isConnected = true;
			while (isConnected)
			{
				string toSend = "";
				if (Game1.MyID == -1)
				{
					toSend = Game1.myPlayer.ClientSend();
					Console.WriteLine("FIRST SET UP");
				}
				else
				{
					toSend = Game1.GameData.Players[Game1.MyID].ClientSend();
				}
				if (Game1.disconnect)
				{
					toSend = "DISCONNECT " + Game1.MyID.ToString();
					isConnected = false;
				}
				sWriter.WriteLine(toSend);
				sWriter.Flush();
				if (isConnected)
				{
					string received = sReader.ReadLine();
					Game1.MyID = Game1.GameData.ClientRecieve(received);
				}
			}
			Game1.canexit = true;
			/*
			sWriter.WriteLine("DISCONNECT"); // To Add to server disconnect handling
			sWriter.Flush();*/
		}
	}
}
