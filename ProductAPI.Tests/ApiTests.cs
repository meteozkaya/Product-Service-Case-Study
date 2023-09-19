using Azure;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace ProductAPI.Tests
{
	[TestClass]
	public class ApiTests
	{
		public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
		{
			private readonly WebApplicationFactory<Program> _factory;

			public HttpClient Client { get; private set; }

			public IntegrationTests(WebApplicationFactory<Program> factory)
			{
				_factory = factory;
				SetUpClient();
			}

			private async Task SeedUser()
			{

				var createUser2 = GenerateCreateUser("Ozan", "Ayko", "ozan.ayko@gmail.com", "admin2", true);
				var response2 = await Client.PostAsync("/api/user", new StringContent(JsonConvert.SerializeObject(createUser2), Encoding.UTF8, "application/json"));

				var createUser3 = GenerateCreateUser("Ezgi", "Ozdikyar", "ezgi.ozdikyar@gmail.com", "admin3", true);
				var response3 = await Client.PostAsync("/api/user", new StringContent(JsonConvert.SerializeObject(createUser3), Encoding.UTF8, "application/json"));

			}

			private async Task SeedProduct()
			{

				var createProduct2 = GenerateCreateProduct(124, 27.5, 11, "Tabak", "Karaca", "Lira", "Porselen Tabak", true);
				var response2= await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createProduct2), Encoding.UTF8, "application/json"));

				var createProduct3 = GenerateCreateProduct(125, 37.5, 12, "Bardak", "Casio", "Lira", "Cam Bardak", false);
				var response3= await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createProduct3), Encoding.UTF8, "application/json"));

				var createProduct4 = GenerateCreateProduct(126, 47.5, 13, "Sort", "Casio", "Lira", "Kot Sort", true);
				var response4 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createProduct4), Encoding.UTF8, "application/json"));
			}


			private User GenerateCreateUser(string name, string surname, string mail, string pass, bool isActive)
			{
				return new User()
				{
					Name = name,
					Surname = surname,
					Email = mail,
					Password = pass,
					IsActive = isActive
				};
			}

			private Product GenerateCreateProduct(int code, double price, int stock, string name, string brand, string currency, string detail, bool isActive)
			{
				return new Product()
				{
					Code = code,
					Price = price,
					Stock = stock,
					Name = name,
					Brand = brand,
					Currency = currency,
					Detail = detail,
					IsActive = isActive
				};
			}


			// TEST NAME - Add User
			[Fact]
			public async Task TestCase0()
			{

				var createUser = GenerateCreateUser("Mete", "Ozkaya", "mete.ozkaya@gmail.com", "admin1", true);
				var response0 = await Client.PostAsync("/api/user", new StringContent(JsonConvert.SerializeObject(createUser), Encoding.UTF8, "application/json"));

				response0.StatusCode.Should().Be(HttpStatusCode.OK);

				var realData0 = JsonConvert.DeserializeObject(await response0.Content.ReadAsStringAsync()); 
				var expectedData0 = JsonConvert.DeserializeObject("{\"name\":\"Mete\",\"surname\":\"Ozkaya\",\"mail\":\"mete.ozkaya@gmail.com\",\"password\":\"admin1\",\"isActive\":true}");
				realData0.Should().BeEquivalentTo(expectedData0);
			}

			// TEST NAME - Auth Control
			[Fact]
			public async Task TestCase1()
			{
				await SeedUser();

				var createUser1 = ("mete.ozkaya@gmail.com", "admin1");
				var response1 = await Client.PostAsync("/api/user/token", new StringContent(JsonConvert.SerializeObject(createUser1), Encoding.UTF8, "application/json"));
				response1.StatusCode.Should().Be(HttpStatusCode.OK);

				var createUser2 = ("mete.ozkaya@gmail.com", "admin1");
				var response2 = await Client.PostAsync("/api/user/token", new StringContent(JsonConvert.SerializeObject(createUser2), Encoding.UTF8, "application/json"));
				response2.StatusCode.Should().Be(HttpStatusCode.OK);

				response1.Should().NotBeEquivalentTo(response2);

			}


			// TEST NAME - Add Product
			[Fact]
			public async Task TestCase2()
			{

				var createProduct1 = GenerateCreateProduct(123, 17.5, 10, "Saat", "Casio", "Lira", "Analog Saat", true);
				var response1 = await Client.PostAsync("/api/product", new StringContent(JsonConvert.SerializeObject(createProduct1), Encoding.UTF8, "application/json"));
				response1.StatusCode.Should().Be(HttpStatusCode.OK);

				var realData0 = JsonConvert.DeserializeObject(await response1.Content.ReadAsStringAsync());
				var expectedData0 = JsonConvert.DeserializeObject("{\"code\":123,\"price\": 17.5,\"stock\":10,\"name\":\"Saat\",\"brand\":\"Casio\",\"currency\":\"Lira\",\"detail\":\"Analog Saat\",\"isActive\":true}");
				realData0.Should().BeEquivalentTo(expectedData0);
			}

			// TEST NAME - GetProductByid
			[Fact]
			public async Task TestCase3()
			{
				await SeedProduct();

				var response0 = await Client.GetAsync("api/product/detail/3");
				response0.StatusCode.Should().Equals(404);

				var response1 = await Client.GetAsync("api/product?id=2");
				response1.StatusCode.Should().Be(HttpStatusCode.OK);

				var realData1 = JsonConvert.DeserializeObject(await response1.Content.ReadAsStringAsync());
				var expectedData1 = JsonConvert.DeserializeObject("{\"code\":124,\"price\": 27.5,\"stock\":11,\"name\":\"Tabak\",\"brand\":\"Karaca\",\"currency\":\"Lira\",\"detail\":\"Porselen Tabak\",\"isActive\":true}");
				realData1.Should().Equals(404);

			}


			// TEST NAME - DeleteProduct and Check if it is seen
			[Fact]
			public async Task TestCase4()
			{

				var response0 = await Client.DeleteAsync("/api/product/2");
				response0.StatusCode.Should().Be(HttpStatusCode.OK);

				// Check if the employee does not exist
				var response1 = await Client.GetAsync("/api/product/detail/2");
				response1.StatusCode.Should().Equals(404);


			}

			private void SetUpClient()
			{
				Client = _factory.WithWebHostBuilder(builder =>
				{
					var databaseOptions = new DbContextOptionsBuilder<DatabaseContext>()
						.UseNpgsql("DataSource=:memory:") 
						.EnableSensitiveDataLogging()
						.Options;

					var dbContext = new DatabaseContext(); 

					var services = new ServiceCollection();

					services.AddSingleton<DatabaseContext>(dbContext);

					var serviceProvider = services.BuildServiceProvider();

					var scopedServiceProvider = serviceProvider.CreateScope().ServiceProvider;

					var client = _factory.WithWebHostBuilder(builder =>
					{
						builder.ConfigureServices(services =>
						{
							services.AddSingleton<DatabaseContext>(scopedServiceProvider.GetService<DatabaseContext>());
						});

						builder.UseStartup<Program>(); 
					}).CreateClient();


				}).CreateClient();
			}
		}
	}
}