using Hotel.Business.DTOs.ServiceImageDTOs;
using Hotel.Business.DTOs.ServiceOfferDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceImageController : ControllerBase
	{
		private readonly IServiceImageService _serviceImage;
		public ServiceImageController(IServiceImageService serviceImage)
		{
			_serviceImage = serviceImage;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var list =await _serviceImage.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("searchByServiceId/{id}")]
		public async Task<IActionResult> GetByServiceId(int id)
		{
			try
			{
				var list = await _serviceImage.GetByCondition(x => x.ServiceOfferId == id);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var list = await _serviceImage.GetByIdAsync(id);
				return Ok(list);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}


		[HttpPost("CreateImageForServiceId/{serviceId}")]
		public async Task<IActionResult> CreateImageForServiceId(int serviceId, [FromForm] CreateServiceImageDto createService)
		{
			try
			{
				await _serviceImage.CreateImageForServiceId(serviceId,createService);
				return Ok("Created");
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm] UpdateServiceImageDto updateService)
		{
			try
			{
				await _serviceImage.UpdateAsync(id, updateService);
				return Ok("Updated");
			}
			catch (IncorrectIdException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}
			catch (BadRequestException ex)
			{

				return NotFound(ex.Message);
			}

			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}

		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _serviceImage.Delete(id);
				return Ok("Deleted");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);

			}
		}
	}
}
