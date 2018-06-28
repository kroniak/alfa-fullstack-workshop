namespace Server.Services
{
    /// <summary>
    /// Interface for checking numbers and extracting card types
    /// </summary>
    public interface ICardService
    {
        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        bool CheckCardNumber(string number);

        /// <summary>
        /// Check card number by Alfabank emmiter property
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was emmited in Alfabank </returns>
        bool CheckCardIssuer(string number);

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Card payment system</returns>
        CardPaymentSystem CardPaymentSystemExtract(string number);
    }
}