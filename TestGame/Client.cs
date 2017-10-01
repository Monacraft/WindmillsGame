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
		private TcpClient _client;

		private StreamReader _sReader;
		private StreamWriter _sWriter;

		private Boolean _isConnected;

		public Client(String ipAddress, int portNum)
		{
			_client = new TcpClient();
			_client.Connect(ipAddress, portNum);
			Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
			t.Start(_client);
		}
		public void HandleClient(object obj)
		{
			TcpClient client = (TcpClient)obj;
			_sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
			_sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
			_isConnected = true;
			//String sData = null;
			while (_isConnected)
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

				//Console.WriteLine("Sending: " + toSend);
				_sWriter.WriteLine(toSend);
				_sWriter.Flush();
				//Console.WriteLine(Game1.MyID.ToString());
				string received = _sReader.ReadLine();
				//Console.WriteLine("Received: " + received);
				Game1.MyID = Game1.GameData.ClientRecieve(received);

			}
		}
	}
}
