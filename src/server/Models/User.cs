using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Server.Exceptions;
using Server.Infrastructure;
using Server.Services;

namespace Server.Models
{
    /// <summary>
    /// User domain model
    /// </summary>
    public class User
    {
        private readonly IBusinessLogicService blService = new BusinessLogicService();

        private IList<Card> _cards = new List<Card>();

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="userName">Login of the user</param>
        public User(string userName)
        {
            // TODO return own Exception class
            if (string.IsNullOrWhiteSpace(userName))
                throw new UserDataException("username is null or empty", userName);

            UserName = userName;
        }

        /// <summary>
        /// Getter and setter username of the user for login
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Getter and setter Surname of the user
        /// </summary>
        /// <returns><see langword="string"/></returns>
        public string Surname { get; set; }

        /// <summary>
        /// Getter and setter Firstname of the user
        /// </summary>
        /// <returns><see langword="string"/></returns>
        public string Firstname { get; set; }

        /// <summary>
        /// Getter and setter Middlename of the user
        /// </summary>
        /// <returns><see langword="string"/></returns>
        public string Middlename { get; set; }

        /// <summary>
        /// Getter and setter Bithday of the user
        /// </summary>
        /// <returns>Datetime</returns>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Getter user card list
        /// </summary>
        public IList<Card> Cards
        {
            get
            {
                return new ReadOnlyCollection<Card>(_cards);
            }
        }

        /// <summary>
        /// Added new card to list
        /// </summary>
        /// <param name="shortCardName"></param>
        public Card OpenNewCard(string shortCardName, Currency currency, CardType cardType)
        {
            if (cardType == CardType.UNKNOWN)
            {
                throw new UserDataException("Wrong type card", cardType.ToString());
            }
            if (Cards.Any(x => x.CardName == shortCardName))
            {
                throw new UserDataException("Card is already exist", shortCardName);
            }

            var newCard = new Card(blService.GenerateNewCardNumber(cardType), shortCardName, cardType, currency);
            _cards.Add(newCard);
            blService.AddBonusOnOpen(newCard);
            return newCard;
        }
    }
}