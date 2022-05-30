using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTicket.Server
{
    public class SmSwService : ISmSwService
    {
        private readonly SaasdbDB _saasdbDBContext;

        public SmSwService(SaasdbDB saasdbDBContext)
        {
            _saasdbDBContext = saasdbDBContext;
        }
        public bool CheckUserByApiKey(string apiKey)
        {
            bool isValid = false;
            DateTime ApiExpiry;
            TblUser user;
            user = _saasdbDBContext.TblUsers.FirstOrDefault(x => x.ApiKey == apiKey);
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
    }
}
