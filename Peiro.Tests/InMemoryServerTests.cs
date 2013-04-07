using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Xunit;

namespace Peiro.Tests
{
	public class InMemoryServerTests
	{
		[Fact]
		public void Create_RigistrationAction_ActionCalled()
		{
			bool called = false;
			
			InMemoryServer.Create(config => called = true);

			Assert.True(called);
		}
		[Fact]
		public void Create_RigistrationAction_RecievesNonNullConfiguration()
		{
			InMemoryServer.Create(config => Assert.NotNull(config));
		}
		[Fact]
		public void Create_NoOpRegistrationAction_IncludeErrorDetailPolicySetToAlways()
		{
			var server = InMemoryServer.Create(a => { });

			Assert.Equal(IncludeErrorDetailPolicy.Always, server.Configuration.IncludeErrorDetailPolicy);
			server.Dispose();
		}
		[Fact]
		public void Create_NoOpRegistryAction_MessageHanderContainsPassThroughMessageHandler()
		{
			var server = InMemoryServer.Create(a => { });
			
			Assert.NotNull(server.Configuration.MessageHandlers.Where(h => h is PassThroughMessageHandler).FirstOrDefault());
			server.Dispose();
		}
		[Fact]
		public void Create_ConfigUsedInRegistration_IsInReturnedServer()
		{
			HttpConfiguration appliedToRegistration = null;

			var server = InMemoryServer.Create(c => appliedToRegistration = c);

			Assert.Same(appliedToRegistration, server.Configuration);
			server.Dispose();
		}
	}
}
