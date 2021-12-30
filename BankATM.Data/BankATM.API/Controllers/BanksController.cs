using BankATM.Data;
using BankATM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace BankATM.API.Controllers
{
    public class BanksController : ApiController
    {
        public BankDataEntities bankDataEntities { get; set; }
        public BankService bankService { get; set; }
        public BanksController()
        {
            bankDataEntities = new BankDataEntities();
            bankService = new BankService(bankDataEntities);
        }
        [HttpGet]
        [Route("api/banks")]
        public IEnumerable<bank> Get()
        {

            using (BankDataEntities entities = new BankDataEntities())
            {
                return entities.banks.ToList();

            }
        }

        [HttpGet]
        [Route("api/banks/{bankId}")]
        public bank Get(string bankId)
        {

            using (BankDataEntities entities = new BankDataEntities())
            {
                return entities.banks.SingleOrDefault(e=>e.id==bankId);

            }
        }
        [HttpPost]
        [Route("api/banks/{bankName}")]
        public void CreateBank([FromUri] string bankName,[FromBody]string branch)
        {
            bankService.AddBank(bankName,branch);
        }


    }
}