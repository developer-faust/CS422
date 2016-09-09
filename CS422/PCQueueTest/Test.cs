using NUnit.Framework;
using System;
using CS422;
namespace PCQueueTest
{
	[TestFixture ()]
	public class Test
	{

		private PCQueue queue;

		[SetUp]
		public void Setup()
		{
			queue = new PCQueue ();
		}

		[Test ()]
		public void TestEnqueueDequeue ()
		{
			int value = 0;

			for (int i = 0; i < 1000; i++) {
				queue.Enqueue (i);
			}

			bool result;
			for (int i = 0; i < 1000; i++) {
				result = queue.Dequeue (ref value);

				Assert.AreEqual (i, value);
				Assert.IsTrue (result);
			} 

			result = queue.Dequeue (ref value);
			Assert.IsFalse (result);
		}
	}
}

