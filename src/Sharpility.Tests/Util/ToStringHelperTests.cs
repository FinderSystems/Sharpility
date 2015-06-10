
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NFluent;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class ToStringHelperTests
    {
        [Test]
        public void ShouldBuildToString()
        {
            // given
            var obj = new TestObj
            {
                Id = 1,
                Name = "Test",
                Values = Lists.AsList(1, 2, 3)
            };

            // when
            var toString = ToStringHelper.Of(obj)
                .Add("Id", obj.Id)
                .Add("Name", obj.Name)
                .Add("Values", obj.Values)
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj {Id:1, Name:Test, Values:[1, 2, 3]}");
        }

        [Test]
        public void ShouldBuildToStringFromProperties()
        {
            // given
            var obj = new TestObj
            {
                Id = 1,
                Name = "Test",
                Values = Lists.AsList(1, 2, 3)
            };

            // when
            var toString = ToStringHelper.Of(obj)
                .AddProperties()
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj {Name:Test, Id:1, Objects:null, Values:[1, 2, 3], Flag:Default, Duration:null}");
        }

        [Test]
        public void ShouldBuildToStringFromPropertiesWithoutNulls()
        {
            // given
            var obj = new TestObj
            {
                Id = 1,
                Name = "Test",
                Values = Lists.AsList(1, 2, 3)
            };

            // when
            var toString = ToStringHelper.Of(obj)
                .AddProperties()
                .SkipNulls()
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj {Name:Test, Id:1, Values:[1, 2, 3], Flag:Default}");
        }

        [Test]
        public void ShouldBuildToStringFromFields()
        {
            // given
            var obj = new TestObj2(age: 30, surname: "Testowski", data: Lists.AsList("A", "B", "C"));

            // when
            var toString = ToStringHelper.Of(obj)
                .AddFields()
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj2 {age:30, surname:Testowski, data:[A, B, C]}");
        }

        [Test]
        public void ShouldBuildToStringFromFieldsWithoutNulls()
        {
            // given
            var obj = new TestObj2(age: 30, surname: null, data: null);

            // when
            var toString = ToStringHelper.Of(obj)
                .AddFields()
                .SkipNulls()
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj2 {age:30}");
        }

        [Test]
        public void ShouldBuildToStringFromMembers()
        {
            // given
            var obj = new TestObj3(id: 4, name: "TEST", values: null, age: 123, data: Lists.Singleton("X"),
                surname: "Zdzislawski", flag: BindingFlags.DeclaredOnly, objects: Lists.EmptyList<TestObj>());

            // when
            var toString = ToStringHelper.Of(obj)
                .AddMembers()
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj3 {Name:TEST, Id:4, Objects:[], Values:null, Flag:DeclaredOnly, Duration:null, age:123, surname:Zdzislawski, data:[X]}");
        }

        [Test]
        public void ShouldBuildToStringFromMembersWithoutNulls()
        {
            // given
            var obj = new TestObj3(id: 4, name: "TEST", values: null, age: 123, data: Lists.Singleton("X"),
                surname: null, flag: BindingFlags.DeclaredOnly, objects: null);

            // when
            var toString = ToStringHelper.Of(obj)
                .AddMembers()
                .SkipNulls()
                .ToString();

            // then
            Check.That(toString).IsEqualTo("TestObj3 {Name:TEST, Id:4, Flag:DeclaredOnly, age:123, data:[X]}");
        }

        [Test]
        public void ShouldGenerateToStringOfObject()
        {
            // given
            var listObject1 = new TestObj
            {
                Id = 3,
                Name = "listObject1"
            };
            var listObject2 = new TestObj
            {
                Id = 100,
                Objects = Lists.Singleton(listObject1)
            };
            var obj = new TestObj
            {
                Id = 1,
                Name = "Test",
                Flag = BindingFlags.CreateInstance,
                Objects = Lists.AsList(listObject1, listObject2),
                Duration = TimeSpan.FromSeconds(5)
            };
            var helper = ToStringHelper.Of(obj)
                .AddProperties()
                .SkipNulls()
                .GenerateToStringOfProperties();

            // when
            var toString = helper.ToString();

            const string expected =
                "TestObj {" +
                    "Name:Test, " +
                    "Id:1, " +
                    "Objects:[" +
                        "TestObj {Name:listObject1, Id:3, Flag:Default}, " +
                        "TestObj {Id:100, Objects:[" +
                            "TestObj {Name:listObject1, Id:3, Flag:Default}" +
                        "], " +
                        "Flag:Default}" +
                    "], " +
                    "Flag:CreateInstance, " +
                    "Duration:00:00:05}";
            Check.That(toString).IsEqualTo(expected);
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class TestObj
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public IList<TestObj> Objects { get; set; }
            public IList<int> Values { get; set; }
            public BindingFlags Flag { get; set; }
            public TimeSpan? Duration { get; set; }
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private class TestObj2
        {
            private int age;
            private string surname;
            private IList<string> data;

            internal TestObj2(int age, string surname, IList<string> data)
            {
                this.age = age;
                this.surname = surname;
                this.data = data;
            }
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private class TestObj3 : TestObj
        {
            private int age;
            private string surname;
            private IList<string> data;

            internal TestObj3(string name, int id, IList<TestObj> objects, IList<int> values, BindingFlags flag, int age, string surname, IList<string> data)
            {
                Name = name;
                Id = id;
                Objects = objects;
                Values = values;
                Flag = flag;
                this.age = age;
                this.surname = surname;
                this.data = data;
            }
        }
    }
}
