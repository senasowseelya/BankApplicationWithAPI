using System;
using System.Collections.Generic;
using System.Text;

namespace BankATM.Services
{
     public class BankAlreadyExistsException:Exception
    {
        public BankAlreadyExistsException():base("Bank with given name already exists")
        {

        }
    }
}
