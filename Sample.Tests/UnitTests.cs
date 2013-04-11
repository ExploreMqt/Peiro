using System.Net;
using Peiro;
using Xunit;
using Sample.Controllers;

namespace Sample.Tests
{
    public class UnitTests
    {
		[Fact]
		public void Get_InvalidWidgetId_NotFound()
		{
			var sut = new WidgetController();
			sut.FakeRequest();

			var response = sut.Get(0);

			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
		[Fact]
		public void Get_ValidId_Ok()
		{
			var sut = new WidgetController();
			sut.FakeRequest();

			var response = sut.Get(1);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
    }
}
