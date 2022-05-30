using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWTicket.Server
{
    public interface ISmSwService
    {
        bool CheckUserByApiKey(string apiKey);
    }
}
