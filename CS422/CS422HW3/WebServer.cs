using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace CS422
{
	public class WebServer
	{
		private static int _port;
		private static bool _running = false;
		private static TcpListener listener;
		public const string Name = "CS422 WebServer";

		private const string DefaultTemplate = 
			"HTTP/1.1 200 OK\r\n" +
			"Content-Type:text/html\r\n" +
			"\r\n\r\n" +
			"\r\n\r\n" +
			"<html>ID Number:{0}<br>" +
			"DateTime.Now:{1}<br>" +
			"Requested URL:{2}</html>";

		public WebServer ()
		{
		}


		public static bool Start (int port, string responseTemplate)
		{ 
			listener = new TcpListener (IPAddress.Any, port); 
			listener.Start ();

			_running = true;

			while (_running) {
				Console.WriteLine ("Waiting for connection...");
				TcpClient client = listener.AcceptTcpClient ();

				Console.WriteLine ("Client connected!");
				HandleClient (client);
				client.Close ();

			}

			_running = false;

			return true;
		}

		private static void Run ()
		{
			_running = true;
			listener.Start ();

			while (_running) {
				Console.WriteLine ("Waiting for connection...");
				TcpClient client = listener.AcceptTcpClient ();

				Console.WriteLine ("Client connected!");
				HandleClient (client);
				client.Close ();
					
			} 

			_running = false;
			listener.Stop ();
		}

		private static void HandleClient (TcpClient client)
		{
			StreamReader reader = new StreamReader (client.GetStream ());

			string msg = "";
			while (reader.Peek () != -1) {
				msg += reader.ReadLine () + "\n";
			}

			Debug.WriteLine ("Request: \n" + msg);

			Request req = Request.GetRequest (msg);
			Response resp = Response.From (req);
			resp.Post (client.GetStream ());
		}
	}

	public class Request
	{
		public String ID { get; set; }

		public DateTime dateTime { get; set; }

		public String Host { get; set; }


		private Request (String type, DateTime dTime, string host)
		{
			ID = type;
			dateTime = dTime;
			Host = host;
			
		}

		public static Request GetRequest (string request)
		{
			if (String.IsNullOrEmpty (request))
				return null;
			
			String[] tokens = request.Split (' ');
			String id = tokens [0];
			DateTime dTime = (DateTime)(tokens [1]).;
			String host = tokens [2];

			return new Request (id, dTime, host);
		}
	}

	public class Response
	{
		private const string DefaultTemplate = 
			"HTTP/1.1 200 OK\r\n" +
			"Content-Type:text/html\r\n" +
			"\r\n\r\n" +
			"\r\n\r\n" +
			"<html>ID Number:{0}<br>" +
			"DateTime.Now:{1}<br>" +
			"Requested URL:{2}</html>";

		private Byte[] data = null;


		private Response (Byte[] data)
		{
			this.data = data;
			
		}

		public static Response From (Request req)
		{
			if (req == null)
				return new Response ("404 not found");
		}

		public void Post (NetworkStream stream)
		{
			StreamWriter writer = new StreamWriter (stream);
			writer.Write (String.Format ("HTTP/1.1 200 OK\r\n" +
			"Content-Type:text/html\r\n" +
			"\r\n\r\n" +
			"\r\n\r\n" +
			"<html>ID Number:{0}<br>" +
			"DateTime.Now:{1}<br>" +
			"Requested URL:{2}</html>", 11343200, DateTime.Now.Ticks, WebServer.Name));
		}
	}
}

