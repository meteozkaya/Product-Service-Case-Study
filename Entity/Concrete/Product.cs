using Core.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Product: IEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int Code { get; set; }
		public double Price { get; set; }
		public int Stock { get;set; }
		public string Name { get; set; }
		public string Brand { get; set; }
		public string Currency { get; set; }
		public string Detail { get; set; }
		public bool IsActive { get; set; }
	}
}
