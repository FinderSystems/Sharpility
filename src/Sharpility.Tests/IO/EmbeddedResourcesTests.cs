using System;
using System.Collections.Generic;
using System.IO;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.IO;

namespace Sharpility.Tests.IO
{
    [TestFixture]
    public class EmbeddedResourcesTests
    {
        [Test]
        public void ShouldReturnEmbeddedResourcesFromCurrentAssembly()
        {
            // given
            var classLoader = GetType();

            // when
            var resources = EmbeddedResources.Resources(classLoader);

            // then
            Check.That(resources).HasSize(3);
            Check.That(resources).Contains(
                "Sharpility.Tests.IO.Test1", 
                "Sharpility.Tests.IO.Test2", 
                "Sharpility.Tests.IO.Test3");
        }

        [Test]
        public void ShouldReturnFilteredEmbeddedResourcesFromCurrentAssembly()
        {
            // given
            var classLoader = GetType();
            Predicate<string> filter = resource => resource.EndsWith("Test1") || resource.EndsWith("Test2");

            // when
            var resources = EmbeddedResources.Resources(classLoader, filter);

            // then
            Check.That(resources).HasSize(2);
            Check.That(resources).Contains(
                "Sharpility.Tests.IO.Test1",
                "Sharpility.Tests.IO.Test2");
        }

        [Test]
        public void ShouldThrowExceptionWhenLoadingNotExistingResouce()
        {
            // given
            var classLoader = GetType();
            const string resource = "not.existing";

            // when
            var result = Try.To(() => EmbeddedResources.LoadResource(classLoader, resource));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<FileNotFoundException>();
            Check.That(caughtException.Message).IsEqualTo("Could not find resource: 'not.existing' at " + classLoader);
        }

        [Test, TestCaseSource("LoadEmbeddedResourceTestCases")]
        public string ShouldLoadEmbeddedResourceContent(string resource)
        {
            // given
            var classLoader = GetType();

            // when
            var content = EmbeddedResources.LoadResourceContent(classLoader, resource);

            // then
            return content;
        }

        private static IEnumerable<ITestCaseData> LoadEmbeddedResourceTestCases()
        {
            yield return new TestCaseData("Sharpility.Tests.IO.Test1")
                .SetName("Should return content for 'Sharpility.Tests.IO.Test1' embedded resource")
                .Returns("Test1");

            yield return new TestCaseData("Sharpility.Tests.IO.Test2")
                .SetName("Should return content for 'Sharpility.Tests.IO.Test2' embedded resource")
                .Returns("Test2");

            yield return new TestCaseData("Sharpility.Tests.IO.Test3")
                .SetName("Should return content for 'Sharpility.Tests.IO.Test3' embedded resource")
                .Returns("A\r\nB\r\nC");
        } 
    }
}
