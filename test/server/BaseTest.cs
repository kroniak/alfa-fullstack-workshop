using Server.Services;

namespace ServerTest
{
    public class BaseTest
    {
        protected readonly ICardService cardService = new CardService();
    }
}
