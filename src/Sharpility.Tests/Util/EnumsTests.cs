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
        public void ShouldReturnEnumByNameCaseInsensitive()
        {
            // given
            const string name = "testvalue2";

            // when
            var result = Enums.ValueOf<TestEnum>(name, ignoreCase: true);

            // when
            Check.That(result).IsEqualTo(TestEnum.TestValue2);
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

        [Test]
        public void ShouldReturnEnumByNameOnTry()
        {
            // given
            const string name = "TestValue2";

            // var
            var value = Enums.TryValueOf<TestEnum>(name);

            // then
            Check.That(value).IsEqualTo(TestEnum.TestValue2);
        }

        [Test]
        public void ShouldReturnEnumByNameCaseInsensitiveOnTry()
        {
            // given
            const string name = "TESTVALUE2";

            // var
            var value = Enums.TryValueOf<TestEnum>(name, ignoreCase: true);

            // then
            Check.That(value).IsEqualTo(TestEnum.TestValue2);
        }

        [Test]
        public void ShouldReturnNullEnumValueWhenNotFound()
        {
            // given
            const string name = "not existing";

            // var
            var value = Enums.TryValueOf<TestEnum>(name);

            // then
            Check.That(value).IsNull();
        }

        [Test]
        public void ShouldReturEnumValueByNameIgnoringDefault()
        {
            // given
            const string name = "TestValue1";
            const TestEnum defaultValue = TestEnum.TestValue2;

            // when
            var value = Enums.ValueOf(name, defaultValue);

            // then
            Check.That(value).IsEqualTo(TestEnum.TestValue1);
        }

        [Test]
        public void ShouldReturEnumValueByNameCaseInsensivieIgnoringDefault()
        {
            // given
            const string name = "Testvalue1";
            const TestEnum defaultValue = TestEnum.TestValue2;

            // when
            var value = Enums.ValueOf(name, defaultValue, ignoreCase: true);

            // then
            Check.That(value).IsEqualTo(TestEnum.TestValue1);
        }

        [Test]
        public void ShouldReturnDefaultEnumValueWhenNotFoundByName()
        {
            // given
            const string name = "not existing";
            const TestEnum defaultValue = TestEnum.TestValue2;

            // when
            var value = Enums.ValueOf(name, defaultValue);

            // then
            Check.That(value).IsEqualTo(defaultValue);
        }
    }

    internal enum TestEnum
    {
        TestValue1 = 5,
        TestValue2 = 6
    }
}
