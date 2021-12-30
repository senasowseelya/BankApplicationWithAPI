using System;
using System.Collections.Generic;
using System.Text;

namespace BankATM.Services
{
     public class BankDoesntExistException:Exception
    {
        public BankDoesntExistException() : base("Bank Doesnot Exist.. Please check Bank Name")
        {
        }
    }
}
