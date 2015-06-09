
using System.Collections.Immutable;
using NFluent;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class DictionariesTests
    {
        [Test]
        public void ShouldCreateDictionaryFromEntries()
        {
            // given
            var entries = new []
            {
                Dictionaries.Entry(1, "A"),
                Dictionaries.Entry(2, "B"),
                Dictionaries.Entry(3, "C")
            };

            // when
            var dictionary = Dictionaries.CreateFromEntries(entries);

            // then
            Check.That(dictionary).HasSize(3);
            Check.That(dictionary).Contains(entries);
        }

        [Test]
        public void ShouldCreateEmptyDictionary()
        {
            // when
            var dictionary = Dictionaries.Empty<string, int>();

            // then
            Check.That(dictionary).IsEmpty();
        }

        # region QuickDictionaryCreateTests

        [Test]
        public void ShouldQuickCreateDictionary1()
        {
            // when
            var dictionary = Dictionaries.Create(1, "A");

            // then
            Check.That(dictionary).HasSize(1);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
        }

        [Test]
        public void ShouldQuickCreateDictionary2()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B");

            // then
            Check.That(dictionary).HasSize(2);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
        }

        [Test]
        public void ShouldQuickCreateDictionary3()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C");

            // then
            Check.That(dictionary).HasSize(3);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
        }

        [Test]
        public void ShouldQuickCreateDictionary4()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D");

            // then
            Check.That(dictionary).HasSize(4);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
        }

        [Test]
        public void ShouldQuickCreateDictionary5()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E");

            // then
            Check.That(dictionary).HasSize(5);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
        }

        [Test]
        public void ShouldQuickCreateDictionary6()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F");

            // then
            Check.That(dictionary).HasSize(6);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
        }

        [Test]
        public void ShouldQuickCreateDictionary7()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G");

            // then
            Check.That(dictionary).HasSize(7);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
        }

        [Test]
        public void ShouldQuickCreateDictionary8()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G",
                8, "H");

            // then
            Check.That(dictionary).HasSize(8);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
            Check.That(dictionary).Contains(Dictionaries.Entry(8, "H"));
        }

        [Test]
        public void ShouldQuickCreateDictionary9()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G",
                8, "H",
                9, "J");

            // then
            Check.That(dictionary).HasSize(9);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
            Check.That(dictionary).Contains(Dictionaries.Entry(8, "H"));
            Check.That(dictionary).Contains(Dictionaries.Entry(9, "J"));
        }

        [Test]
        public void ShouldQuickCreateDictionary10()
        {
            // when
            var dictionary = Dictionaries.Create(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G",
                8, "H",
                9, "J",
                10, "I");

            // then
            Check.That(dictionary).HasSize(10);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
            Check.That(dictionary).Contains(Dictionaries.Entry(8, "H"));
            Check.That(dictionary).Contains(Dictionaries.Entry(9, "J"));
            Check.That(dictionary).Contains(Dictionaries.Entry(10, "I"));
        }

        # endregion QuickDictionaryCreateTests

        # region QuickImmutableDictionaryCreateTests

        [Test]
        public void ShouldQuickCreateImmutableDictionary1()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(1, "A");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(1);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary2()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(2);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary3()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C");

            // then
            Check.That(dictionary).HasSize(3);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary4()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(4);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary5()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(5);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary6()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(6);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary7()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(7);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary8()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G",
                8, "H");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(8);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
            Check.That(dictionary).Contains(Dictionaries.Entry(8, "H"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary9()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G",
                8, "H",
                9, "J");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(9);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
            Check.That(dictionary).Contains(Dictionaries.Entry(8, "H"));
            Check.That(dictionary).Contains(Dictionaries.Entry(9, "J"));
        }

        [Test]
        public void ShouldQuickCreateImmutableDictionary10()
        {
            // when
            var dictionary = Dictionaries.CreateImmutable(
                1, "A",
                2, "B",
                3, "C",
                4, "D",
                5, "E",
                6, "F",
                7, "G",
                8, "H",
                9, "J",
                10, "I");

            // then
            Check.That(dictionary).IsInstanceOf<ImmutableDictionary<int, string>>();
            Check.That(dictionary).HasSize(10);
            Check.That(dictionary).Contains(Dictionaries.Entry(1, "A"));
            Check.That(dictionary).Contains(Dictionaries.Entry(2, "B"));
            Check.That(dictionary).Contains(Dictionaries.Entry(3, "C"));
            Check.That(dictionary).Contains(Dictionaries.Entry(4, "D"));
            Check.That(dictionary).Contains(Dictionaries.Entry(5, "E"));
            Check.That(dictionary).Contains(Dictionaries.Entry(6, "F"));
            Check.That(dictionary).Contains(Dictionaries.Entry(7, "G"));
            Check.That(dictionary).Contains(Dictionaries.Entry(8, "H"));
            Check.That(dictionary).Contains(Dictionaries.Entry(9, "J"));
            Check.That(dictionary).Contains(Dictionaries.Entry(10, "I"));
        }

        
        # endregion QuickImmutableDictionaryCreateTests
    }
}
