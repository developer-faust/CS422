using System;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net;
using System.Collections.Concurrent;

namespace CS422
{
	public static class WebServer
	{
		private static int _NumOfThreads;
		private static Thread _ListeningThread;
		private static TcpListener _Listener;
		public static List<Thread> _Pool { get; } = new List<Thread>();
		private static readonly BlockingCollection<TcpClient> _ClientsCollection =
			new BlockingCollection<TcpClient> ();
		 

		public static void Start(int port, ushort threadsCount)
		{ 
			if (_ListeningThread != null)
			{
				throw new InvalidOperationException ("Server is running");
			} 

			_NumOfThreads = threadsCount;

			_ListeningThread = new Thread (new ParameterizedThreadStart (ListenThreadWork));
			_ListeningThread.Start (port); 

			// Initialize all threads to wait a client connection
			for (int i = 0; i < threadsCount; i++) 
			{

				// Create a new thread and add it to the list of threads
				Thread t = new Thread (ClientThreadWork);
				t.Start ( );
				_Pool.Add (t);
			}
		}
		 
		private static void ClientThreadWork( )
		{

			// Do Work While true
			while (true)
			{ 
				Console.WriteLine ("Doing Work");

				TcpClient client = _ClientsCollection.Take ();

				// No Clients
				if (client == null)
				{
					break;
				}

				Request request = CreateRequest (client); 


			}

		}

		private static Request CreateRequest(TcpClient client)
		{
			throw new NotImplementedException ();

		}
		private static void ListenThreadWork(object obj)
		{
			int port = (int)obj; 
			Console.WriteLine ("Listening on port {0}", port);

			TcpClient client;
			TcpListener listener = new TcpListener (IPAddress.Any, port);

			listener.Start ();  
			 
			while (true)
			{
				try 
				{
					client = listener.AcceptTcpClient();
					_ClientsCollection.Add(client);

					
				} catch (SocketException ex) {
					break;
				}

			}

		}



	}
}

