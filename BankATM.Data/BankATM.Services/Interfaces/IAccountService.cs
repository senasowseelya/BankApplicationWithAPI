using BankATM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankATM.Services.Interfaces
{
    internal interface IAccountService
    {
        public bool Deposit(string accNumber, decimal amount, string currencyName);
        public bool Withdraw(string accNumber, decimal amount, string currencyName);
        public List<transaction> Displaytransactions(string accNumber);

        public bool ChangePassword(string accNumber, string oldpassword, string newPassword);
        public bool TransferAmount(string sendAccNumber, string recAccNumber, decimal amount, string currencyName, string mode);
    }
}
