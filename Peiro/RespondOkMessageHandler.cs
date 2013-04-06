using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Peiro
{
	/// <summary>
	/// This concrete instance of DelegatingHandler is used to simplify creating an OK response message.
	/// </summary>
	public class RespondOkMessageHandler : DelegatingHandler
	{
		public Func<HttpRequestMessage, Task<HttpResponseMessage>> RespondOk = request => CreateOkResponse(request);
		public static Task<HttpResponseMessage> CreateOkResponse(HttpRequestMessage request)
		{
			var result = new TaskCompletionSource<HttpResponseMessage>();
			result.SetResult(request.CreateResponse(HttpStatusCode.OK));
			return result.Task;
		}
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			return RespondOk(request);
		}
	}
}
