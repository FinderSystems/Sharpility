using System;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class PrecognitionsTests
    {
        [Test]
        public void ShouldPassEvalitionWithMessage()
        {
            // given
            const bool condition = true;
            const string errorMessage = "test";

            // when
            var result = Try.To(() => Precognitions.Evaluate(condition, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldPassEvalitionWithException()
        {
            // given
            const bool condition = true;
            const string errorMessage = "test";
            Func<ApplicationException> exception = () => new ApplicationException(errorMessage);

            // when
            var result = Try.To(() => Precognitions.Evaluate(condition, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldPassCallbackEvaluationWithMessage()
        {
            // given
            Func<bool> condition = () => true;
            const string errorMessage = "test";

            // when
            var result = Try.To(() => Precognitions.Evaluate(condition, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldPassCallbackEvaluationWitException()
        {
            // given
            Func<bool> condition = () => true;
            const string errorMessage = "test";
            Func<ApplicationException> exception = () => new ApplicationException(errorMessage);

            // when
            var result = Try.To(() => Precognitions.Evaluate(condition, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowArgumentExceptionOnEvaluationWithMessage()
        {
            // given
            const bool condition = false;
            const string errorMessage = "test";

            // when
            var result = Try.To(() => Precognitions.Evaluate(condition, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnEvaluation()
        {
            // given
            const bool condition = false;
            const string errorMessage = "test";
            Func<ApplicationException> exception = () => new ApplicationException(errorMessage);

            // when
            var result = Try.To(() => Precognitions.Evaluate(condition, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ApplicationException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }
    }
}
