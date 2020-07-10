using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Resources;

namespace SuperMarket.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IMapper _mapper;

       
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterResource forRegisterResource)
        {
            forRegisterResource.UserName = forRegisterResource.UserName.ToLower();
            var user = _mapper.Map<UserForRegisterResource, User>(forRegisterResource);
            var result = await _authService.Register(user,forRegisterResource.Password);
            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginResource user)
        {
            var result = await _authService.Login(user.Username,user.Password);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }

    }
}