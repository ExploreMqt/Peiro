using System;
using System.Web.Http.Dependencies;
using Xunit;

namespace Peiro.Tests
{
	public class FakeResolverTests
	{
		[Fact]
		public void Constructor_ImplementsIDependencyResolver()
		{
			Assert.IsAssignableFrom<IDependencyResolver>(new FakeResolver());
		}
		internal class Widget : IDisposable
		{
			public void Dispose(){}
		}
		[Fact]
		public void Register_Widget_TupleOfDesiredTypeAndFactory()
		{
			object typeAndFactory = FakeResolver.Register(typeof(Widget), () => new Widget());
			
			Assert.IsType<Tuple<Type,Func<object>>>(typeAndFactory);
		}
		[Fact]
		public void GetService_NonRegistered_Null()
		{
			var sut = new FakeResolver();

			var instance = sut.GetService(typeof(Widget));

			Assert.Null(instance);
		}
		[Fact]
		public void GetService_WidgetRegistered_InstanceOfWidget()
		{
			var sut = new FakeResolver(FakeResolver.Register(typeof(Widget), () => new Widget()));

			var result = sut.GetService(typeof(Widget));

			Assert.IsType<Widget>(result);
		}
	}
}
