using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IPbmsService
    {
        bool CheckUserByApiKey(string apiKey);
    }
}
