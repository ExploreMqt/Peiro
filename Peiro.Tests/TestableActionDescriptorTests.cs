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
