using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using NUnit.Framework;

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
            IEnumerable<MethodInfo> methods =
                typeof (ApiTests).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(m => m.GetAttributes<TestAttribute>().Any());

            foreach (MethodInfo methodInfo in methods)
                methodInfo.Invoke(apitests, null);
        }
    }
}
