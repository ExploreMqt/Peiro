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
		public Func<HttpRequestMessage, Task<HttpResponseMessage>> Respond = request => CreateResponse(request, HttpStatusCode.OK);
		public Action<HttpRequestMessage> HandledMessage = m => { };
		public static Task<HttpResponseMessage> CreateResponse(HttpRequestMessage request, HttpStatusCode statusCode)
		{
			var result = new TaskCompletionSource<HttpResponseMessage>();
			result.SetResult(request.CreateResponse(statusCode));
			return result.Task;
		}
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HandledMessage(request);
			return Respond(request);
		}
	}
}
