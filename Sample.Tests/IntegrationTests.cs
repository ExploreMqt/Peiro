using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peiro;
using System.Web.Http;
using Xunit;
using System.Net.Http;
using System.Net;

namespace Sample.Tests
{
	public class IntegrationTests : IDisposable
	{
		[Fact]
		public void Post_NewWidget_Created()
		{
			HttpResponseMessage response = inMemoryServer.Post(
				"http://www.sample.com/api/", 
				"widget/", 
				new Widget { Id = 1, SerialNumber = "xyz-001" });

			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		public class Widget
		{
			public int Id;
			public string SerialNumber;
		}
		private HttpServer inMemoryServer;
		public IntegrationTests()
		{
			inMemoryServer = InMemoryServer.Create(WebApiConfig.Register);
		}
		public void Dispose()
		{
			if (inMemoryServer != null)
			{
				inMemoryServer.Dispose();
				inMemoryServer = null;
			}
		}
	}
}
