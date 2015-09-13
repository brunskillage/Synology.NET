using Castle.Core.Internal;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace SynologyClient.ApiTests
{
    [TestFixture]
    public class AllTestsAsSingleTest
    {
        [Test]
        [Ignore]
        public void RunallTests()
        {
            var apitests = new ApiTests();
            var methods =
                typeof(ApiTests).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(m => m.GetAttributes<TestAttribute>().Any());

            foreach (var methodInfo in methods)
                methodInfo.Invoke(apitests, null);
        }
    }
}