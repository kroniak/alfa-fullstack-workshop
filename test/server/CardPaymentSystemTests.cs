using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Services;
using Xunit;

namespace ServerTest
{
    public class CardPaymentSystemTests
    {
        private readonly ICardService cardService = new CardService();

        #region Positive cases
        /// <summary>
        /// Test all maintainable payment systems
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="expectedPaymentSystem"></param>
        [Theory]
        [InlineData("4111 1111 1111 1111", CardPaymentSystem.Visa)]
        [InlineData("5555 5555 5555 5599", CardPaymentSystem.Mastercard)]
        [InlineData("6390 0250 90024 82332", CardPaymentSystem.Maestro)]
        [InlineData("4665 0312 3456 7890", CardPaymentSystem.Electron)]
        public void TestMaintainablePaymentSystems(string cardNumber, 
            CardPaymentSystem expectedPaymentSystem)
        {
            Assert.Equal(expectedPaymentSystem, cardService.CardPaymentSystemExtract(cardNumber));
        }

        /// <summary>
        /// Test American express
        /// </summary>
        /// <param name="cardNumber"></param>
        [Theory]
        [InlineData("3751 1700 0000 000")]
        public void TestNonMaintainablePaymentSystems(string cardNumber)
        {
            Assert.Equal(CardPaymentSystem.Invalid,
                cardService.CardPaymentSystemExtract(cardNumber));
        }


        #endregion

        #region Negative cases

        [Theory]
        [InlineData("")]
        [InlineData("1234 1233 1233")]
        [InlineData("1234asd 1234 1233 1234")]
        [InlineData("1234 1234 1233$^& 1234")]
        public void TestWrongFormatCardNumber(string cardNumber)
        {
            Assert.Equal(CardPaymentSystem.Invalid,
                cardService.CardPaymentSystemExtract(cardNumber));
        }

        #endregion



    }
}
