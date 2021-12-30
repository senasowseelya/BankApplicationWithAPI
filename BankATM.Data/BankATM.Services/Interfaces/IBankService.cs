using BankATM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankATM.Services.Interfaces
{
    internal interface IBankService
    {
        public bank AddBank(string bankname, string branch);
        public string CreateAccountService(String CurrentBankId, bankuser newUser);
        public bool RemoveAccount(string currentBankId, string accountNumber);
        public bool ModifyServiceCharges(string currentBankId, string serviceChargeid, decimal serviceChargeValue);
        public bool AcceptNewCurrency(string currentBankId, currency newCurrency);
        public List<transaction> DisplayTransactions(string accountNumber);
        public void AddEmployee(string currentBankId, bankuser user);
        public bool RevertTransaction(string transactionId, string senderId);
    }
}
