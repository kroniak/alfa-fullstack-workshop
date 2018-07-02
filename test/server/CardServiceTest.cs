using System;
using Xunit;
using Server.Services;
using Server.Infrastructure;

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
        [Theory]
        [InlineData("4083967629457310")]
        [InlineData("5395 0290 0902 1990")]
        [InlineData("   4978 588211036789    ")]
        public void CheckCardNumberPassed(string value)
            => Assert.True(cardService.CheckCardNumber(value), $"{value} is not a valid card number");

        /// <summary>
        /// Check if this cards numbers is not valid
        /// </summary>
        [Theory]
        [InlineData("1234 1234 1233 1234")]
        [InlineData("12341233123")]
        [InlineData("")]
        [InlineData(null)]
        public void CheckCardNumberFailed(string value)
            => Assert.False(cardService.CheckCardNumber(value), $"{value} is a valid card number");

        /// <summary>
        /// Check if this cards numbers is not emitted by AlfaBank
        /// </summary>
        [Theory]
        [InlineData("5395029009021990")]
        [InlineData("4978588211036789")]
        public void CheckCardEmittedFailed(string value)
            => Assert.False(cardService.CheckCardEmmiter(value), $"{value} is a valid Alfabank card number");

        /// <summary>
        /// Check if this cards numbers is not emitted by AlfaBank
        /// </summary>
        [Theory]
        [InlineData("4083969259636239")]
        [InlineData("5101265622568232")]
        public void CheckCardEmittedPassed(string value)
            => Assert.True(cardService.CheckCardEmmiter(value), $"{value} is a not valid Alfabank card number");

        /// <summary>
        /// Extract card type test
        /// </summary>
        [Theory]
        [InlineData("4083969259636239", CardType.VISA)]
        [InlineData("5308276794485221", CardType.MASTERCARD)]
        [InlineData("6762302693240520", CardType.MAESTRO)]
        [InlineData("6762502693240520", CardType.UNKNOWN)]
        public void CardTypeExtract(string number, CardType type)
            => Assert.Equal(type, cardService.CardTypeExtract(number));
    }
}
