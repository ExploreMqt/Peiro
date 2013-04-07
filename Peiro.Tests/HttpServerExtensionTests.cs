using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Threading;
using Xunit;

namespace Peiro.Tests
{
	public class HttpServerExtensionTests
	{
		[Fact]
		public void Get_HttpServer_RequestMethodIsGet()
		{
			var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
			var messageHandler = new CaptureRequestMessageHandler();
			config.MessageHandlers.Add(messageHandler);
			var server = new HttpServer(config);

			server.Get("http://www.sample.com", string.Empty);
			server.Dispose();

			Assert.Equal(HttpMethod.Get, messageHandler.CapturedRequest.Method);
		}
		[Fact]
		public void Post_SendingContent_RequestMethodIsPost()
		{
			var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
			var messageHandler = new CaptureRequestMessageHandler();
			config.MessageHandlers.Add(messageHandler);
			var server = new HttpServer(config);

			server.Post("http://www.sample.com", string.Empty, new Widget());
			server.Dispose();

			Assert.Equal(HttpMethod.Post, messageHandler.CapturedRequest.Method);
		}
		[Fact]
		public void Post_NoContent_RequestMethodIsPost()
		{
			var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
			var messageHandler = new CaptureRequestMessageHandler();
			config.MessageHandlers.Add(messageHandler);
			var server = new HttpServer(config);

			server.Post("http://www.sample.com", string.Empty);
			server.Dispose();

			Assert.Equal(HttpMethod.Post, messageHandler.CapturedRequest.Method);
		}
		[Fact]
		public void Put_SendingContent_RequestMethodIsPut()
		{
			var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
			var messageHandler = new CaptureRequestMessageHandler();
			config.MessageHandlers.Add(messageHandler);
			var server = new HttpServer(config);

			server.Put("http://www.sample.com", string.Empty, new Widget());
			server.Dispose();

			Assert.Equal(HttpMethod.Put, messageHandler.CapturedRequest.Method);
		}
		[Fact]
		public void Delete_SendingContent_RequestMethodIsDelete()
		{
			var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
			var messageHandler = new CaptureRequestMessageHandler();
			config.MessageHandlers.Add(messageHandler);
			var server = new HttpServer(config);

			server.Delete("http://www.sample.com", string.Empty);
			server.Dispose();

			Assert.Equal(HttpMethod.Delete, messageHandler.CapturedRequest.Method);
		}
		public class Widget
		{
			public int Id;
		}
		internal class CaptureRequestMessageHandler : DelegatingHandler
		{
			public HttpRequestMessage CapturedRequest;
			protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			{
				CapturedRequest = request;
				return base.SendAsync(request, cancellationToken);
			}
		}
	}
}
