using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Peiro
{
	/// <summary>
	/// A concrete implementation of the abstract DelegatingHandler class. 
	/// This class allows integration tests to simulate the whole stack without actually using IIS.
	/// </summary>
	public class PassThroughMessageHandler : DelegatingHandler
	{
		public PassThroughMessageHandler()
		{
		}
		public PassThroughMessageHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{
		}
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			return base.SendAsync(request, cancellationToken);
		}
	}
}
