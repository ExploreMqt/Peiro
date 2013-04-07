using System;
using System.Web.Http;

namespace Peiro
{
	/// <summary>
	/// Creates an instance of HttpServer and gives it the PassThroughMessageHandler, allowing nearly the 
	/// full stack to be in play for testing. It's primary purpose is to enable integration testing.
	/// </summary>
	public class InMemoryServer
	{
		public static HttpServer Create(Action<HttpConfiguration> registrationAction)
		{
			var config = new HttpConfiguration();
			registrationAction(config);
			config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
			config.MessageHandlers.Add(new PassThroughMessageHandler());
			return new HttpServer(config);
		}
	}
}
