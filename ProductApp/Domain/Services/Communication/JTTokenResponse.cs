using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Domain.Services.Communication
{
    public class JTTokenResponse: BaseResponse<UserToken>
    {
        public JTTokenResponse(UserToken resource)
            : base(resource)
        {
        }

        public JTTokenResponse(string message)
            : base(message)
        {
        }
    }
}
