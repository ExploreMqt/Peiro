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
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace Peiro
{
	/// <summary>
	/// This class simulates what WebApi sets up for an a request. 
	/// This class is most commonly used in unit tests
	/// </summary>
	public static class ApiControllerExtensions
	{
		public static string ControllerName(this ApiController controller)
		{
			string name = controller.GetType().Name;
			return name.Substring(0, name.IndexOf("Controller", StringComparison.CurrentCultureIgnoreCase));
		}
		public static HttpControllerContext FakeControllerContext(this ApiController controller, string uri)
		{
			return FakeControllerContext(controller, uri, new KeyValuePair<string, string>("DefaultApi", "{controller}/{id}"));
		}
		public static HttpControllerContext FakeControllerContext(this ApiController controller, string uri, params KeyValuePair<string,string>[] routes)
		{
			var config = new HttpConfiguration();
			if (uri.EndsWith("/") == false)
				uri += "/";
			var controllerName = controller.ControllerName();
			var request = new HttpRequestMessage(HttpMethod.Post, uri + controllerName);
			IHttpRoute lastRoute = null;
			Array.ForEach(routes, r => lastRoute = config.Routes.MapHttpRoute(r.Key, r.Value));
			var routeData = new HttpRouteData(lastRoute, new HttpRouteValueDictionary { { "controller", controllerName.ToLower() } });
			return new HttpControllerContext(config, routeData, request);
		}
		public static void FakeRequest(this ApiController controller)
		{
			controller.ControllerContext = controller.FakeControllerContext("http://www.sample.com");
			controller.Request = controller.ControllerContext.Request;
			controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = controller.ControllerContext.Configuration;
			controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey] = controller.ControllerContext.RouteData;
		}
	}
}
