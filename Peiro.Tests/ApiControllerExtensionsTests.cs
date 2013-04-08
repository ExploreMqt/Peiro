using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Xunit;

namespace Peiro.Tests
{
	public class ApiControllerExtensionsTests
	{
		[Fact]
		public void ControllerName_TestController_Test()
		{
			var controller = new TestController();

			var name = controller.ControllerName();

			Assert.Equal("Test", name);
		}
		[Fact]
		public void FakeControllerContext_ControllerAndUri_ContextIsNotNull()
		{
			var controller = new TestController();

			var context = controller.FakeControllerContext("http://www.sample.com");

			Assert.NotNull(context);
		}
		[Fact]
		public void FakeControllerContext_ControlerAndUri_DefaultRouteMapped()
		{
			var controller = new TestController();

			var context = controller.FakeControllerContext("http://www.sample.com");

			Assert.Equal("{controller}/{id}", context.RouteData.Route.RouteTemplate);
		}

		public class TestController : ApiController
		{
		}
	}
}
