
using System;
using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Extensions
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        public void ShouldExtendListByAddAllMethod()
        {
            // given
            IList<string> list = new List<string>();

            // when
            list.AddAll(Lists.AsList("A", "B", "C"));
            list.AddAll("D", "E", "F");

            // then
            Check.That(list).ContainsExactly("A", "B", "C", "D", "E", "F");
        }

        [Test]
        public void ShouldExtendSetByAddAllMethod()
        {
            // given
            ISet<string> set = new HashSet<string>();

            // when
            set.AddAll(Lists.AsList("A", "B", "C"));
            set.AddAll("D", "E", "F");

            // then
            Check.That(set).HasSize(6);
            Check.That(set).Contains("A", "B", "C", "D", "E", "F");
        }

        [Test]
        public void ShouldExtendListByRemoveAllMethod()
        {
            // given
            IList<string> list = new List<string> { "A", "B", "C", "D", "E", "F" };

            // when
            list.RemoveAll(Lists.AsList("A", "B"));
            list.RemoveAll("E", "F");

            // then
            Check.That(list).ContainsExactly("C", "D");
        }

        [Test]
        public void ShouldExtendSetByRemoveAllMethod()
        {
            // given
            ISet<string> set = new HashSet<string> { "A", "B", "C", "D", "E", "F" };

            // when
            set.RemoveAll(Lists.AsList("A", "B"));
            set.RemoveAll("E", "F");

            // then
            Check.That(set).HasSize(2);
            Check.That(set).Contains("C", "D");
        }

        [Test]
        public void ShouldExtendListByConvertAllMethod()
        {
            // given
            IList<int> list = new List<int> {1, 2, 3, 4};
            Converter<int, string> converter = item => item.ToString();

            // when
            var result = list.ConvertAll(converter);

            // then
            Check.That(result).ContainsExactly("1", "2", "3", "4");
        }

        [Test]
        public void ShouldExtendSetByConvertAllMethod()
        {
            // given
            ISet<int> set = new HashSet<int> { 1, 2, 3, 4 };
            Converter<int, string> converter = item => item.ToString();

            // when
            var result = set.ConvertAll(converter);

            // then
            Check.That(result).HasSize(4);
            Check.That(result).Contains("1", "2", "3", "4");
        }
    }
}
