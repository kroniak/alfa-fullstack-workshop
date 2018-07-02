using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// Transaction domain model
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Sum in transaction
        /// </summary>
        /// <returns><see langword="decimal"/>representation of the sum transaction</returns>
        public decimal Sum { get; }

        /// <summary>
        /// Link to valid card
        /// </summary>
        /// <returns><see cref="Card"/></returns>
        public Card CardFrom { get; }

        /// <summary>
        /// Link to valid card
        /// </summary>
        /// <returns><see cref="Card"/></returns>
        public Card CardTo { get; }

        private readonly IBusinessLogicService blService = new BusinessLogicService();

        /// <summary>
        /// Create new transaction with validation
        /// </summary>
        /// <param name="sum">Sum of the transaction</param>
        /// <param name="from">Link card from</param>
        /// <param name="to">Link card to</param>
        public Transaction(decimal sum, Card from, Card to)
        {
            if (from == null)
                throw new BusinessLogicException(TypeBusinessException.TRANSACTION,
                "From card is null", "Не найдена карта с которой совершается перевод");

            if (to == null)
                throw new BusinessLogicException(TypeBusinessException.TRANSACTION,
                "To card is null", "Не найдена карта на которую совершается перевод");

            if (sum <= 0)
                throw new UserDataException("Transaction need more then 0", $"from {from.CardName} to {to.CardName}");

            if (!blService.CheckCardActivity(from))
                throw new BusinessLogicException(TypeBusinessException.CARD,
                "Card is expired", $"Карта {from.CardNumber } просрочена");

            if (!blService.CheckCardActivity(to))
                throw new BusinessLogicException(TypeBusinessException.CARD,
                "Card is expired", $"Карта {to.CardNumber } просрочена");

            if (blService.GetBalanceOfCard(from) < sum)
                throw new BusinessLogicException(TypeBusinessException.CARD,
                "No money", $" Недостаточно средств на карте {from.CardNumber }");

            CardFrom = from;
            CardTo = to;
            Sum = blService.GetConvertSum(sum, from.Currency, to.Currency);
        }

        /// <summary>
        /// Create new Transactions
        /// </summary>
        /// <param name="sum">Sum of the transaction</param>
        /// <param name="to">Link card to</param>
        public Transaction(decimal sum, Card to)
        {
            if (to == null)
                throw new BusinessLogicException(TypeBusinessException.TRANSACTION, "To card is null",
                "Не найдена карта на которую совершается перевод");
            if (sum <= 0)
                throw new UserDataException("Sum of the transaction need more then 0", $"Add to {to.CardName}");

            CardTo = to;
            Sum = blService.GetConvertSum(sum, Currency.RUR, to.Currency);
        }
    }
}