using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using SWTicket.Server.Storage;
using Microsoft.AspNetCore.Mvc;
using SWTicket.Shared.ViewModels;

namespace SWTicket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarningsController : ControllerBase
    {
        private readonly Storage.IRepository<Earning> _earningsRpository;
        private readonly SaasdbDB _saasdbDB;

        public EarningsController(IRepository<Earning> earningsRpository,
            SaasdbDB saasdbDB)
        {
            _earningsRpository = earningsRpository;
            _saasdbDB = saasdbDB;
        }
        [HttpGet]
        public IEnumerable<Earning> Get()
        {
            List<Earning> list = new List<Earning>()
            {
                new Earning()
                {
                    Id = new System.Guid(),
                    Date = System.DateTime.Now.Date,
                    Category = EarningCategory.Caoching,
                    Subject = "This is for coatching of C# Course",
                    Amount = 2000,
                }
            };
            //return _earningsRpository.GetAll()
            //    .OrderByDescending(w => w.Date);
            list = (from a in _saasdbDB.TblToDoes
                   select new Earning()
                   {
                       Subject = a.ToDoSubject,
                       Date = Convert.ToDateTime(a.CreationDate),
                       Amount = a.Id,
                       Category = EarningCategory.Freelancing,
                   }).ToList();
            return list;
        }

        [HttpPost]
        public async Task Post(Earning earning)
        {
            _earningsRpository.Add(earning);
        }
    }
}
