using System;
using System.Linq;
using System.Text;
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

            return !(card.DTOpenCard.AddYears(card.Validity) <= DateTime.Today);
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

            var credit = card.Transactions.Where(x => x.CardToNumber == card.CardNumber).Sum(x => x.ToSum);
            var debit = card.Transactions.Where(x => x.CardFromNumber == card.CardNumber).Sum(x => x.FromSum);
            return credit - debit;
        }

        /// <summary>
        /// Generate new card number from BIN list
        /// </summary>
        /// <param name="cardType">Type of the card/param>
        /// <returns>Generated new card number</returns>
        public string GenerateNewCardNumber(CardType cardType)
        {
            string startBin;

            switch (cardType)
            {
                case CardType.MIR:
                    startBin = "2";
                    break;
                case CardType.VISA:
                    startBin = "4";
                    break;
                case CardType.MAESTRO:
                    startBin = "6";
                    break;
                case CardType.MASTERCARD:
                    startBin = "51";
                    break;
                default:
                    throw new BusinessLogicException(TypeBusinessException.CARD, "Cannot create new card number");
            }

            startBin = Constants.AlfaBINs.First(x => x.StartsWith(startBin));
            if (string.IsNullOrWhiteSpace(startBin))
                throw new BusinessLogicException(TypeBusinessException.CARD, "Cannot create new card number");

            // generate new CardNumber for user
            return GenerateNewCardNumber(startBin);
        }

        private string GenerateNewCardNumber(string prefix, int length = 16)
        {
            string ccnumber = prefix;

            while (ccnumber.Length < (length - 1))
            {
                double rnd = (new Random().NextDouble() * 1.0f - 0f);
                ccnumber += Math.Floor(rnd * 10);
            }

            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();
            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum
            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != (length - 2))
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;
        }
    }
}