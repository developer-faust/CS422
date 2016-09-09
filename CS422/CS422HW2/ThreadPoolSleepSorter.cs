using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace CS422
{
	public class ThreadPoolSleepSorter : IDisposable
	{
		private TextWriter mWriter;
		private readonly ushort mThreadCount;
		 
		private BlockingCollection<SleepSortTask> mTaskCollection = new BlockingCollection<SleepSortTask> ();
		Thread worker;
		private bool isDisposed = false;
 
		public ThreadPoolSleepSorter()
		{
			
			
		}

		public ThreadPoolSleepSorter (TextWriter output, ushort threadCount)
		{ 
  			
			mWriter = output;							// output to console.  

			if (threadCount == 0)						// Sets number of threads to 64.
				mThreadCount = 64;
			else
				mThreadCount = threadCount;

			//output.WriteLine ("Thread Count = {0}", threadCount);

			for (int i = 0; i < mThreadCount; i++) {
				worker = new Thread (new ParameterizedThreadStart (DoWork));
				worker.Start (mTaskCollection);
			} 
		}


		public void DoWork(object collection)
		{
			var task = (BlockingCollection<SleepSortTask>) collection;
			while (!mTaskCollection.IsCompleted)
			{
				try {
					task.Take ().Execute (mWriter); 

				} catch (Exception ex) {
					 
				}
			} 
		}

		public void Sort(byte[] values)
		{ 
			if (values == null)
				throw new ArgumentNullException ("value");
			
			foreach (var item in values) 
			{
				mTaskCollection.Add (new SleepSortTask {
					Data = item 
				});
				 
				//mWriter.WriteLine (item);
			} 
 
		}

		public void Dispose()
		{   
			mTaskCollection.CompleteAdding ();
			worker.Join ();
		} 
	}

	internal class SleepSortTask
	{
		public byte Data { get; set; }
		public void Execute(TextWriter writer)
		{
			Thread.Sleep (Data * 1000);
			writer.WriteLine (Data);
		}
	}
}

