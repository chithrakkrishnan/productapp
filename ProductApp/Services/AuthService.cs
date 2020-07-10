using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Services
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository authRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        public async Task<UserResponse> Register(User user, string password)
        {
            var existingUser = await _authRepository.UserExists(user.Username);
            if (existingUser)
                return new UserResponse("Username already exists.");

            await _authRepository.Register(user, password);
            await _unitOfWork.Complete();

            return new UserResponse(user);
        }

        public async Task<JTTokenResponse> Login(string userName, string password)
        {
            var existingUser = await _authRepository.Login(userName,password);
            if (existingUser == null)
                return new JTTokenResponse("Unauthorized user");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.Username)
            };

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var token1 = tokenHandler.WriteToken(token);

            return new JTTokenResponse(token1);
        }
    }
}
