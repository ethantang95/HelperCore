﻿using HelperCore.Optional;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCoreTests.OptionalTests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestCreatingOptionalFromObject()
        {
            var dummy = Optional.From(new DummyClass());
        }

        [Test]
        public void TestCreatingOptionalFromValue()
        {
            var value = Optional.From(10);
        }

        [Test]
        public void TestGettingValidOptionalObject()
        {
            var dummy = CreateDummy();

            var optionalDummy = Optional.From(dummy);

            var optionalVal = optionalDummy.Value;

            Assert.AreEqual(dummy, optionalVal);
        }

        [Test]
        public void TestGettingValidOptionalValue()
        {
            var optionalValue = Optional.From(10);

            var optionalVal = optionalValue.Value;

            Assert.AreEqual(10, optionalVal);
        }

        [Test]
        public void TestGettingNullObject()
        {
            var optionalDummy = Optional<DummyClass>.FromNull();
            Assert.Throws<NullReferenceException>(() => { var val = optionalDummy.Value; });

            var optionalDummy2 = Optional<DummyClass>.From(null);
            Assert.Throws<NullReferenceException>(() => { var val = optionalDummy2.Value; });
        }

        [Test]
        public void TestGettingNullValue()
        {
            var optionalValue = Optional<int>.FromNull();
            Assert.Throws<NullReferenceException>(() => { var val = optionalValue.Value; });
        }

        [Test]
        public void TestCheckingContainsObject()
        {
            var optionalDummy = Optional.From(new DummyClass());

            Assert.True(optionalDummy.Present);
        }

        [Test]
        public void TestCheckingContainsValue()
        {
            var optionalValue = Optional.From(10);

            Assert.True(optionalValue.Present);
        }

        [Test]
        public void TestCheckingContainsNullObject()
        {
            var optionalDummy = Optional<DummyClass>.FromNull();
            Assert.False(optionalDummy.Present);

            var optionalDummy2 = Optional<DummyClass>.From(null);
            Assert.False(optionalDummy2.Present);
        }

        [Test]
        public void TestCheckingContainsNullValue()
        {
            var optionalValue = Optional<int>.FromNull();
            Assert.False(optionalValue.Present);
        }

        [Test]
        public void TestOrElseHasObject()
        {
            var dummy = CreateDummy();
            var elseDummy = CreateDummy(2);

            var optionalDummy = Optional.From(dummy);

            Assert.AreEqual(dummy, optionalDummy.OrElse(elseDummy));
        }

        [Test]
        public void TestOrElseHasValue()
        {
            var optionalValue = Optional.From(10);

            Assert.AreEqual(10, optionalValue.OrElse(5));
        }

        [Test]
        public void TestOrElseNoObject()
        {
            var elseDummy = CreateDummy(2);

            var optionalDummy = Optional<DummyClass>.FromNull();

            Assert.AreEqual(elseDummy, optionalDummy.OrElse(elseDummy));

            var optionalDummy2 = Optional<DummyClass>.From(null);

            Assert.AreEqual(elseDummy, optionalDummy2.OrElse(elseDummy));
        }

        [Test]
        public void TestOrElseNoValue()
        {
            var optionalValue = Optional<int>.FromNull();

            Assert.AreEqual(5, optionalValue.OrElse(5));
        }

        [Test]
        public void TestDefaultHasObject()
        {
            var dummy = CreateDummy();

            var optionalDummy = Optional.From(dummy);

            Assert.AreEqual(dummy, optionalDummy.OrDefault());
        }

        [Test]
        public void TestDefaultHasValue()
        {
            var optionalValue = Optional.From(10);

            Assert.AreEqual(10, optionalValue.OrDefault());
        }

        [Test]
        public void TestDefaultNoObject()
        {
            var optionalDummy = Optional<DummyClass>.FromNull();
            Assert.AreEqual(default(DummyClass), optionalDummy.OrDefault());

            var optionalDummy2 = Optional<DummyClass>.From(null);
            Assert.AreEqual(default(DummyClass), optionalDummy2.OrDefault());
        }

        [Test]
        public void TestDefaultNoValue()
        {
            var optionalValue = Optional<int>.FromNull();
            Assert.AreEqual(default(int), optionalValue.OrDefault());
        }

        [Test]
        public void TestChainning()
        {
            var dummy = CreateDummy();
            dummy.OptionalString = Optional.From("world");

            var optionalDummy = Optional.From(dummy);

            Assert.AreEqual("world", optionalDummy.Value.OptionalString.Value);
        }

        [Test]
        public void TestEquals()
        {
            var optionalValue = Optional.From(5);

            Assert.True(optionalValue.Equals(5));
        }

        [Test]
        public void TestNullOptionalEquals()
        {
            var optionalValue = Optional<int>.FromNull();

            Assert.False(optionalValue.Equals(5));
        }

        [Test]
        public void TestNullObjEquals()
        {
            var optionalValue = Optional.From(5);

            Assert.False(optionalValue.Equals(null));
        }

        [Test]
        public void TestHashCode()
        {
            var optionalValue = Optional.From(5);

            Assert.AreEqual(5.GetHashCode(), optionalValue.GetHashCode());
        }

        [Test]
        public void TestNullHashCode()
        {
            var optionalValue = Optional<int>.FromNull();

            Assert.AreEqual(0, optionalValue.GetHashCode());
        }

        private DummyClass CreateDummy()
        {
            return CreateDummy(0);
        }

        private DummyClass CreateDummy(int value)
        {
            var dummy = new DummyClass();
            dummy.IntValue = value;
            dummy.StringValue = "hello";
            return dummy;
        }
    }

    internal class DummyClass
    {
        public int IntValue { get; set; }
        public string StringValue { get; set; }
        public Optional<string> OptionalString { get; set; }
    }
}
