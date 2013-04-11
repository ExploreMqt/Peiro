using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sample.Models;

namespace Sample.Controllers
{
	public class WidgetController : ApiController
	{
		public HttpResponseMessage Get(int id)
		{
			if (id <= 0)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Widget exist with an id of zero");
			return Request.CreateResponse(HttpStatusCode.OK, new Widget { Id = id, SerialNumber = "abc-123" });
		}

		//// POST api/values
		//public void Post([FromBody]string value)
		//{
		//}

		//// PUT api/values/5
		//public void Put(int id, [FromBody]string value)
		//{
		//}

		//// DELETE api/values/5
		//public void Delete(int id)
		//{
		//}
	}
}