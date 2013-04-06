using System;
using System.Web.Http.Controllers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Peiro
{
	/// <summary>
	/// HttpActionDescriptor is an abstract class and there are no public concrete derived classes. 
	/// You need an HttpActionDescriptor instance to create HttpActionContext objects. 
	/// This is a no-op implementation which facilitates creating HttpActionContext instances.
	/// </summary>
	public class TestableActionDescriptor : HttpActionDescriptor
	{
		public override string ActionName
		{
			get { return null; }
		}
		public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
		{
			return null;
		}
		public override Collection<HttpParameterDescriptor> GetParameters()
		{
			return null;
		}
		public override Type ReturnType
		{
			get { return null; }
		}
	}
}
