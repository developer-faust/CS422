using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CS422
{
	public enum ValidStates
	{
		Method,
		Uri,
		Headers,
		Validated,
		Invalidated
	}

	public enum RequestStatus
	{
		Valid,
		InValid,
		Indeterminate
	}
	
	public class Request
	{

		public HashSet<string> Methods{ get; } = new HashSet<string>();
		public HashSet<string> Versions{ get; } = new HashSet<string>();

		private bool _hasValidMethod;
		private bool _hasValidUri;
		private bool _hasValidVersion;

		private NetworkStream _ClientNetworkStream;

		public Stream Body { get; private set; }
		public ValidStates ValidationState { get; private set; }
		public string Method {get; private set; }
		public string RequestedUri { get; private set; }

		public string Version { get; private set; } 
		public ConcurrentDictionary<string, string> RequestHeaders { get; }



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
			Method = string.Empty;
			RequestedUri = string.Empty;
			Version = string.Empty;

			RequestHeaders = new ConcurrentDictionary<string, string> ();
		}

		public Request (IEnumerable<string> validMethods = null,
		                IEnumerable<string> validVersion = null) : this ()
		{
			if (validMethods != null) 
			{
				foreach (var item in validMethods) 
				{
					Methods.Add (item);
				}
			}

			if (validVersion != null)
			{
				foreach (var item in validVersion) {
					Versions.Add (item);
				}				
			} 
		}

		#region Response types 

		// Not found response type
		public void NotFoundResponse(string page)
		{

		}

		public bool WriteResponse(string response)
		{
			
		} 
		#endregion

		#region Validation
		public bool ValidateRequest(NetworkStream ns)
		{
			_ClientNetworkStream = ns;

			byte[] buf = new byte[4096];
			var requestStream = new MemoryStream ();

			while (ValidationState != ValidStates.Validated)
			{
				int read = _ClientNetworkStream.Read (buf, 0, buf.Length);

				if (read > 0) 
				{
					// Write the request stream into buffer
					requestStream.Write (buf, 0, read);


					#region Validate the request
					var requestString = Encoding.ASCII.GetString (requestStream.ToArray ());

					// Case 1: Parts of the request is invalid
					if (TryValidate(requestString) == ValidStates.Invalidated)
					{
						requestStream.Dispose ();
						return false;
					}

					#endregion
				}

			}

			SetRequestBody (requestStream);

			return true;
		}

		public RequestStatus IsValidRequestMethod(string value, ref string outMethod)
		{
			RequestStatus status = RequestStatus.InValid;

			// First white space immediately following the Method
			var indexOfSpace = value.IndexOf(' ');
			int methodLength = indexOfSpace < 0 ? value.Length : indexOfSpace;
		}


		#endregion

		#region request body
		private void SetRequestBody(MemoryStream rs)
		{
			const string doubleClrf = "\r\n\r\n";
			const string Content_Type = "Content-Type: text/html";
			const string Content_Length = "content-length";


			// The start of the request body is past the doubleClrf
			int startOfBody = Encoding.ASCII.GetString (rs.ToArray ())
				.IndexOf (doubleClrf, StringComparison.Ordinal) + doubleClrf.Length;


			// Seek to the begining of the body inside request stream
			rs.Seek (startOfBody, SeekOrigin.Begin);

			#region verify Content Length
			if (RequestHeaders.ContainsKey (Content_Length)) 
			{
				long fixedLength;
				if(long.TryParse(RequestHeaders[Content_Length], out fixedLength))
				{
					// TODO: Contactonate the request stream with clientStream and fixedLength into the body
					return;
				}

			}
			#endregion

			// TODO: Create the request Body by concatonating request stream and client stream
			
			
		}



		private ValidStates TryValidate(string value)
		{

		}

		System.Net.Sockets.NetworkStream Response;
	}
}

