using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NFluent;
using NiceTry;
using NUnit.Framework;
using Sharpility.Base;
using Sharpility.Util;

namespace Sharpility.Tests.Base
{
    [TestFixture]
    public class PreconditionsTests
    {

        #region EvaulatePassTests

        [Test]
        public void ShouldPassEvaluationWithMessage()
        {
            // given
            const bool condition = true;
            const string errorMessage = "test";

            // when
            var result = Try.To(() => Preconditions.Evaluate(condition, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldPassEvaluationWithException()
        {
            // given
            const bool condition = true;
            const string errorMessage = "test";
            Func<ApplicationException> exception = () => new ApplicationException(errorMessage);

            // when
            var result = Try.To(() => Preconditions.Evaluate(condition, exception));

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
            var result = Try.To(() => Preconditions.Evaluate(condition, errorMessage));

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
            var result = Try.To(() => Preconditions.Evaluate(condition, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        #endregion EvaulatePassTests

        #region EvaulateFailTests

        [Test]
        public void ShouldThrowArgumentExceptionOnEvaluationWithMessage()
        {
            // given
            const bool condition = false;
            const string errorMessage = "test";

            // when
            var result = Try.To(() => Preconditions.Evaluate(condition, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnEvaluationWithException()
        {
            // given
            const bool condition = false;
            const string errorMessage = "test";
            Func<ApplicationException> exception = () => new ApplicationException(errorMessage);

            // when
            var result = Try.To(() => Preconditions.Evaluate(condition, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ApplicationException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnCallbackEvaluationWithException()
        {
            // given
            Func<bool> condition = () => false;
            const string errorMessage = "test";
            Func<ApplicationException> exception = () => new ApplicationException(errorMessage);

            // when
            var result = Try.To(() => Preconditions.Evaluate(condition, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ApplicationException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion EvaulateFailTests

        #region IsNotNullTests

        [Test]
        public void ShouldPassIsNotNullEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Value is not specified";
            var obj = new object();

            // when
            var result = Try.To(() => Preconditions.IsNotNull(obj, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsNotNullEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Value is not specified";
            const object obj = null;

            // when
            var result = Try.To(() => Preconditions.IsNotNull(obj, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassIsNotNullEvaluationWithException()
        {
            // given
            const string errorMessage = "Value is not specified";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            var obj = new object();

            // when
            var result = Try.To(() => Preconditions.IsNotNull(obj, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsNotNullEvaluationWithException()
        {
            // given
            const string errorMessage = "Value is not specified";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            const object obj = null;

            // when
            var result = Try.To(() => Preconditions.IsNotNull(obj, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion IsNotNullTests

        #region IsNullTests

        [Test]
        public void ShouldPassIsNullEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Value should be null";
            const object obj = null;

            // when
            var result = Try.To(() => Preconditions.IsNull(obj, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Value should be null";
            var obj = new object();

            // when
            var result = Try.To(() => Preconditions.IsNull(obj, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassIsNullEvaluationWithException()
        {
            // given
            const string errorMessage = "Value should be null";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            const object obj = null;

            // when
            var result = Try.To(() => Preconditions.IsNull(obj, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullEvaluationWithException()
        {
            // given
            const string errorMessage = "Value should be null";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            var obj = new object();

            // when
            var result = Try.To(() => Preconditions.IsNull(obj, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion IsNullTests

        #region IsStringNotEmptyTests

        [Test]
        public void ShouldPassIsStringNotEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "String cannot be empty";
            const string value = "abc";

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(value, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsStringNotEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "String cannot be empty";
            const string value = "";

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(value, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullStringNotEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "String cannot be empty";
            const string value = null;

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(value, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassIsStringNotEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "String cannot be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            const string value = "abc";

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(value, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsStringNotEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "String cannot be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            const string value = "";

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(value, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullStringNotEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "String cannot be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            const string value = null;

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(value, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion IsStringNotEmptyTests

        #region IsCollectionNotEmptyTests

        [Test]
        public void ShouldPassIsCollectionNotEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection cannot be empty";
            IEnumerable<int> enumerable = Lists.AsList(1, 2, 3);

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsCollectionNotEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection cannot be empty";
            IEnumerable<int> enumerable = Lists.EmptyList<int>();

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullCollectionNotEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection cannot be empty";
            IEnumerable<int> enumerable = null;

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassIsCollectionNotEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection cannot be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.AsList(1, 2, 3);

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsCollectionNotEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection cannot be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.EmptyList<int>();

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullCollectionNotEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection cannot be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = null;

            // when
            var result = Try.To(() => Preconditions.IsNotEmpty(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion IsCollectionNotEmptyTests

        #region IsCollectionEmptyTests

        [Test]
        public void ShouldPassIsCollectionEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection must be empty";
            IEnumerable<int> enumerable = Lists.EmptyList<int>();

            // when
            var result = Try.To(() => Preconditions.IsEmpty(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsCollectionEmptyEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection must be empty";
            IEnumerable<int> enumerable = Lists.AsList(1);

            // when
            var result = Try.To(() => Preconditions.IsEmpty(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassIsCollectionEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection must be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.EmptyList<int>();

            // when
            var result = Try.To(() => Preconditions.IsEmpty(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsCollectionEmptyEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection must be empty";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.Singleton(3);

            // when
            var result = Try.To(() => Preconditions.IsEmpty(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion IsCollectionEmptyTests

        #region IsSingletonTests

        [Test]
        public void ShouldPassIsSingletonEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            IEnumerable<string> enumerable = Lists.Singleton("A");

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsSingletonEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            IEnumerable<string> enumerable = Lists.AsList("A", "B");

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsEmptyCollectionSingletonEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            IEnumerable<string> enumerable = Lists.EmptyList<string>();

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullCollectionSingletonEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            IEnumerable<int> enumerable = null;

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassIsSingletonEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.Singleton(1);

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnIsSingletonEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.AsList(1, 2);

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsEmptyCollectionSingletonEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = Lists.EmptyList<int>();

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnIsNullCollectionSingletonEvaluationWithException()
        {
            // given
            const string errorMessage = "Collection must contains only one element";
            Func<InvalidProgramException> exception = () => new InvalidProgramException(errorMessage);
            IEnumerable<int> enumerable = null;

            // when
            var result = Try.To(() => Preconditions.IsSingleton(enumerable, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidProgramException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        #endregion IsSingletonTests

        #region EvaluateEnumTests

        [Test]
        public void ShouldPassEnumEvaluation()
        {
            // given
            const string value = "Test";

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }
        
        [Test]
        public void ShouldThrowExceptionOnInvalidEnumEvaluation()
        {
            // given
            const string value = "invalid";

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(
                "Invalid enum value: 'invalid' for Sharpility.Tests.Base.PreconditionsTests+TestEnum expected one of [Test, Test1, Test2]");
        }

        [Test]
        public void ShouldThrowExceptionOnNullEnumEvaluation()
        {
            // given
            const string value = null;

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(
                "Invalid enum value: 'null' for Sharpility.Tests.Base.PreconditionsTests+TestEnum expected one of [Test, Test1, Test2]");
        }

        [Test]
        public void ShouldPassEnumEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Invalid enum value";
            const string value = "Test";

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnInvalidEnumEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Invalid enum";
            const string value = "invalid";

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnNullEnumEvaluationWithMessage()
        {
            // given
            const string errorMessage = "Invalid enum";
            const string value = null;

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, errorMessage));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassEnumEvaluationWithException()
        {
            // given
            const string errorMessage = "Invalid enum value";
            const string value = "Test";
            Func<InvalidCastException> exception = () => new InvalidCastException(errorMessage);

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnInvalidEnumEvaluationWithException()
        {
            // given
            const string errorMessage = "Invalid enum";
            const string value = "invalid";
            Func<InvalidCastException> exception = () => new InvalidCastException(errorMessage);

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidCastException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldThrowExceptionOnNullEnumEvaluationWithException()
        {
            // given
            const string errorMessage = "Invalid enum";
            const string value = null;
            Func<InvalidCastException> exception = () => new InvalidCastException(errorMessage);

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, exception));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<InvalidCastException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage);
        }

        [Test]
        public void ShouldPassEnumEvaluationWithConverter()
        {
            // given
            const string errorMessage = "Invalid enum value expected: ";
            const string value = "Test";
            Converter<IEnumerator, Exception> converter =
                enumValues => new ArgumentException(errorMessage + Strings.ToString(enumValues));

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, converter));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNull();
        }

        [Test]
        public void ShouldThrowExceptionOnInvalidEnumEvaluationWithConverter()
        {
            // given
            const string errorMessage = "Invalid enum value expected: ";
            const string value = "invalid";
            Converter<IEnumerator, Exception> converter =
                enumValues => new ArgumentException(errorMessage + Strings.ToString(enumValues));

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, converter));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage + "[Test, Test1, Test2]");
        }

        [Test]
        public void ShouldThrowExceptionOnNullEnumEvaluationWithConverter()
        {
            // given
            const string errorMessage = "Invalid enum value expected: ";
            const string value = null;
            Converter<IEnumerator, Exception> converter =
                enumValues => new ArgumentException(errorMessage + Strings.ToString(enumValues));

            // when
            var result = Try.To(() => Preconditions.EvaluateEnum<TestEnum>(value, converter));

            // then
            var caughtException = result.IsFailure ? result.Error : null;
            Check.That(caughtException).IsNotNull();
            Check.That(caughtException).IsInstanceOf<ArgumentException>();
            Check.That(caughtException.Message).IsEqualTo(errorMessage + "[Test, Test1, Test2]");
        }

        #endregion EvaluateEnumTests

        private enum TestEnum
        {
            Test,
            Test1,
            Test2
        }
    }
}
