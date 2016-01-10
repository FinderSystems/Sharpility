using System;
using System.Diagnostics.CodeAnalysis;
using NFluent;
using NUnit.Framework;
using Sharpility.Extensions;
using Sharpility.Util;

namespace Sharpility.Tests.Util
{
    [TestFixture]
    public class InstanceCreatorTests
    {
        [Test]
        public void ShouldCreateInstanceOfSimpleObject()
        {
            // when
            var instance = InstanceCreator.CreateIntance<SimpleObject>();

            // then
            Check.That(instance).IsNotNull();
        }

        [Test]
        public void ShouldCreateInstanceOfObjectWithPrivateConstructor()
        {
            // when
            var instance = InstanceCreator.CreateIntance<PrivateConstructor>();

            // then
            Check.That(instance).IsNotNull();
        }

        [Test]
        public void ShouldCreateInstanceOfObjectWithoutDefaultConstrutor()
        {
            // when
            var instance = InstanceCreator.CreateIntance<NoDefaultConstructor>();

            // then
            Check.That(instance).IsNotNull();
            Check.That(instance)
                .IsEqualTo(new NoDefaultConstructor(a: null, b: 0, timeSpan: default(TimeSpan), obj: null));
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class SimpleObject
        {
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class PrivateConstructor
        {
            private PrivateConstructor()
            {
            }
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private class NoDefaultConstructor
        {
            private int b;
            private string a;
            private TimeSpan timeSpan;
            private SimpleObject obj;

            internal NoDefaultConstructor(int b, string a, TimeSpan timeSpan, SimpleObject obj)
            {
                this.b = b;
                this.a = a;
                this.timeSpan = timeSpan;
                this.obj = obj;
            }

            public override bool Equals(object obj)
            {
                return this.EqualsByFields(obj);
            }

            public override int GetHashCode()
            {
                return this.FieldsHash();
            }

            public override string ToString()
            {
                return this.FieldsToString();
            }
        }
    }
}
