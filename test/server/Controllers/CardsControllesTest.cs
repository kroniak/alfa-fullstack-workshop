using Server.Controllers;
using Server.Data;
using Xunit;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Server.Models;
using Server.Services;

namespace ServerTest.ControllersTest
{
    public class CardsControllerTest
    {
        private readonly ICardService _cardService = new CardService();
        private readonly IBusinessLogicService _businessLogicService = new BusinessLogicService(new CardService());

        [Fact]
        public void GetCardsPassed()
        {
            // Arrange
            var mock = new Mock<IBankRepository>();
            var mockCards = new FakeDataGenerator(_businessLogicService).GenerateFakeCards();

            mock.Setup(r => r.GetCards()).Returns(mockCards);

            var controller = new CardsController(mock.Object, _cardService, _businessLogicService);

            // Test
            var cards = controller.Get();

            // Assert
            mock.Verify(r => r.GetCards(), Times.AtMostOnce());
            Assert.Equal(3, cards.Count());
        }
    }
}