using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using System.Numerics;

namespace ProductAPI
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			CreateMap<UserRequestDto, User>().ReverseMap();
			CreateMap<User, UserResponseDto>().ReverseMap();


			CreateMap<ProductRequestDto, Product>().ReverseMap();
			CreateMap<Product, ProductResponseDto>().ReverseMap();

		}
	}
}
