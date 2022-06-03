using BlazorTicketSystem.Shared;
using BlazorTicketSystem.Shared.ViewModels;
using DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTicketSystem.Server
{
    public class RestService : IRestService
    {
        private readonly DbContext _dbContext;

        public RestService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CheckUserByApiKey(string apiKey)
        {
            bool isValid = false;
            DateTime ApiExpiry;
            TblUser user;
            user = _dbContext.TblUsers.FirstOrDefault(x => x.ApiKey == apiKey);
            if(user == null)
                return false;
            else
            {
                if(user.ApiExpiryDate >= DateTime.Now.Date)
                    return true;
                else
                    return false;
            }
        }

        public async Task<TblUser> CheckLogin(string userName, string password)
        {
            var user = _dbContext.TblUsers.FirstOrDefault(r => r.UserName.Equals(userName) && r.Password.Equals(Pcode112.Encrypt(password)));
            return user;
        }
    }
}
