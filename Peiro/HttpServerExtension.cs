using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
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

