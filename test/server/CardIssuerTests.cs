﻿using Server.Services;
using Xunit;

namespace ServerTest
{
    /// <summary>
    /// Test card issuers
    /// </summary>
    public class CardIssuerTests : BaseTest
    {
        #region Positive cases
        /// <summary>
        /// Check valid for alfa bank ard number
        /// </summary>
        [Theory]
        [InlineData("4390 7700 0000 0123")]
        public void TestAlfaBankCard(string cardNumber)
        {
            bool isAlfaBankIssuer = cardService.CheckCardIssuer(cardNumber);
            Assert.True(isAlfaBankIssuer);
        }

        /// <summary>
        /// Check card number from another bank issuer
        /// </summary>
        [Theory]
        [InlineData("4111 1111 1111 1111")]
        public void TestNonAlfaBankCard(string cardNumber)
        {
            bool isAlfaBankIssuer = cardService.CheckCardIssuer(cardNumber);
            Assert.False(isAlfaBankIssuer);
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
            bool isAlfaBankIssuer = cardService.CheckCardIssuer(cardNumber);
            Assert.False(isAlfaBankIssuer);
        }

        #endregion

    }
}
