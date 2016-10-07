using System;

namespace CS422
{
	class MainClass
	{
		private static IndexedNumsStream myIndexNumStream;
		private static byte[] buffer;
		public static void Main (string[] args)
		{

			int bytesRead;
			buffer = new byte[1000];
			myIndexNumStream = new IndexedNumsStream (255);
			try {

				bytesRead = myIndexNumStream.Read (buffer, 0, 300);

				for (int i = 0; i < bytesRead; i++) {
					Console.WriteLine ("{0} : [{1}]", i, buffer[i]);								
				}

				
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}

			try {

				bytesRead = myIndexNumStream.Read (buffer, -10, 300);

				for (int i = 0; i < bytesRead; i++) {
					Console.WriteLine ("{0} : [{1}]", i, buffer[i]);								
				}

			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}

		}
	}
}
