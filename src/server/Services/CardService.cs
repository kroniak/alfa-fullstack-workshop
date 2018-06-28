using System.Text.RegularExpressions;

namespace Server.Services
{
    /// <summary>
    /// Our implementing of the <see cref="ICardService"/> interface
    /// </summary>
    public class CardService : ICardService
    {
        Regex digitsMatch = new Regex("^[0-9]+$");

        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        public bool CheckCardNumber(string cardNumber)
        {
            string number;
            if (string.IsNullOrEmpty(cardNumber) ||
                (number = ValidateCardNumberFormat(cardNumber)) == null)
            {
                return false;
            }

            int sum = 0;
            int length = number.Length;
            int i;
            if (length % 2 == 0)
            {
                
            }
            for (i < length; i++)
            {
                
            }
        }

        /// <summary>
        /// Check card number by Alfabank emmiter property
        /// </summary>
        /// <param name="cardNumber">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was emmited in Alfabank </returns>
        public bool CheckCardIssuer(string cardNumber) => throw new System.NotImplementedException();

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="cardNumber">card number in any format</param>
        /// <returns>Return 0 is card is invalid, 1 if card is mastercard, 2 is visa, 3 is maestro, 4 is visa electon</returns>
        public int CardTypeExtract(string cardNumber) => throw new System.NotImplementedException();

        /// <summary>
        /// Removes all symbols except digits. 
        /// </summary>
        /// <param name="cardNumber">card number without spaces or null if number contains anyu other symbols</param>
        private string ValidateCardNumberFormat(string cardNumber)
        {
            // remove spaces
            cardNumber = cardNumber.Replace(" ", "");
            // use regular expression to detect non digit symbols
            if (digitsMatch.IsMatch(cardNumber))
            {
                return cardNumber;
            }

            return null;
        }
    }
}