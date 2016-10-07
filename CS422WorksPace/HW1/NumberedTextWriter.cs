using System;
using System.IO;

namespace CS422
{
	public class NumberedTextWriter : TextWriter
	{
		private int lineNum;
		private TextWriter writer; 

		public NumberedTextWriter (TextWriter wrapThis)
		{
			// If we pass in NULL TextWriter throw an exception
			if (null == wrapThis)
				throw new ArgumentNullException ("wrapThis");


			// Initialize the TextWriter object with one passed into constructor
			writer = wrapThis;
			lineNum = 1;			// default value of 1 
			
		}

		public NumberedTextWriter (TextWriter wrapThis, int startingLineNumber)
		{
			if (null == wrapThis)
				throw new ArgumentNullException ("wrapThis");

			if (startingLineNumber < 0)
				throw new ArgumentOutOfRangeException ("startingLineNumber");

			writer = wrapThis;
			lineNum = startingLineNumber;
		}
		 

		public override void WriteLine ()
		{
			writer.WriteLine ("{0}: ", lineNum);
			lineNum++;
		}

		public override void WriteLine (string value)
		{
			writer.WriteLine ("{0}: {1}", lineNum, value);
			lineNum++;
		} 

		public override System.Text.Encoding Encoding {
			get {
				if (null == writer)
					throw new ArgumentNullException ("writer is Null");

				return writer.Encoding;
			}
		}
	}
}

