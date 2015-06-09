using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Sharpility.Extensions;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class ComparerExtensionsTests
    {
        [Test, TestCaseSource("ReverseComparerTestCases")]
        public int ShouldExtendComparerByReverseMethod(IComparer<object> comparer)
        {
            // given
            var obj1 = new object();
            var obj2 = new object();

            // when
            var reverse = comparer.Reverse();

            // then
            return reverse.Compare(obj1, obj2);
        }

        private static IEnumerable<ITestCaseData> ReverseComparerTestCases()
        {
            var comparer = new Mock<IComparer<object>>();
            comparer.Setup(instance => instance.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(1);
            yield return new TestCaseData(comparer.Object)
                .SetName("Should return reversed comparer: 1")
                .Returns(-1);

            comparer = new Mock<IComparer<object>>();
            comparer.Setup(instance => instance.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(-1);
            yield return new TestCaseData(comparer.Object)
                .SetName("Should return reversed comparer: -1")
                .Returns(1);

            comparer = new Mock<IComparer<object>>();
            comparer.Setup(instance => instance.Compare(It.IsAny<object>(), It.IsAny<object>())).Returns(0);
            yield return new TestCaseData(comparer.Object)
                .SetName("Should return reversed comparer: 0")
                .Returns(0);
        }
    }
}
