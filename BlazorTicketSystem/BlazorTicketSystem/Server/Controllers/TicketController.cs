using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorTicketSystem.Shared;
using BlazorTicketSystem.Shared.ViewModels;
using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTicketSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public TicketController(SaasdbDB context, ISmSwService pbmsService)
        {
            _context = context;
            _pbmsService = pbmsService;
        }
        private readonly SaasdbDB _context;
        private readonly ISmSwService _pbmsService;

        [HttpGet]
        public IEnumerable<Ticket> Get(string key = "", int tenant = 0, string q = "")
        {
            key = Common.testApiUserKey;
            var list = Enumerable.Empty<TblToDo>();
            //    ; new IEnumerable<TblToDo>()
            //{
            //    new TblToDo
            //    {
            //        CompanyId = 0,
            //        CreationDate = DateTime.Now.Date,
            //        Id = 0,
            //        ImageFileName = "gallery.jpg",
            //        LastStatus = "",
            //        LastUpdate = DateTime.Now,
            //        TeamMember = "NA",
            //        ToDoSubject = "NA",
            //    }
            //};
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

        [HttpPost("ToDoAddComment")]
        public TblToDoDetail ToDoAddComment(TblToDoDetail toDoDetails, string key = "")
        {
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

        [HttpGet("[action]")]
        public IEnumerable<TblCompany> Tenants(string key = "")
        {
            key = Common.testApiUserKey;
            if (_pbmsService.CheckUserByApiKey(key))
                return _context.TblCompanies.ToList();
            else
                return Enumerable.Empty<TblCompany>();
        }
    }
}
