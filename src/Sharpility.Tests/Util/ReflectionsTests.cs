
using System;
using System.Diagnostics.CodeAnalysis;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class ReflectionsTests
    {
        [Test]
        public void ShouldReturnCurrentType()
        {
            // when
            var currentType = Reflections.CurrentType();

            // then
            Check.That(currentType).IsEqualTo(GetType());
        }

        [Test]
        public void ShouldReturnObjectProperties()
        {
            // given
            const int secret = 33;
            var testObject = new SimpleTypeWithProperties
            {
                Id = 1,
                Name = "Test",
                Value = null
            };
            testObject.SetSecret(secret);

            // when
            var properties = Reflections.Properties(testObject);

            // then
            Check.That(properties.OrderedKeys).ContainsExactly("Id", "Name", "Value", "Secret");
            Check.That(properties.Get("Id")).IsEqualTo(testObject.Id);
            Check.That(properties.Get("Name")).IsEqualTo(testObject.Name);
            Check.That(properties.Get("Value")).IsEqualTo(testObject.Value);
            Check.That(properties.Get("Secret")).IsEqualTo(secret);
        }

        [Test]
        public void ShouldReturnObjectPropertiesIncludingBaseType()
        {
            // given
            const int secret = 33;
            var testObject = new NestedTypeWithProperties(id: 1, secret: secret, 
                value: 5, name: "x", extension: "abc");

            // when
            var properties = Reflections.Properties(obj: testObject, includeBase: true);

            // then
            Check.That(properties.OrderedKeys).ContainsExactly("Extension", "Id", "Name", "Value", "Secret");
            Check.That(properties.Get("Id")).IsEqualTo(testObject.Id);
            Check.That(properties.Get("Name")).IsEqualTo(testObject.Name);
            Check.That(properties.Get("Value")).IsEqualTo(testObject.Value);
            Check.That(properties.Get("Secret")).IsEqualTo(secret);
            Check.That(properties.Get("Extension")).IsEqualTo(testObject.Extension);
        }

        [Test]
        public void ShouldReturnCallingMethodName()
        {
            // when
            var methodName = CallingMethodNameProvider.Get();

            // then
            Check.That(methodName).IsEqualTo("ShouldReturnCallingMethodName");
        }

        [Test]
        public void ShouldReturnObjectFields()
        {
            // given
            var obj = new ObjectWithFields(name: "Test", value: 15, date: new DateTime(2015, 4, 21, 0, 0, 0));

            // when
            var fields = Reflections.Fields(obj);

            // then
            Check.That(fields.OrderedKeys).ContainsExactly("name", "value", "date");
            Check.That(fields.Get("name")).IsEqualTo("Test");
            Check.That(fields.Get("value")).IsEqualTo(15);
            Check.That(fields.Get("date")).IsEqualTo(new DateTime(2015, 4, 21, 0, 0, 0));
        }

        [Test]
        public void ShouldReturnObjectFieldsIncludingBaseType()
        {
            // given
            var obj = new ComplexObjectWithFields(id: 1, name: "Test", value: 15, date: null);

            // when
            var fields = Reflections.Fields(obj: obj, includeBase: true);

            // then
            Check.That(fields.OrderedKeys).ContainsExactly("id", "name", "value", "date");
            Check.That(fields.Get("id")).IsEqualTo(1);
            Check.That(fields.Get("name")).IsEqualTo("Test");
            Check.That(fields.Get("value")).IsEqualTo(15);
            Check.That(fields.GetIfPresent("date")).IsNull();
        }

        [Test]
        public void ShouldReturnObjectFieldsExcludingBaseType()
        {
            // given
            var obj = new ComplexObjectWithFields(id: 1, name: "Test", value: 15, date: new DateTime(2015, 4, 21, 0, 0, 0));

            // when
            var fields = Reflections.Fields(obj: obj, includeBase: false);

            // then
            Check.That(fields.OrderedKeys).ContainsExactly("id");
            Check.That(fields.Get("id")).IsEqualTo(1);
        }

        private class SimpleTypeWithProperties
        {
            public int Id { get; set; }
            public string Name { get; set; }
            internal int? Value { get; set; }
            private int Secret { get; set; }

            internal void SetSecret(int value)
            {
                Secret = value;
            }
        }

        private class NestedTypeWithProperties : SimpleTypeWithProperties
        {
            public string Extension { get; private set; }

            internal NestedTypeWithProperties(int secret, int? value, string name, int id, string extension)
            {
                Value = value;
                Name = name;
                Id = id;
                Extension = extension;
                SetSecret(secret);
            }
        }

        private static class CallingMethodNameProvider
        {
            internal static string Get()
            {
                return Reflections.CallingMethodName();
            }
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private class ObjectWithFields
        {
            private readonly string name;
            private readonly int value;
            private readonly DateTime? date;

            internal ObjectWithFields(string name, int value, DateTime? date)
            {
                this.date = date;
                this.value = value;
                this.name = name;
            }
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private class ComplexObjectWithFields : ObjectWithFields
        {
            private readonly int id;

            internal ComplexObjectWithFields(int id, string name, int value, DateTime? date)
                : base(name, value, date)
            {
                this.id = id;
            }
        }
    }
}
