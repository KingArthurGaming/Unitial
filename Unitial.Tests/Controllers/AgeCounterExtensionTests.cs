using System;
using Unitial.Services.Data;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class AgeCounterExtensionTests
    {
        [Fact]
        public void AgeShoudBeCorrect()
        {
            var date = DateTime.Now;

            date = date.AddYears(-16);
            date = date.AddDays(-1);

            var ageCalculator = AgeCounterExtension.GetAge(date);
            Assert.Equal(16, ageCalculator);
        }

        [Fact]
        public void AgeShoudBeIncorrect()
        {
            var date = DateTime.Now;

            date = date.AddYears(14);

            var ageCalculator = AgeCounterExtension.GetAge(date);
            Assert.NotEqual(16, ageCalculator);
        }

        [Fact]
        public void AgeOfNullShoudBeMinusOne()
        {

            var ageCalculator = AgeCounterExtension.GetAge(null);
            Assert.Equal(-1, ageCalculator);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void AgesShoudBeCorrect(int years)
        {
            var date = DateTime.Now;

            date = date.AddYears(-years);
            date = date.AddDays(-1);

            var ageCalculator = AgeCounterExtension.GetAge(date);
            Assert.Equal(years, ageCalculator);
        }
    }
}
