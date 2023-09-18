using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserManager : IUserService
	{
		private IUserDal _userDal;
		private readonly SecurityService _securityService;

		public UserManager(IUserDal userDal, SecurityService securityService)
		{
			_userDal = userDal;
			_securityService = securityService;
		}

		public User Add(User user)
		{
			string hashedPassword = _securityService.HashPassword(user.Password);
			user.Password = hashedPassword;
			return _userDal.Add(user);
		}

		public bool CheckPassword(string email, string password)
		{
			User user = _userDal.GetByEmail(email);
			if (user == null)
			{
				throw new Exception("User not found with email " + email + " !");
			}

			return user.Password == _securityService.HashPassword(password);
		}

		public User GetByEmail(string username)
		{
			return _userDal.GetByEmail(username);
		}
	}
}
