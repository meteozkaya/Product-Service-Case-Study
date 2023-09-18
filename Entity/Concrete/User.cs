using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class User:IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
	}
}
