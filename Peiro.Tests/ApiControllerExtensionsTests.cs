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
using System.Web.Http;
using System.Web.Http.Hosting;
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
		[Fact]
		public void FakeRequest_Controller_HasControllerContext()
		{
			var controller = new TestController();

			controller.FakeRequest();

			Assert.NotNull(controller.ControllerContext);
		}
		[Fact]
		public void FakeRequest_Controller_HasRequest()
		{
			var controller = new TestController();

			controller.FakeRequest();

			Assert.NotNull(controller.Request);
		}
		[Fact]
		public void FakeRequest_Controller_RequestPropertyConfigurationKeySet()
		{
			var controller = new TestController();

			controller.FakeRequest();

			Assert.Equal(controller.ControllerContext.Configuration, controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey]);
		}
		[Fact]
		public void FakeRequest_Controller_RequestPropertyRouteDataKeySet()
		{
			var controller = new TestController();

			controller.FakeRequest();

			Assert.Equal(controller.ControllerContext.RouteData, controller.Request.Properties[HttpPropertyKeys.HttpRouteDataKey]);
		}

		public class TestController : ApiController
		{
		}
	}
}
