using System;
using System.Runtime.Serialization;

namespace BankATM.Services
{
    
    public class CurrencyNotSupportedException : Exception
    {
        public CurrencyNotSupportedException():base("This Currency type is not supported")
        {

        }

        
    }
}