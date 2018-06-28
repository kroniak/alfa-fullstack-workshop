using System;
using System.Collections.Generic;
using Server.Exceptions;

namespace Server.Models
{
    /// <summary>
    /// User domain model
    /// </summary>
    public class User
    {
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
        /// Getter user card list
        /// </summary>
        public List<Card> Cards { get; } = new List<Card>();

        /// <summary>
        /// Added new card to list
        /// </summary>
        /// <param name="shortCardName"></param>
        public Card OpenNewCard(string shortCardName)
            => throw new System.NotImplementedException();

        // TODO add fields
    }
}