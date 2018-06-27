using Server.Services;
using Xunit;
namespace ServerTest
{
    public class CardNumberTests
    {
        private readonly ICardService cardService = new CardService();


        #region Positive cases

        [Theory]
        [InlineData("4111 1111 1111 1111")]
        public void TestLuhnAlgorithmWithValidCard(string cardNumber)
        {
            Assert.True(cardService.CheckCardNumber(cardNumber));
        }

        [Theory]
        [InlineData("4111 1111 1111 1112")]
        public void TestLuhnAlgorithmWithNotValidCard(string cardNumber)
        {
            Assert.False(cardService.CheckCardNumber(cardNumber));
        }

        #endregion

        #region Negative cases

        [Theory]
        [InlineData("")]
        [InlineData("1234 1233 1233")]
        [InlineData("1234asd 1234 1233 1234")]
        [InlineData("1234 1234 1233$^& 1234")]
        public void TestLuhnAlgorithmWithWrongFormatCard(string cardNumber)
        {
            Assert.False(cardService.CheckCardNumber(cardNumber));
        }

        #endregion

        

    }
}
