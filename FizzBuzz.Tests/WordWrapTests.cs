using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Katas.Tests
{
    [TestFixture]
    public class WordWrapTests
    {
        [Test]
        public void GetCountOfNonSpaceSplitters_ValueIsNull_ReturnsZero()
        {
            string[] nullValue = null;
            var countOfSplitters = WordWrap.GetCountOfNonSpaceSplitters(ref nullValue);

            Assert.That(countOfSplitters, Is.EqualTo(0));
        }

        [Test]
        public void GetCountOfNonSpaceSplitters_AllValuesAreValid_ReturnsCountOfValidSplitters()
        {
            var baseValue = new string[] { "test", "equals", "null" };
            var splitters = new string[3];
            Array.Copy(baseValue, splitters, baseValue.Length);

            var countOfSplitters = WordWrap.GetCountOfNonSpaceSplitters(ref splitters);

            Assert.Multiple(() =>
            {
                Assert.That(countOfSplitters, Is.EqualTo(3));
                Assert.That(splitters, Has.Length.EqualTo(3));
                Assert.That(splitters, Is.EqualTo(baseValue));
            });
        }

        [Test]
        public void GetCountOfNonSpaceSplitters_SomeValuesAreValid_ReturnsCountOfValidSplitters()
        {
            var baseValue = new string[] { "tes t", "equals", "nu\nll", "some value", "valid\tvalue" };
            var splitters = new string[baseValue.Length];
            Array.Copy(baseValue, splitters, baseValue.Length);

            var countOfSplitters = WordWrap.GetCountOfNonSpaceSplitters(ref splitters);

            Assert.Multiple(() =>
            {
                Assert.That(countOfSplitters, Is.EqualTo(2));
                Assert.That(splitters, Has.Length.EqualTo(2));
                Assert.That(splitters, Is.Not.EqualTo(baseValue));
            });
        }

        [Test]
        public void Wrap_StringToWrapIsNull_ThrowNullReferenseException()
        {
            Assert.That(() => WordWrap.Wrap(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Wrap_StringToWrapIsOkSplittersAreNull_ReturnsOriginalString()
        {
            var stringToWrap = "Some awesome string";

            var wrappedString = WordWrap.Wrap(stringToWrap, null);

            Assert.That(wrappedString, Is.EqualTo(stringToWrap));
        }

        [Test]
        public void Wrap_AllValuesAreOk_ReturnsWrappedString()
        {
            var stringToWrap = "Some awesome string";
            var expectedString = "Some awesome\nstring";


            var wrappedString = WordWrap.Wrap(stringToWrap, "awesome");

            Assert.That(wrappedString, Is.EqualTo(expectedString));
        }

        [Test]
        public void Wrap_StringToWrapIsOkSplittersAreNotValid_ReturnsWrappedString()
        {
            var stringToWrap = "Some awesome string";


            var wrappedString = WordWrap.Wrap(stringToWrap, "awesome ", "Some\n");

            Assert.That(wrappedString, Is.EqualTo(stringToWrap));
        }
    }
}
