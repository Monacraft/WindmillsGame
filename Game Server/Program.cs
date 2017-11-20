using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
	class MainClass
	{
		//public static string v;
		//public static Data GameData;
		public static GameState thisGame;
		public static Data gameData;
		//public static int MyID;
		public static void Main(string[] args)
		{
			//GameData = new Data();
			//thisGame = new GameState("My Server");
			gameData = new Data("My Server", "");
			Console.WriteLine("Starting Server:");
			Server();
			return;
		}

		public static void Server()
		{
			//Console.WriteLine("Starting Server");
			gameData.state = 1;
			TcpServer t = new TcpServer(11000);
			//t.players = new string[32];
			//AsynchronousSocketListener.StartListening();

			Console.ReadLine();
		}
	}
}
