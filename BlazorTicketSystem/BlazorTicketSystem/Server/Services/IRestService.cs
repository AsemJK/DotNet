using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorTicketSystem.Shared.ViewModels;
using DataModels;

namespace BlazorTicketSystem.Server
{
    public interface IRestService
    {
        bool CheckUserByApiKey(string apiKey);
        Task<TblUser> CheckLogin(string userName, string password);
    }
}
