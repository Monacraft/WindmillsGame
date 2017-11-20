using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace Server
{

	class TcpServer
	{
		private TcpListener server;
		private Boolean isRunning;

		public TcpServer(int port)
		{
			server = new TcpListener(IPAddress.Any, port);
			server.Start();

			isRunning = true;

			LoopClients();
		}

		public void LoopClients()
		{
			while (isRunning)
			{
				// wait for client connection
				TcpClient newClient = server.AcceptTcpClient();
				Console.WriteLine("New Client");
				// client found.
				// create a thread to handle communication
				Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
				t.Start(newClient);
			}
		}
		//public ManualResetEvent update;
		public void HandleClient(object obj)
		{
			// retrieve client from parameter passed to thread
			TcpClient client = (TcpClient)obj;

			// sets two streams
			StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
			StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
			// you could use the NetworkStream to read and write, 
			// but there is no forcing flush, even when requested

			Boolean bClientConnected = true;
			String sData = null;
			//update.Reset();
			while (bClientConnected)
			{
				// reads from stream
				sData = sReader.ReadLine();

				// shows content on the console.
				//Console.WriteLine("Recieved: " + sData);
				if (sData.Split(' ')[0] == "DISCONNECT")
				{
					MainClass.gameData.DisconnectRecieve(sData);
					bClientConnected = false;
				}
				else
				{
					int SenderID = MainClass.gameData.Recieve(sData);

					string status = MainClass.gameData.ServerSend(SenderID);

					sWriter.WriteLine(status);
					sWriter.Flush();
				}
			}
		}
	}
}