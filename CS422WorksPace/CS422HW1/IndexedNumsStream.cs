using System;
using System.IO;

namespace CS422
{
	public class IndexedNumsStream : Stream
	{

		private long currentPosition;
		private byte  streamLength;
	
		public IndexedNumsStream (long length)
		{

			if (length < 0)
				streamLength = 0; 

			if (length > 255)
				length = 255;

			currentPosition = 0;
			streamLength = (byte) length;  

		}

		public override long Length {
			get {
				return streamLength;
			}
		}

		public override void SetLength(long value)
		{
			 
			if(value < 0)
				streamLength = (byte) 0;
				

			streamLength = (byte) value; 
		}

		public override long Position 
		{
			get{ 
				if (currentPosition < 0)
					return 0;
				
				return currentPosition; 
			}
			set
			{ 
				if (value < 0)
					currentPosition = 0;  
				else if (value > streamLength)
					currentPosition = streamLength;

				currentPosition = value; 
			}
		} 

		public override bool CanRead {
			get {
				return true;
			}
		}

		public override bool CanSeek {
			get {
				return true;
			}
		}

		public override bool CanWrite {
			get {
				return false;
			}
		}

		public override void Flush()
		{
			throw new NotImplementedException();
		}

		protected override void Dispose (bool disposing)
		{
			throw new NotImplementedException ();
		}

		public override long Seek (long offset, SeekOrigin origin)
		{ 
			switch (origin) 
			{

				case SeekOrigin.Begin:
					Position = offset;
					break;
				case SeekOrigin.Current:
					Position += offset;
					break;
				case SeekOrigin.End:
					Position = Length + offset;
					break;
				default:
					break;
			}

			return currentPosition;  
		}

		public override int Read (byte[] buffer, int offset, int count)
		{

			
			int bytesRead = 0;
			var previousPosition = Position; 
			
			if (null == buffer)
				throw new ArgumentNullException ("buffer is null.");
			
			if(offset < 0 || count < 0)
				throw new ArgumentOutOfRangeException ("offset or count is negative.");

			if (buffer.Length < offset + count)
				throw new ArgumentException ("The sum of offset and count is larger than the buffer length."); 
			 
			for (int i = 0; i < count; i++) { 
			
				var value = (byte)(Position % 256);  

				buffer [i + offset] = value;
				bytesRead++;
				Position++;     
			}

			return bytesRead;

		}

		public override void Write (byte[] buffer, int offset, int count)
		{
			if (CanWrite == false)
				throw new NotSupportedException ("Stream is readonly");
		}

	}
}

