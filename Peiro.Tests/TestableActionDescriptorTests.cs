using System.Web.Http.Controllers;
using Xunit;
using System.Threading;

namespace Peiro.Tests
{
	public class TestableActionDescriptorTests
	{
		[Fact]
		public void DefaultConstructor_CastableToActionDescriptor()
		{
			var sut = new TestableActionDescriptor();

			Assert.NotNull(sut as HttpActionDescriptor);
		}
		[Fact]
		public void ActionName_ReturnsNull()
		{
			var sut = new TestableActionDescriptor();

			Assert.Null(sut.ActionName);
		}
		[Fact]
		public void ExecuteAsync_ReturnsNull()
		{
			var sut = new TestableActionDescriptor();

			Assert.Null(sut.ExecuteAsync(null, null, CancellationToken.None));
		}
		[Fact]
		public void GetParameters_ReturnsNull()
		{
			var sut = new TestableActionDescriptor();

			Assert.Null(sut.GetParameters());
		}
		[Fact]
		public void ReturnType_ReturnsNull()
		{
			var sut = new TestableActionDescriptor();

			Assert.Null(sut.ReturnType);
		}
	}
}
