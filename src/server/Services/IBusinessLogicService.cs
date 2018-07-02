using Server.Infrastructure;
using Server.Models;

namespace Server.Services
{
    /// <summary>
    /// Interface for Business Logic Service
    /// </summary>
    public interface IBusinessLogicService
    {
        /// <summary>
        /// Convert currency from to
        /// </summary>
        /// <param name="sum">Sum for convert</param>
        /// <param name="from">Convert from this currency</param>
        /// <param name="to">Convert to this currency</param>
        /// <returns>Converted sum on <see langword="decimal"/></returns>
        decimal GetConvertSum(decimal sum, Currency from, Currency to);

        /// <summary>
        /// Generate new card number from BIN list
        /// </summary>
        /// <param name="cardType">Type of the card/param>
        /// <returns>Generated new card number</returns>
        string GenerateNewCardNumber(CardType cardType);

        /// <summary>
        /// Add bonus o new card
        /// </summary>
        /// <param name="card">Card to</param>
        void AddBonusOnOpen(Card card);

        /// <summary>
        /// Check Card expired or not
        /// </summary>
        /// <param name="card">Card for cheking</param>
        /// <returns><see langword="true"/> if card is active</returns>
        bool CheckCardActivity(Card card);

        /// <summary>
        /// Get balance of the card
        /// </summary>
        /// <param name="card">Card to calculaing</param>
        /// <returns><see langword="decimal"/> sum</returns>
        decimal GetBalanceOfCard(Card card);
    }
}