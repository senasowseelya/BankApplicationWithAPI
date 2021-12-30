using BankATM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankATM.Services.Interfaces;

namespace BankATM.Services
{
    public class BankService : IBankService
    {
        BankDataEntities dbContext { get; set; }
        public BankService(BankDataEntities bankDataEntities)
        {
           this. dbContext = bankDataEntities;
        }
        public bank AddBank(string bankname, string branch)
        {

            bank newBank = new bank();
            newBank.name = bankname;
            newBank.balance = 0;
            newBank.branch = branch;
            newBank.ifsc = bankname.Substring(0, bankname.Length - 1) + "567";
            newBank.id = bankname.Substring(0, bankname.Length - 1) + "123";
            dbContext.banks.AddObject(newBank);
            dbContext.SaveChanges();
            return newBank;
        }
        public string CreateAccountService(String CurrentBankId, bankuser newUser)
        {
            account newAccount = new account();
            newAccount.bankId = CurrentBankId;
            newAccount.accountNumber = GenerateAccountNumber();
            newAccount.accountId = GenerateAccId(CurrentBankId);
            newAccount.balance = 0;
            newAccount.bankuser = newUser;
            newAccount.status = "Active";
            newAccount.dateOfIssue = DateTime.Today;
            newUser.id = newUser.name.Substring(0, 4);
            newUser.username = newAccount.accountNumber;
            newUser.password = newAccount.accountNumber;
            dbContext.accounts.AddObject(newAccount);
            dbContext.bankusers.AddObject(newUser);
            dbContext.SaveChanges();

            return newAccount.accountNumber;


        }
        private string GenerateAccountNumber()
        {
            var random = new Random();
            var r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;

        }
        private String GenerateAccId(String BankId)
        {
            var AccID = BankId.Substring(0, 3) + DateTime.Now.ToString("yyyyMMddhhmmss");
            return AccID;
        }
        public bool RemoveAccount(string currentBankId, string accountNumber)
        {
            account acc = (from account in dbContext.accounts where account.accountNumber == accountNumber select account).FirstOrDefault();
            if (acc == null)
                new AccountDoesntExistException();
            acc.status = "InActive";
            dbContext.SaveChanges();
            return true;

        }
        public bool ModifyServiceCharges(string currentBankId, string serviceChargeid, decimal serviceChargeValue)
        {
            var serviceChargeObj = (from charge in dbContext.serviceCharges where charge.id == serviceChargeid && charge.bankId == currentBankId select charge).FirstOrDefault();
            if (serviceChargeObj == null)
                return false;
            else
                serviceChargeObj.value = serviceChargeValue;
            dbContext.SaveChanges();
            return true;

        }
        public bool AcceptNewCurrency(string currentBankId, currency newCurrency)
        {
            newCurrency.bankid = currentBankId;
            dbContext.currencies.AddObject(newCurrency);
            dbContext.SaveChanges();
            return true;

        }
        public List<transaction> DisplayTransactions(string accountNumber)
        {
            var transactions = (from transaction in dbContext.transactions where transaction.senderaccountId == accountNumber || transaction.receiveraccountId == accountNumber select transaction).ToList();
            return transactions;
        }
        public void AddEmployee(string currentBankId, bankuser user)
        {
            employee employee = new employee();
            employee.bankId = currentBankId;
            employee.userId = user.id;
            employee.bankuser = user;
            //employee.bank=(from bank in dbContext.banks where bank.id== currentBankId select bank).FirstOrDefault();
            employee.employeeId = DateTime.Now.ToString("yyyyMMddhhmmss");
            dbContext.employees.AddObject(employee);
            dbContext.SaveChanges();


        }
        public bool RevertTransaction(string transactionId, string senderId)
        {
            account reqSenderAccount, reqReceiverAccount;
            var transactionObj = (from transaction in dbContext.transactions where transaction.transid == transactionId && transaction.senderaccountId == senderId select transaction).Single();
            if (transactionObj == null)
                return false;
            else
                reqSenderAccount = (from account in dbContext.accounts where account.accountId == transactionObj.senderaccountId select account).SingleOrDefault();
            if (transactionObj.type == "1")
                reqSenderAccount.balance -= transactionObj.transactionAmount;
            else if (transactionObj.type == "2")
                reqSenderAccount.balance += transactionObj.transactionAmount;
            else if (transactionObj.type == "3")
            {
                reqReceiverAccount = (from account in dbContext.accounts where account.accountId == transactionObj.receiveraccountId select account).SingleOrDefault();
                reqSenderAccount.balance += transactionObj.transactionAmount;
                reqReceiverAccount.balance -= transactionObj.transactionAmount;

            }

            dbContext.SaveChanges();
            return true;

        }

    }
}
