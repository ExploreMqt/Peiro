using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Peiro
{
	/// <summary>
	/// This enables you to supply fake implementations to sections of code that doesn't have a direct public interface.
	/// NOTE: If you are using an IoC, you will not need this class.
	/// </summary>
	public class FakeResolver : IDependencyResolver
	{
		public static Tuple<Type, Func<object>> Register(Type type, Func<object> factory)
		{
			return new Tuple<Type,Func<object>>(type, factory);
		}
		private readonly Tuple<Type, Func<object>>[] registered;
		public FakeResolver(params Tuple<Type, Func<object>>[] resolvers)
		{
			registered = resolvers; 
		}
		public IDependencyScope BeginScope()
		{
			throw new NotImplementedException();
		}

		public object GetService(Type serviceType)
		{
			var resolver = Array.Find(registered, r => r.Item1 == serviceType);
			if (resolver != null)
				return resolver.Item2();
			return null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new List<object>();
		}

		public void Dispose()
		{
		}
	}
}
