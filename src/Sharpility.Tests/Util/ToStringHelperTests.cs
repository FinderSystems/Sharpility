
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
                Objects = Lists.AsList(listObject1, listObject2)
            };
            var helper = ToStringHelper.Of(obj)
                .AddProperties()
                .SkipNulls()
                .GenerateToStringOfSubProperties();

            // when
            var toString = helper.ToString();

            const string expected =
                "TestObj {" +
                    "Name:Test, " +
                    "Id:1, " +
                    "Objects:[" +
                        "TestObj {Name:listObject1, Id:3, Flag:BindingFlags {}}, " +
                        "TestObj {Id:100, Objects:[" +
                            "TestObj {Name:listObject1, Id:3, Flag:BindingFlags {}}" +
                        "], " +
                        "Flag:BindingFlags {}}" +
                    "], " +
                    "Flag:BindingFlags {}}";
            Check.That(toString).IsEqualTo(expected);

        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class TestObj
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public IList<TestObj> Objects { get; set; }
            public BindingFlags Flag { get; set; }
        }
    }
}
