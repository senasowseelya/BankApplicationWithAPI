using BankATM.Data;
using BankATM.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace BankATM.API.Controllers
{
    public class AccountsController : ApiController
    {
        public BankDataEntities bankDataEntities { get; set; }
        public BankService bankService { get; set; }
        public AccountService accountService { get; set; }
        public AccountsController()
        {
            bankDataEntities= new BankDataEntities();
            bankService = new BankService(bankDataEntities);
            accountService = new AccountService(bankDataEntities);
        }

        // GET: Accounts
        [HttpGet]
        [Route("api/accounts")]

        public IEnumerable<account> Get()
        {
            using (BankDataEntities entities = new BankDataEntities())
            {
                return entities.accounts.ToList();
            }

        }
        [HttpGet]
        [Route("api/accounts/{accountId}")]
        public IHttpActionResult Get(string accountId)
        {
            using (BankDataEntities entities = new BankDataEntities())
            {
                try
                {
                    return Ok(entities.accounts.SingleOrDefault(e => e.accountId == accountId));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        [HttpGet]
        [Route("api/accounts/display")]

        public IHttpActionResult DisplayTransactions([FromBody] string accountNumber)
        {
            return Ok(accountService.Displaytransactions(accountNumber));
        }

        //post
        [HttpPost]
        [Route("api/accounts/{bankId}")]
        public IHttpActionResult CreateAccount([FromBody]bankuser user,[FromUri]string bankId)
        {
            return Ok(bankService.CreateAccountService(bankId, user));
        }

        //put
        [HttpPut]
        [Route("api/accounts/deposit")]
        public IHttpActionResult Deposit([FromBody] JObject parameters)
        {
            
                return Ok(accountService.Deposit((string)parameters["accountNumber"], (decimal)parameters["amount"], (string)parameters["currencyName"]));
        }

        [HttpPut]
        [Route("api/accounts/withdraw")]

        public IHttpActionResult Withdraw([FromBody] JObject parameters)
        {
            return Ok(accountService.Withdraw((string)parameters["accountNumber"], (decimal)parameters["amount"], (string)parameters["currencyName"]));
        }

        [HttpPut]
        [Route("api/accounts/changePassword")]
        public IHttpActionResult ChangePassword([FromBody] JObject parameters)
        {
            return Ok(accountService.ChangePassword((string)parameters["accountNumber"], (string)parameters["oldPassword"], (string)parameters["newPassword"]));
        }

        [HttpPut]
        [Route("api/accounts/transfer")]
        public IHttpActionResult Transfer([FromBody] JObject parameters)
        {
            return Ok(accountService.TransferAmount((string)parameters["senderAccountNumber"], (string)parameters["receiverAccountNumber"],(decimal)parameters["amount"], (string)parameters["currencyName"],(string)parameters["mode"]));
        }
        






    }
}