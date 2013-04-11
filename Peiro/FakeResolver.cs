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
