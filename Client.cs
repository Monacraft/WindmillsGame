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

			HandleCommunication();
		}

		public void HandleCommunication()
		{
			_sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
			_sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

			_isConnected = true;
			String sData = null;
			while (_isConnected)
			{
				//Console.Write("&gt; ");
				//sData = Console.ReadLine();

				// write data and make sure to flush, or the buffer will continue to 
				// grow, and your data might not be sent when you want it, and will
				// only be sent once the buffer is filled.
				string toSend = "";
				if (Game1.MyID == -1)
					toSend = Game1.myPlayer.ClientSend();
				else
					toSend = Game1.GameData.Players[Game1.MyID].ClientSend();
				
				Console.WriteLine("Sending: " + toSend);
				_sWriter.WriteLine(toSend);
				_sWriter.Flush();

				string received = _sReader.ReadLine();
				Console.WriteLine("Received: " + received);
				Game1.MyID = Game1.GameData.ClientRecieve(received);
				// if you want to receive anything
				// String sDataIncomming = _sReader.ReadLine();
			}
		}
	}
}
