using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;
using System;

namespace FizzBuzz.Tests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void PrintAllNumbers_PrintNumbersInConsole()
        {
            using var consoleOutput = new ConsoleOutput();
            FizzBuzz.PrintAllNumbers();

            Assert.That(consoleOutput.GetOuput(), Is.Not.Empty);
        }

        [Test]
        public void GetFizzOrBuzz_ValidValue_ReturnsFizzOrBuzzBaseOnNumber()
        {
            var shouldBeNumber = FizzBuzz.GetFizzOrBuzz(1);
            var shouldBeFizz = FizzBuzz.GetFizzOrBuzz(3);
            var shouldBeBuzz = FizzBuzz.GetFizzOrBuzz(5);
            var shouldBeFizzBuzz = FizzBuzz.GetFizzOrBuzz(15);
            Assert.Multiple(() =>
            {
                Assert.That(shouldBeNumber, Is.EqualTo("1"));
                Assert.That(shouldBeFizz, Is.EqualTo("Fizz"));
                Assert.That(shouldBeBuzz, Is.EqualTo("Buzz"));
                Assert.That(shouldBeFizzBuzz, Is.EqualTo("FizzBuzz"));
            });
        }

        [Test]
        public void IsInRange_ValueInRange_ReturnsTrue()
        {
            for (int i = 1; i <= 100; i++)
            {
                var isInRange = FizzBuzz.IsInRange(i);
                Assert.That(isInRange);
            }
        }

        [Test]
        public void IsInRange_ValueOutOfRange_ReturnsFalse()
        {
            var firstOutOfRangeValue = -1;
            var secondOutOfRangeValue = 101;

            var isInRange = FizzBuzz.IsInRange(firstOutOfRangeValue);
            Assert.That(!isInRange);

            isInRange = FizzBuzz.IsInRange(secondOutOfRangeValue);
            Assert.That(!isInRange);
        }
    }
}