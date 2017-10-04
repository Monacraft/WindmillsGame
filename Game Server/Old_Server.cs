using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Server
{
	public class AsynchronousSocketListener
	{
		// Thread signal.  
		public static ManualResetEvent allDone = new ManualResetEvent(false);
		public static ManualResetEvent stayconnected = new ManualResetEvent(false);

		public AsynchronousSocketListener()
		{
		}
		//public static bool open;
		public static void StartListening()
		{
			// Data buffer for incoming data.  
			byte[] bytes = new Byte[1024];

			// Establish the local endpoint for the socket.  
			// The DNS name of the computer  
			// running the listener is "host.contoso.com".  
			IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

			// Create a TCP/IP socket.  
			Socket listener = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.  
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);
				while (true)
				{
					// Set the event to nonsignaled state.  
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.  
					//Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

					// Wait until a connection is made before continuing.  
					allDone.WaitOne();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}

		public static void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.  
			allDone.Set();
			//stayconnected.Reset();
			// Get the socket that handles the client request.  
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.  
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
			//stayconnected.WaitOne();
			listener = (Socket)ar.AsyncState;
			listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);
		}
		public static string[] players;

		public static void ReadCallback(IAsyncResult ar)
		{
			String content = String.Empty;

			// Retrieve the state object and the handler socket  
			// from the asynchronous state object.  
			StateObject state = (StateObject)ar.AsyncState;
			Socket handler = state.workSocket;

			//stayconnected.Set();
			// Read data from the client socket.   
			int bytesRead = handler.EndReceive(ar);

			if (bytesRead > 0)
			{
				// There  might be more data, so store the data received so far.  
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer, 0, bytesRead));

				// Check for end-of-file tag. If it is not there, read   
				// more data.  
				content = state.sb.ToString();
				if (content.IndexOf("<EOF>") > -1)
				{
					// All the data has been read from the   
					// client. Display it on the console.  
					//Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",content.Length, content);
					/*int IDtosend = MainClass.GameData.ServerRecieve(content.Substring(0, (content.Length - 5)));
					// Echo the data back to the client.  
					string status = MainClass.GameData.ServerSend(IDtosend);*/
					int SenderID = MainClass.thisGame.Recieve(content.Substring(0, (content.Length - 5)));
					MainClass.thisGame.Gravity();
					string status = MainClass.thisGame.Send(SenderID);
					//Console.WriteLine("Sending back");
					Send(handler, status);

					//stayconnected.Set();
				}
				else
				{
					// Not all data received. Get more.  
					handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,new AsyncCallback(ReadCallback), state);
				}
			}
		}

		private static void Send(Socket handler, String data)
		{
			// Convert the string data to byte data using ASCII encoding.  
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.  
			//StateObject state = new StateObject();
			//state.workSocket = handler;
			handler.BeginSend(byteData, 0, byteData.Length, 0,new AsyncCallback(SendCallback), handler);
			//handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,new AsyncCallback(ReadCallback), state);
		}

		private static void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.  
				Socket handler = (Socket)ar.AsyncState;
				//Console.WriteLine("Here");
				// Complete sending the data to the remote device.  
				int bytesSent = handler.EndSend(ar);
				//Console.WriteLine("Sent {0} bytes to client.", bytesSent);
				//handler.EndAccept(ar);
				//Socket listener = (Socket)ar.AsyncState;
				//listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);
				//handler.Shutdown(SocketShutdown.Send);
				//StateObject state = new StateObject();
				//handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,new AsyncCallback(ReadCallback), state);
				//handler.Close();
				Socket listener = (Socket)ar.AsyncState;
				//listener.Listen(100);
				listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
	public class StateObject
	{
		// Client socket.  
		public Socket workSocket = null;
		// Size of receive buffer.  
		public const int BufferSize = 256;
		// Receive buffer.  
		public byte[] buffer = new byte[BufferSize];
		// Received data string.  
		public StringBuilder sb = new StringBuilder();	}
}