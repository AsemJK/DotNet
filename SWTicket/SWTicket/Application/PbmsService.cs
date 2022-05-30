using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class PbmsService : IPbmsService
    {
        private readonly SaasDb _saasdbDBContext;

        public PbmsService(SaasdbDB saasdbDBContext)
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
