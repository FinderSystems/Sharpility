using System;
using NFluent;
using NUnit.Framework;
using Sharpility.Util;
using NiceTry;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class EnumsTests
    {
        [Test]
        public void ShouldReturnEnumByName()
        {
            // given
            const string name = "TestValue1";

            // when
            var result = Enums.ValueOf<TestEnum>(name);

            // when
            Check.That(result).IsEqualTo(TestEnum.TestValue1);
        }

        [Test]
        public void ShouldThrowExceptionWhenEnumNotFoundByName()
        {
            // given
            const string name = "not existing";

            // when
            var result = Try.To(() => Enums.ValueOf<TestEnum>(name));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
        }

        [Test]
        public void ShouldReturnEnumsValues()
        {
            // when
            var values = Enums.Values<TestEnum>();

            // when
            Check.That(values).Contains(TestEnum.TestValue1, TestEnum.TestValue2);
        }

        [Test]
        public void ShouldReturnEnumsOrdinalValues()
        {
            // when
            var values = Enums.Ordinals<TestEnum>();

            // when
            Check.That(values).Contains((int)TestEnum.TestValue1, (int)TestEnum.TestValue2);
        }

        [Test]
        public void ShouldReturnEnumValuesNames()
        {
            // wen
            var names = Enums.Names<TestEnum>();

            // then
            Check.That(names).Contains("TestValue1", "TestValue2");
        }
    }

    internal enum TestEnum
    {
        TestValue1 = 5,
        TestValue2 = 6
    }
}
