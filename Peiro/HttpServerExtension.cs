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
using System.Net.Http.Formatting;
using System.Web.Http;

/// <summary>
/// Helper methods to make integration tests cleaner and more readable.
/// </summary>
public static class HttpServerExtension
{
	private static void NoOp(HttpRequestMessage message)
	{
	}
	private static void NoContent(HttpRequestMessage message)
	{
	}
	public static HttpResponseMessage Delete(this HttpServer server, string baseUri, string relativeUri)
	{
		return Delete(server, baseUri, relativeUri, NoOp);
	}
	public static HttpResponseMessage Delete(this HttpServer server, string baseUri, string relativeUri, Action<HttpRequestMessage> setup)
	{
		return CreateRequestAndSendAsync(server, HttpMethod.Delete, baseUri, relativeUri, NoContent, setup);
	}
	public static HttpResponseMessage Get(this HttpServer server, string baseUri, string relativeUri)
	{
		return Get(server, baseUri, relativeUri, NoOp);
	}
	public static HttpResponseMessage Get (this HttpServer server, string baseUri, string relativeUri, Action<HttpRequestMessage> setup)
	{
		return CreateRequestAndSendAsync(server, HttpMethod.Get, baseUri, relativeUri, NoContent, setup);
	}
	public static HttpResponseMessage Post(this HttpServer server, string baseUri, string relativeUri)
	{
		return Post(server, baseUri, relativeUri, NoContent);
	}
	public static HttpResponseMessage Post<T>(this HttpServer server, string baseUri, string relativeUri, T content)
	{
		return Post(server, baseUri, relativeUri, content, NoOp);
	}
	public static HttpResponseMessage Post(this HttpServer server, string baseUri, string relativeUri, Action<HttpRequestMessage> setup)
	{
		return CreateRequestAndSendAsync(server, HttpMethod.Post, baseUri, relativeUri, NoContent, setup);
	}
	public static HttpResponseMessage Post<T>(this HttpServer server, string baseUri, string relativeUri, T content, Action<HttpRequestMessage> setup)
	{
		return CreateRequestAndSendAsync(
			server, 
			HttpMethod.Post, 
			baseUri, 
			relativeUri, 
			request => request.Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter()),
			setup);
	}
	public static HttpResponseMessage Put<T>(this HttpServer server, string baseUri, string relativeUri, T content)
	{
		return Put(server, baseUri, relativeUri, content, NoOp);
	}
	public static HttpResponseMessage Put<T>(this HttpServer server, string baseUri, string relativeUri, T content, Action<HttpRequestMessage> setup)
	{
		return CreateRequestAndSendAsync(
			server,
			HttpMethod.Put,
			baseUri,
			relativeUri,
			request => request.Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter()),
			setup);
	}
	private static HttpResponseMessage CreateRequestAndSendAsync(
		HttpServer server,
		HttpMethod method,
		string baseUri,
		string relateiveUri,
		Action<HttpRequestMessage> addContent,
		Action<HttpRequestMessage> setup)
	{
		var client = new HttpClient(server);
		var request = new HttpRequestMessage(method, baseUri + relateiveUri);
		setup(request);
		addContent(request);

		return client.SendAsync(request).Result;
	}
}

