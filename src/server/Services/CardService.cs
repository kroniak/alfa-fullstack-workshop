namespace Server.Services
{
    /// <summary>
    /// Our implementing of the <see cref="ICardService"/> interface
    /// </summary>
    public class CardService : ICardService
    {
        /// <summary>
        /// Check card number by Lun algoritm
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card is valid</returns>
        public bool CheckCardNumber(string number) => throw new System.NotImplementedException();

        /// <summary>
        /// Check card number by Alfabank emmiter property
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return <see langword="true"/> if card was emmited in Alfabank </returns>
        public bool CheckCardIssuer(string number) => throw new System.NotImplementedException();

        /// <summary>
        /// Extract card number
        /// </summary>
        /// <param name="number">card number in any format</param>
        /// <returns>Return 0 is card is invalid, 1 if card is mastercard, 2 is visa, 3 is maestro, 4 is visa electon</returns>
        public int CardTypeExtract(string number) => throw new System.NotImplementedException();
    }
}