﻿using System;
using CS422;

namespace CS422HW5
{
	class MainClass
	{ 
		public static void Main (string[] args)
		{
			
			Console.WriteLine ("Hello World!"); 
			Console.ReadKey ();
			WebServer.Start (4220, 5); 
		}
	}
}
