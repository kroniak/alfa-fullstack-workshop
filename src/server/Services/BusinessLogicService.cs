using System;
using System.Linq;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Models;

namespace Server.Services
{
    /// <summary>
    /// Business Logic Service
    /// </summary>
    public class BusinessLogicService : IBusinessLogicService
    {
        /// <summary>
        /// Convert currency from to
        /// </summary>
        /// <param name="sum">Sum for convert</param>
        /// <param name="from">Convert from this currency</param>
        /// <param name="to">Convert to this currency</param>
        /// <returns>Converted sum on <see langword="decimal"/></returns>
        public decimal GetConvertSum(decimal sum, Currency from, Currency to)
        {
            if (from == to) return sum;

            return sum * Constants.Currencies[from] / Constants.Currencies[to];
        }

        /// <summary>
        /// Add bonus o new card
        /// </summary>
        /// <param name="card">Card to</param>
        public void AddBonusOnOpen(Card card)
        {
            if (card == null)
                throw new BusinessLogicException(TypeBusinessException.CARD, "Card is null", "Карта не найдена");

            card.AddTransaction(new Transaction(10M, card));
        }

        /// <summary>
        /// Check Card expired or not
        /// </summary>
        /// <param name="card">Card for cheking</param>
        /// <returns><see langword="true"/> if card is active</returns>
        public bool CheckCardActivity(Card card)
        {
            if (card == null)
                throw new BusinessLogicException(TypeBusinessException.CARD, "Card is null", "Карта не найдена");

            return card.DTOpenCard.AddYears(card.Validity) <= DateTime.Today;
        }

        /// <summary>
        /// Get balance of the card
        /// </summary>
        /// <param name="card">Card to calculaing</param>
        /// <returns><see langword="decimal"/> sum</returns>
        public decimal GetBalanceOfCard(Card card)
        {
            if (card == null)
                throw new BusinessLogicException(TypeBusinessException.CARD, "Card is null", "Карта не найдена");

            return card.Transactions.Sum(x => x.Sum);
        }

        /// <summary>
        /// Generate new card number from BIN list
        /// </summary>
        /// <param name="cardType">Type of the card/param>
        /// <returns>Generated new card number</returns>
        public string GenerateNewCardNumber(CardType cardType)
        {
            throw new System.NotImplementedException();
        }
    }
}