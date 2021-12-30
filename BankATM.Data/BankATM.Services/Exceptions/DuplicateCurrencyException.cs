using System;
using System.Collections.Generic;
using System.Text;

namespace BankATM.Services
{
    public class DuplicateCurrencyException:Exception
    {
        public DuplicateCurrencyException():base("Currency is already in accepted currencies list")
        {
            
        }
    }
}
