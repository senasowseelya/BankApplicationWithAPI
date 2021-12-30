using System;
using System.Collections.Generic;
using System.Text;

namespace BankATM.Services
{
    public class AccountDoesntExistException : Exception
    {
        public AccountDoesntExistException() : base("Account doesn't exist")
        {
        }
    }
}
