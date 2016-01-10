using System;
using System.Collections.Specialized;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class NameValueCollectionExtensionsTests
    {
        [Test]
        public void ShouldReturnIntValueFromSettings()
        {
            // given
            const string propertyName = "test";
            var settings = new NameValueCollection();
            settings[propertyName] = "123";

            // when
            var value = settings.IntValue(propertyName, 0);

            // then
            Check.That(value).IsEqualTo(123);
        }

        [Test]
        public void ShouldReturnDefaultIntValueFromSettingsIfPropertyNotSpecified()
        {
            // given
            const string propertyName = "test";
            const int defaultValue = 5;
            var settings = new NameValueCollection();

            // when
            var value = settings.IntValue(propertyName, defaultValue);

            // then
            Check.That(value).IsEqualTo(defaultValue);
        }

        [Test]
        public void ShouldReturnNullIntValueFromSettingsIfPropertyNotSpecified()
        {
            // given
            const string propertyName = "test";
            var settings = new NameValueCollection();

            // when
            var value = settings.IntValue(propertyName);

            // then
            Check.That(value).IsNull();
        }

        [Test]
        public void ShouldReturnBoolValueFromSettings()
        {
            // given
            const string property1 = "property1";
            const string property2 = "property2";
            const string property3 = "property3";
            const string property4 = "property4";
            const string property5 = "property5";
            const string property6 = "property6";
            const string property7 = "property7";
            var settings = new NameValueCollection();
            settings[property1] = "true";
            settings[property2] = "false";
            settings[property3] = "TrUe";
            settings[property4] = "1";
            settings[property5] = "0";
            settings[property6] = "3";
            settings[property7] = "invalid";

            // when
            var value1 = settings.BoolValue(property1);
            var value2 = settings.BoolValue(property2);
            var value3 = settings.BoolValue(property3);
            var value4 = settings.BoolValue(property4);
            var value5 = settings.BoolValue(property5);
            var value6 = settings.BoolValue(property6);
            var value7 = settings.BoolValue(property7);

            // then
            Check.That(value1).IsTrue();
            Check.That(value2).IsFalse();
            Check.That(value3).IsTrue();
            Check.That(value4).IsTrue();
            Check.That(value5).IsFalse();
            Check.That(value6).IsFalse();
            Check.That(value7).IsFalse();
        }

        [Test]
        public void ShouldReturnFalseValueFromSettingsWhenPropertyNotSpecified()
        {
            // given
            const string propertyName = "test";
            var settings = new NameValueCollection();

            // when
            var value = settings.BoolValue(propertyName);

            // then
            Check.That(value).IsFalse();
        }

        [Test]
        public void ShouldReturnDefaultBoolValueFromSettingsWhenPropertyNotSpecified()
        {
            // given
            const string propertyName = "test";
            var settings = new NameValueCollection();
            const bool defaultValue = true;

            // when
            var value = settings.BoolValue(propertyName, defaultValue);

            // then
            Check.That(value).IsEqualTo(defaultValue);
        }

        [Test]
        public void ShouldReturDefaultTimeSpanWithUnitFromSettingsWhenPropertyNotSpecified()
        {
            // given
            const string property = "property";
            var settings = new NameValueCollection();
            var defaultValue = TimeSpan.FromSeconds(13);

            // when
            var value = settings.TimeSpanValue(property, defaultValue);

            // then
            Check.That(value).IsEqualTo(defaultValue);
        }

        [Test]
        public void ShouldReturDefaultTimeSpanWithUnitFromSettingsWhenPropertyValueIsEmpty()
        {
            // given
            const string property = "property";
            var settings = new NameValueCollection();
            settings[property] = "";
            var defaultValue = TimeSpan.FromSeconds(13);

            // when
            var value = settings.TimeSpanValue(property, defaultValue);

            // then
            Check.That(value).IsEqualTo(defaultValue);
        }

        [Test]
        public void ShouldReturnTimeSpanFromSettings()
        {
            // given
            const string property = "property";
            var settings = new NameValueCollection();
            settings[property] = "00:01:15";

            // when
            var value = settings.TimeSpanValue(property);

            // then
            Check.That(value).IsEqualTo(TimeSpan.FromSeconds(75));
        }

        [Test]
        public void ShouldReturnNullTimeSpanFromSettingsWhenPropertyNotSpecified()
        {
            // given
            const string property = "property";
            var settings = new NameValueCollection();

            // when
            var value = settings.TimeSpanValue(property);

            // then
            Check.That(value).IsNull();
        }
    }
}
