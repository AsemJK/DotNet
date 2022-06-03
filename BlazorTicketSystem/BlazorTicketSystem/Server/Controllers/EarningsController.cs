using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTicketSystem.Server.Storage;
using BlazorTicketSystem.Shared.ViewModels;
using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTicketSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarningsController : ControllerBase
    {
        private readonly Storage.IRepository<Earning> _earningsRpository;
        private readonly DbContext _dbContext;

        public EarningsController(IRepository<Earning> earningsRpository,
            DbContext dbContext)
        {
            _earningsRpository = earningsRpository;
            _dbContext = dbContext;
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
            return _earningsRpository.GetAll()
                .OrderByDescending(w => w.Date);
        }

        [HttpPost]
        public async Task Post(Earning earning)
        {
            _earningsRpository.Add(earning);
        }
    }
}
