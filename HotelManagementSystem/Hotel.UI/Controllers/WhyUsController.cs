using Hotel.Business.DTOs.SliderHomeDTOs;
using Hotel.Business.DTOs.WhyUsDTOs;
using Hotel.Business.Exceptions;
using Hotel.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WhyUsController : ControllerBase
	{
		private readonly IWhyUsService _whyUsService;
		public WhyUsController(IWhyUsService whyUsService)
		{
			_whyUsService = whyUsService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _whyUsService.GetAllAsync();
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
				var slider = await _whyUsService.GetByCondition(x => x.Title!=null? x.Title.Contains(title):true);
				return Ok(slider);
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
				var slider = await _whyUsService.GetByIdAsync(id);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}


		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateWhyUsDto  createWhyUs)
		{
			try
			{
				await _whyUsService.Create(createWhyUs);
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
		public async Task<IActionResult> Put(int id, [FromForm] UpdateWhyUsDto updateWhyUs)
		{
			try
			{
				await _whyUsService.UpdateAsync(id, updateWhyUs);
				return Ok("Updated");
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
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
				await _whyUsService.Delete(id);
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
