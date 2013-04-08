using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Peiro
{
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
	}
}
