﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Request
{
	public class UserRequestDto:IDto
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }

	}
}