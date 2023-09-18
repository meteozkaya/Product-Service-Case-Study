using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Business.Abstract;

namespace Business.Concrete
{
	public class AuthManager : IAuthService
	{
		private const string secret = "denemedenemedenemedenemedenemedenemedeneme";
		private IUserDal _userdal;
		public AuthManager(IUserDal userRepository)
		{
			_userdal = userRepository;
		}

		public string generateToken(User user)
		{

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new[]
			{
				new Claim(type:"userId",  value : user.Id.ToString()),
			};
			var token = new JwtSecurityToken("https://localhost:5000",
				"https://localhost:5000",
				claims,
				expires: DateTime.Now.AddMinutes(6000),
				signingCredentials: credentials);


			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
