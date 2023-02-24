using Hotel.Business.DTOs.ServiceOfferDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiceOfferController : ControllerBase
	{
		private readonly IServiceOfferService _serviceOfferService;
		public ServiceOfferController(IServiceOfferService serviceOfferService)
		{
			_serviceOfferService = serviceOfferService;
		}

		[HttpGet]
		public async  Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _serviceOfferService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("searchByTitle/{title}")]
		public async Task<IActionResult> GetByTitle(string title)
		{
			try
			{
				var list = await _serviceOfferService.GetByCondition(x=>x.Title!=null?x.Title==title:true);
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
				var list = await _serviceOfferService.GetByIdAsync(id);
				return Ok(list);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}


		//[HttpGet("getAllById/{id}")]
		//public async Task<IActionResult> GetAllById(int id)
		//{
		//	try
		//	{
		//		var list = await _serviceOfferService.GetAllByIdAsync(id);
		//		return Ok(list);
		//	}
		//	catch (NotFoundException ex)
		//	{
		//		return NotFound(ex.Message);
		//	}
		//}


		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateServiceOfferDto createService)
		{
			try
			{
				await _serviceOfferService.Create(createService);
				return Ok("Created");
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm] UpdateServiceOfferDto updateService)
		{
			try
			{
				await _serviceOfferService.Update(id, updateService);
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
				await _serviceOfferService.Delete(id);
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
