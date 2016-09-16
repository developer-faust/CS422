using System;
using System.Threading;
using System.Collections.Generic;

namespace CS422
{
	public class Program
	{

		private static Thread[] ThreadQ;
		private static PCQueue myQueue;
		private static ThreadPoolSleepSorter mySleepSorter;


		public static void Main()
		{
			ThreadQ = new Thread[2];
			myQueue = new PCQueue ();
			Console.WriteLine ("Hello World");


			ThreadQ [0] = new Thread (new ThreadStart (QueueItems ));
			ThreadQ [1] = new Thread (new ThreadStart (DequeueItems ));

			ThreadQ [0].Start ();
			ThreadQ [1].Start ();


			//			Console.WriteLine ("SleepSort");

			// Begin SleepSorter 
//			Random rnd = new Random (); 
//			byte[] rndList = new byte[100];
// 			 
//			 
//
//			for (int i = 0; i < 100; i++) {
//				rndList[i] = (byte)rnd.Next (20);
//				Console.WriteLine ("{0} = {1}", i, rndList[i]);
//			}
//
//			ushort count = 100;
//			mySleepSorter = new ThreadPoolSleepSorter (Console.Out, count); 
//			mySleepSorter.Sort (rndList);
//			mySleepSorter.Dispose ();

			Console.WriteLine ("Done!");
		}

		 

		public static void QueueItems()
		{
			for (int i = 0; i < 500; i++) {
				myQueue.Enqueue (i);
				Console.WriteLine ("enqueued: {0}", i);

			} 
		}

		public static void DequeueItems()
		{
			int value = -1;

			for (int i = 0; i < 500; i++) {
				myQueue.Dequeue(ref value); 
				Console.WriteLine ("dequeued: {0}", value);
			} 
		}
	}
}