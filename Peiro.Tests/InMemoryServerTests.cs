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
