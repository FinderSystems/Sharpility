using System.Collections.Generic;
using System.IO;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.IO;

namespace Sharpility.Tests.IO
{
    [TestFixture]
    public class FilesTests
    {
        [Test]
        public void ShouldDeleteExistingFile()
        {
            // given
            var file = Path.GetTempFileName();
            using (File.Create(file)) { }

            // when
            Files.DeleteIfExists(file);

            // then
            Check.That(File.Exists(file)).IsFalse();
        }

        [Test]
        public void ShouldDeleteDirectoryRecursively()
        {
            // given
            var tempPath = Path.GetTempPath();
            var directory = Files.Path(tempPath, "testdir");
            var file1 = Files.Path(directory, "file1");
            var file2 = Files.Path(directory, "file2");
            Files.TryDeleteDirectoryRecursive(directory);
            Directory.CreateDirectory(directory);
            using (File.Create(file1)) { }
            using (File.Create(file2)) { }

            // when
            var result = Files.DeleteDirectoryRecursiveIfExists(directory);

            // then
            Check.That(result).IsTrue();
            Check.That(Directory.Exists(directory)).IsFalse();
            Check.That(File.Exists(file1)).IsFalse();
            Check.That(File.Exists(file2)).IsFalse();
        }

        [Test]
        public void ShouldIgnoreDeletionOfNotExistingFile()
        {
            // given
            const string file = "notexisting";

            // when
            var result = Try.To(() => Files.DeleteIfExists(file));

            // then
            var passed = result.IsSuccess;
            Check.That(passed).IsTrue();
        }

        [Test]
        public void ShouldIgnoreDeletionOfNotExistingDirectory()
        {
            // given
            const string directory = "notexisting";

            // when
            var result = Try.To(() => Files.DeleteDirectoryRecursiveIfExists(directory));

            // then
            var passed = result.IsSuccess;
            Check.That(passed).IsTrue();
        }

        [Test, TestCaseSource("TrimEndingDirectorySeparatorTestCases")]
        public string ShouldTrimEndingDirectorySeparator(string path)
        {
            // when
            var result = Files.TrimEndingDirectorySeparator(path);

            // then
            return result;
        }

        private static IEnumerable<ITestCaseData> TrimEndingDirectorySeparatorTestCases()
        {
            var separator = Path.DirectorySeparatorChar.ToString();
            yield return new TestCaseData("c:" + separator + "directory" + separator)
                .SetName("Should remove ending separator from path")
                .Returns("c:" + separator + "directory");

            yield return new TestCaseData("c:" + separator + "directory")
                .SetName("Should ignore removing ending separator from path")
                .Returns("c:" + separator + "directory");

            yield return new TestCaseData(separator)
                .SetName("Should remove ending separator from path")
                .Returns("");

            yield return new TestCaseData("")
                .SetName("Should ignore removing ending separator from empty path")
                .Returns("");
        }

        [Test, TestCaseSource("BuildFilePathTestCases")]
        public string ShouldBuildFilePath(string[] paths)
        {
            // when
            var path = Files.Path(paths);

            // then
            return path;
        }

        private static IEnumerable<ITestCaseData> BuildFilePathTestCases()
        {
            var separator = Path.DirectorySeparatorChar.ToString();
            yield return new TestCaseData((object) new [] {"c:", "mydir", "myfile"})
                .Returns("c:" + separator + "mydir" + separator + "myfile");

            yield return new TestCaseData((object) new[] { "c:" + separator, "mydir" + separator })
               .Returns("c:" + separator + "mydir");
        }
    }
}
