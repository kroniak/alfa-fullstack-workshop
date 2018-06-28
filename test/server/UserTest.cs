using System;
using System.IO;
using Server.Exceptions;
using Server.Models;
using Xunit;

namespace ServerTest
{
    public class UserTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateUserException(string value)
            => Assert.Throws<UserDataException>(() => new User(value));
    }
}