using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorTicketSystem.Shared;
using BlazorTicketSystem.Shared.ViewModels;
using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTicketSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public TicketController(DbContext context, IRestService pbmsService,
            IWebHostEnvironment webHost)
        {
            _context = context;
            _pbmsService = pbmsService;
            _webHost = webHost;
        }
        private readonly DbContext _context;
        private readonly IRestService _pbmsService;
        private readonly IWebHostEnvironment _webHost;

        [HttpGet]
        public IEnumerable<Ticket> Get(string key = "", int tenant = 0, string q = "")
        {
            key = Common.testApiUserKey;
            var list = Enumerable.Empty<TblToDo>();
          
            if (!string.IsNullOrEmpty(key))
            {
                if (_pbmsService.CheckUserByApiKey(key))
                {
                    if (tenant == 0)
                    {
                        if (q == "All")
                            list =  _context.TblToDoes.ToList().OrderByDescending(n => n.Id);
                        else
                            list = _context.TblToDoes.Where(w => w.LastStatus.Contains(q)).ToList().OrderByDescending(n => n.Id);
                    }
                    else
                    {
                        if (q == "All")
                            list = _context.TblToDoes.Where(t => t.CompanyId.Equals(tenant)).ToList().OrderByDescending(n => n.Id);
                        else
                            list = _context.TblToDoes.Where(t => t.CompanyId.Equals(tenant)).Where(w => w.LastStatus.Contains(q)).ToList().OrderByDescending(n => n.Id);
                    }
                }
                
                return (from a in list join b in _context.TblCompanies
                        on a.CompanyId equals b.CompanyId
                        select new Ticket{ 
                         CompanyId = a.CompanyId,
                         CreationDate = a.CreationDate,
                         Id = a.Id,
                         ImageFileName = a.ImageFileName,
                         LastStatus = a.LastStatus,
                         LastUpdate = a.LastUpdate,
                         TeamMember = a.TeamMember,
                         TenantName = b.CompanyName,
                         ToDoSubject = a.ToDoSubject,
                        }).ToList().AsEnumerable<Ticket>();
            }
            return (IEnumerable<Ticket>)list;
        }

        [HttpPost]
        public async Task<TblToDo> Add([FromBody] Ticket todo, string key = "")
        {
            key = Common.testApiUserKey;
          
            TblToDo lastEntry = new TblToDo
            {
                Id = 0,
                LastStatus = "Wrong Authentication"
            };
            if (!string.IsNullOrEmpty(key))
            {
                if (_pbmsService.CheckUserByApiKey(key))
                {
                   await _context.TblToDoes.InsertWithIdentityAsync(() => new TblToDo
                    {
                        ToDoSubject = todo.ToDoSubject,
                        TeamMember = todo.TeamMember,
                        CreationDate = DateTime.Now,
                        LastStatus = "New",
                        CompanyId = todo.CompanyId,
                        ImageFileName = todo.ImageFileName,
                        LastUpdate = DateTime.Now,
                    });
                    _context.CommitTransaction();
                    lastEntry = _context.TblToDoes.FirstOrDefault(r => r.Id.Equals(_context.TblToDoes.Max(d => d.Id)));
                }
            }
            return lastEntry;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> Upload(IFormCollection payload)
        {           
            string fileNewNormalizedName = "gallery.png";
            if (payload.Files.Count > 0)
            {
                var formFile = payload.Files[0];
                fileNewNormalizedName = "task_" + DateTime.Now.Year.ToString() + new Random().Next().ToString() + ".jpg";
                var fulPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot","images", "issues" , fileNewNormalizedName).Replace("Server","Client");
                using (FileStream fs = System.IO.File.Create(fulPath))
                {
                    formFile.CopyTo(fs);
                    fs.Flush();
                }
            }
            return Ok(fileNewNormalizedName);
        }

        [HttpPost("UpdateToDo")]
        public async Task<TblToDo> UpdateToDo(TblToDo todo, string key = "")
        {
            TblToDo OldTodo = _context.TblToDoes.FirstOrDefault(t => t.Id.Equals(todo.Id));
            TblToDo NewTodo = new TblToDo();
            if (!string.IsNullOrEmpty(key))
            {
                if (_pbmsService.CheckUserByApiKey(key) && OldTodo.Id > 0)
                {
                   await _context.TblToDoes
                        .Where(r => r.Id.Equals(OldTodo.Id))
                        .Set(p => p.ToDoSubject, todo.ToDoSubject)
                        .Set(p => p.TeamMember, todo.TeamMember)
                        .Set(p => p.LastStatus, todo.LastStatus)
                        .UpdateAsync();
                    _context.CommitTransaction();
                    NewTodo = _context.TblToDoes.FirstOrDefault(r => r.Id.Equals(OldTodo.Id));
                    return NewTodo;
                }
                else
                    return NewTodo;
            }
            else
                return NewTodo;
        }

        [HttpGet("[action]/{id}")]
        public TblToDo Detail(string key = "", int id = 0)
        {
            key = Common.testApiUserKey;
            if (string.IsNullOrEmpty(key))
                return new TblToDo();
            else
            {
                if (_pbmsService.CheckUserByApiKey(key))
                {
                    if (id > 0)
                        return _context.TblToDoes.Where(t => t.Id == id).FirstOrDefault();
                    else
                        return new TblToDo();
                }
                else
                    return new TblToDo();
            }

        }

        [HttpPost("TicketAddComment")]
        public TblToDoDetail ToDoAddComment(TblToDoDetail toDoDetails, string key = "")
        {
            key = Common.testApiUserKey;
            TblToDo OldTodo = _context.TblToDoes.FirstOrDefault(t => t.Id.Equals(toDoDetails.ToDoId));
            int newToDoCommentSerial = 0;
            newToDoCommentSerial = _context.TblToDoDetails.Where(f => f.ToDoId.Equals(toDoDetails.ToDoId)).ToList().Count + 1;
            TblToDoDetail addedEntity = new TblToDoDetail();
            if (!string.IsNullOrEmpty(key))
            {
                if (_pbmsService.CheckUserByApiKey(key) && OldTodo.Id > 0)
                {
                    var newTodoCommentId = _context.TblToDoDetails.InsertWithIdentity(() => new TblToDoDetail
                    {
                        Notes = toDoDetails.Notes,
                        ToDoId = toDoDetails.ToDoId,
                        TeamMember = toDoDetails.TeamMember,
                        Slno = newToDoCommentSerial,
                        ExtraDate = DateTime.Now
                    });
                    _context.CommitTransaction();
                    if (Common.ToIntConvertObject(newTodoCommentId, 0) > 0)
                        addedEntity = _context.TblToDoDetails.FirstOrDefault(r => r.Id.Equals(Common.ToIntConvertObject(newTodoCommentId, 0)));
                }
            }
            return addedEntity;
        }

        [HttpGet("ToDoDetails")]
        public IEnumerable<TblToDoDetail> GetDetails(int ticketId, string key = "")
        {
            key = Common.testApiUserKey;
            if (string.IsNullOrEmpty(key))
                return Enumerable.Empty<TblToDoDetail>();
            else
            {
                if (_pbmsService.CheckUserByApiKey(key))
                {
                    return _context.TblToDoDetails.Where(f => f.ToDoId.Equals(ticketId)).ToList().OrderBy(n => n.Id);
                }
                else
                    return Enumerable.Empty<TblToDoDetail>();
            }

        }

        [HttpGet("Tenants")]
        public IEnumerable<TblCompany> Tenants(string key = "")
        {
            key = Common.testApiUserKey;
            if (_pbmsService.CheckUserByApiKey(key))
                return _context.TblCompanies.ToList();
            else
                return Enumerable.Empty<TblCompany>();
        }

        [HttpPost("UpdateTicketStatus")]
        public async Task<TblToDo> UpdateTicketStatus(Ticket ticket, string key = "")
        {
            key = Common.testApiUserKey;
            TblToDo OldTodo = _context.TblToDoes.FirstOrDefault(t => t.Id.Equals(ticket.Id));
            TblToDo NewTodo = new TblToDo();
            if (!string.IsNullOrEmpty(key))
            {
                if (_pbmsService.CheckUserByApiKey(key) && OldTodo.Id > 0)
                {
                    await _context.TblToDoes
                         .Where(r => r.Id.Equals(OldTodo.Id))
                         .Set(p => p.LastStatus, ticket.LastStatus)
                         .UpdateAsync();
                    _context.CommitTransaction();
                    NewTodo = _context.TblToDoes.FirstOrDefault(r => r.Id.Equals(OldTodo.Id));
                    return NewTodo;
                }
                else
                    return NewTodo;
            }
            else
                return NewTodo;
        }
    }
}
