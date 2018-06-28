using System.Linq;
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
            // validate card number format
            if ((number = ValidateCardNumberFormat(cardNumber)) == null)
            {
                return false;
            }

            // define that card length is even or odd
            int length = number.Length;
            bool isLengthEven = (length % 2) == 0;

            int sum = 0;

            // perform Luhn algorithm
            int current = 0;
            for (int i = 0;  i < length; i++)
            {
                current = number[i];                
                if ( (((i+1) % 2) == 0) == !isLengthEven)
                {
                    current *= 2;
                    current = (current > 9) ? (current - 9) : current ;
                }
                sum += current;
            }

            // check checksum
            if(sum % 10 == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check card number by Alfabank issuer property
        /// </summary>
        /// <param name="cardNumber">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was issued in Alfabank </returns>
        public bool CheckCardIssuer(string cardNumber)
        {
            string number;
            // validate card number format
            if ((number = ValidateCardNumberFormat(cardNumber)) == null)
            {
                return false;
            }

            // get BIN
            string bin = Regex.Match(number, "^.{0,6}").Value;
            return Repository.AlfaBankBINs.Contains(bin);

        }

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="cardNumber">card number in any format</param>
        /// <returns>return CardPaymentSystem enum with corresponding value</returns>
        public CardPaymentSystem CardPaymentSystemExtract(string cardNumber)
        {
            string number;
            // validate card number format
            if ((number = ValidateCardNumberFormat(cardNumber)) == null)
            {
                return CardPaymentSystem.Invalid;
            }

            // check payment system by first digit of card number 
            switch (number[0])
            {
                case '4':
                    return CardPaymentSystem.Visa;
                case '5':
                    return CardPaymentSystem.Mastercard;
                default:
                    return CardPaymentSystem.Invalid;
            }
        }

        /// <summary>
        /// Removes all symbols except digits. 
        /// </summary>
        /// <param name="cardNumber">card number with only digits or null if number contains other symbols except </param>
        private string ValidateCardNumberFormat(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                return null;
            }

            // remove spaces
            cardNumber = cardNumber.Replace(" ", "");

            // use regular expression to detect non digit symbols and length
            if (digitsMatch.IsMatch(cardNumber))
            {
                if (cardNumber.Length >= 13 && cardNumber.Length <= 19)
                {
                    return cardNumber;
                }
            }

            return null;
        }
    }
}