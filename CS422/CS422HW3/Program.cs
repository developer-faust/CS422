using System;

namespace CS422
{
	class MainClass
	{

		private const string DefaultTemplate = 
			"HTTP/1.1 200 OK\r\n" +
			"Content-Type:text/html\r\n" +
			"\r\n\r\n" +
			"\r\n\r\n" +
			"<html>ID Number:{0}<br>" +
			"DateTime.Now:{1}<br>" +
			"Requested URL:{2}</html>";


		public static void Main (string[] args)
		{ 
			var responseTemplate = string.Format (DefaultTemplate, 11343200, DateTime.Now.Ticks, "HelloWorld!");

			Console.WriteLine ("Starting server on port 4220");
		
			WebServer.Start (4220, responseTemplate);


	  
		}
	}
}
