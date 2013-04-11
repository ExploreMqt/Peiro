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
			var sut = new EnableSendAsyncMessageHandler(new RespondOkMessageHandler { HandledMessage = m => messagePassed = m });

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
