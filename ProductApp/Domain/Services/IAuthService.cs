using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Domain.Services
{
    public interface IAuthService
    {
        Task<UserResponse> Register(User user, string password);
        Task<JTTokenResponse> Login(string userName, string password);
    }
}
