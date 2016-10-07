using System;
using CS422; 
using NUnit.Framework;

namespace CS422Test
{
	[TestFixture]
	public class HWTests
	{ 
		NumberedTextWriter NumberedWriter;
		IndexedNumsStream NumsStream;

		[Test]
		public void WriteTest()
		{
			NumberedWriter = new NumberedTextWriter (Console.Out);

			Assert.IsNotNull (NumberedWriter);
		  
		}

		[Test]
		public void ReadTest()
		{
			NumsStream = new IndexedNumsStream (255);

			Assert.IsNotNull (NumsStream);
			Assert.AreEqual (NumsStream.Length, 255);

			NumsStream = new IndexedNumsStream (1000);
			Assert.AreEqual (255, NumsStream.Length);

			NumsStream.SetLength (-500);
			Assert.AreEqual (0, NumsStream.Length);

		}
	}
}

