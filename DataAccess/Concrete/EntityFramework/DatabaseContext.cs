using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Concrete.EntityFramework
{
	public class DatabaseContext:DbContext
	{

		private readonly IConfiguration configuration;

		public DatabaseContext(DbContextOptions<DatabaseContext> options, IConfiguration configuration) : base(options)
		{
			this.configuration = configuration;
		}

		public DatabaseContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SabancıDx;Username=postgres;Password=654525");
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
