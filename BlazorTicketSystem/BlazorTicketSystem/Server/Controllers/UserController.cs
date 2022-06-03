using System.Threading.Tasks;
using BlazorTicketSystem.Shared;
using BlazorTicketSystem.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTicketSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRestService _restServices;

        public UserController(IRestService restServices)
        {
            _restServices = restServices;
        }
        [HttpPost("[action]")]
        public async Task<User> Login(User model) 
        {
            if (model.UserName != string.Empty)
            {              
                var res = _restServices.CheckLogin(model.UserName, model.Password).Result;
                if (res != null)
                {
                    Common.testApiUserKey = res.ApiKey;
                    Common.userCompanyId = res.CompanyId;
                    return new Shared.ViewModels.User
                    {
                        UserName = res.UserName,
                        Password = res.Password,
                        CompanyId = res.CompanyId,
                        Active = res.Active,
                        ApiExpiryDate = res.ApiExpiryDate,
                        ApiKey = res.ApiKey,
                        EmployeeId = res.EmployeeId,
                        Extra1 = res.Extra1,
                        Extra2 = res.Extra2,
                        ExtraDate = res.ExtraDate,
                        Narration = res.Narration,
                        RoleId = res.RoleId,
                        UserId = res.UserId,

                    };
                }
                else
                    return new User();
            }
            else
                return new User();

        }
    }
}
