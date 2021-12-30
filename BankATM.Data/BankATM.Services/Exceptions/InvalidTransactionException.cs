using System;
using System.Collections.Generic;
using System.Text;

namespace BankATM.Services
{
     public class InvalidTransactionException:Exception
    {
        public InvalidTransactionException():base("Invalid TransactionId")
        {

        }
    }
}
