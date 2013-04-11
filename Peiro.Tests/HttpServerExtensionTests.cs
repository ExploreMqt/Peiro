/*Copyright (C) 2013 Jim Argeropoulos

 Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
 files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
 modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
 Software is furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
 WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
 ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
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
