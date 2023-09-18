using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using Entities.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IProductService _productservice;

		public ProductController(IMapper mapper, IProductService productservice)
		{
			_mapper = mapper;
			_productservice = productservice;
		}

		[HttpPost]
		public IActionResult AddProduct(ProductRequestDto product)
		{
			try
			{
				var productRequest = _mapper.Map<Product>(product);
				Product ProductResponse = _productservice.Add(productRequest);
				ProductResponseDto productResponseDto = _mapper.Map<ProductResponseDto>(ProductResponse);
				return Ok(productResponseDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			try
			{
				_productservice.Delete(id);
				return Ok("Product successfully deleted with id " + id + "!");
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public IActionResult UpdateProduct(Product product)
		{
			try
			{
				_productservice.Update(product);
				return Ok("Product successfully Updated!");
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet ("/name/{name}")]
		public IActionResult GetProductByName(string name) 
		{
			try
			{
				var record = _mapper.Map<ProductResponseDto>(_productservice.GetByName(name));
				return Ok(record);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("/code/{code}")]
		public IActionResult GetProductByCode(int code)
		{
			try
			{
				var record = _mapper.Map<ProductResponseDto>(_productservice.GetByCode(code));
				return Ok(record);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("/price/{min}/{max}")]
		public IActionResult GetProductByUnitPrice(int min, int max)
		{
			try
			{

				List<ProductResponseDto> responseDtos = _mapper.Map<List<ProductResponseDto>>(_productservice.GetByUnitePrice(min, max));

				List<ProductResponseDto> record = _mapper.Map<List<ProductResponseDto>>(_productservice.GetByUnitePrice(min,max));
				return Ok(record);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("/brand/{brand}")]
		public IActionResult GetProductByBrand(string brand)
		{
			try
			{
				var record = _mapper.Map<ProductResponseDto>(_productservice.GetByBrand(brand));
				return Ok(record);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("/detail/{id}")]
		public IActionResult GetProductDetail(int id)
		{
			try
			{
				var record = _mapper.Map<ProductResponseDto>(_productservice.GetProductDetail(id));
				return Ok(record);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}