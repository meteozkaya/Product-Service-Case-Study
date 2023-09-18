using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers
{
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;
		public UserController( IUserService userService, IAuthService authService, IMapper mapperService)
		{
			_userService = userService;
			_authService = authService;
			_mapper = mapperService;
		}

		[HttpPost]
		public IActionResult AddUser(UserRequestDto user)
		{
			try
			{
				var userRequest = _mapper.Map<User>(user);
				User userResponse = _userService.Add(userRequest);
				UserResponseDto userResponseDto = _mapper.Map<UserResponseDto>(userResponse);
				return Ok(userResponseDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpPost("token")]
		public IActionResult Login(LoginRequestDto user)
		{
			try
			{
				if (_userService.CheckPassword(user.Email, user.Password))
				{
					string token = _authService.generateToken(_userService.GetByEmail(user.Email));
					return Ok(token);
				}
				else
				{
					return BadRequest("False Password!");
				}

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}


	}
}
