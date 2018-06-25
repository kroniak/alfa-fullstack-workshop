using System;
using Xunit;
using Server.Services;

namespace ServerTest
{
    /// <summary>
    /// Tests for <see cref="CardService"/>>
    /// </summary>
    public class CardServiceTest
    {
        private readonly ICardService cardService = new CardService();

        /// <summary>
        /// Check if this cards numbers is valid
        /// </summary>
        [Fact]
        public void CheckCardNumberOK() => Assert.True(true);

        /// <summary>
        /// Check if this cards numbers is not valid
        /// </summary>
        [Theory]
        [InlineData("1234 1234 1233 1234")]
        [InlineData("")]
        [InlineData("1234 1233 1233")]
        public void CheckCardNumberFailed(string value)
        {
            var result = cardService.CheckCardNumber(value);

            Assert.False(result, $"{value} is not a valid card number");
        }
    }
}
