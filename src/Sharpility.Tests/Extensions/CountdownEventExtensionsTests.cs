using System.Threading;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class CountdownEventExtensionsTests
    {
        [Test]
        public void ShouldSignalCountDownEvent()
        {
            // given
            var countDownEvent = new CountdownEvent(1);

            // when
            var result = countDownEvent.TrySignal();

            // then
            Check.That(result).IsTrue();
            Check.That(countDownEvent.CurrentCount).IsEqualTo(0);
        }

        [Test]
        public void ShouldIgnoreSignalingCountDownWhenCurrentCountIsZero()
        {
            // given
            var countDownEvent = new CountdownEvent(0);

            // when
            var result = countDownEvent.TrySignal();

            // then
            Check.That(result).IsFalse();
            Check.That(countDownEvent.CurrentCount).IsEqualTo(0);
        }
    }
}
