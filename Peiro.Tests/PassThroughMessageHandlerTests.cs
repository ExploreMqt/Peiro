using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Peiro.Tests
{
	public class PassThroughMessageHandlerTests
	{
		[Fact]
		public void SendAsync_Request_PassedToInnerHandler()
		{
			var request = new HttpRequestMessage();
			HttpRequestMessage messagePassed = null;
			var sut = new EnableSendAsyncMessageHandler(new RespondOkMessageHandler
			{
				RespondOk = r =>
					{
						messagePassed = r;
						return RespondOkMessageHandler.CreateOkResponse(r);
					}
			});

			sut.CallSendAsync(request);
			sut.Dispose();

			Assert.Same(request, messagePassed);
		}
		/// <summary>
		/// Needed because SendAsync is a protected method. This enables us to test the behavior of PassThroughMessageHandler.
		/// </summary>
		internal class EnableSendAsyncMessageHandler : PassThroughMessageHandler
		{
			public EnableSendAsyncMessageHandler()
			{
			}
			public EnableSendAsyncMessageHandler(HttpMessageHandler innerHandler)
				: base(innerHandler)
			{
			}
			public Task<HttpResponseMessage> CallSendAsync(HttpRequestMessage request)
			{
				return SendAsync(request, new CancellationToken());
			}
		}
	}
}
