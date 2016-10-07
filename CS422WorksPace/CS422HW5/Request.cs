using System;
using System.IO;
using System.Collections.Concurrent;

namespace CS422
{
	
	public class Request
	{
		Stream Body;
		ConcurrentDictionary<string, string> Headers;
		string Method;
		string RequestTarget;
		string HTTPVersion; 

		public long GetContentLengthOrDefault(long defaultValue)
		{
			throw new NotImplementedException ();
			
		}

		public Tuple<long, long> GetRangeHeader()
		{
			throw new NotImplementedException ();
		}
		public Request ()
		{
		}

		System.Net.Sockets.NetworkStream Response;
	}
}

