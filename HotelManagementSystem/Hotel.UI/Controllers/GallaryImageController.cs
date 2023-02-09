using Hotel.Business.DTOs.GallaryImageDTOs;
using Hotel.Business.DTOs.ServiceImageDTOs;
using Hotel.Business.Exceptions;
using Hotel.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GallaryImageController : ControllerBase
	{
		private readonly IGallaryImageService _gallaryService;
		public GallaryImageController(IGallaryImageService gallaryService)
		{
			_gallaryService = gallaryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _gallaryService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}


		[HttpGet("searchByCatagoryId/{id}")]
		public async Task<IActionResult> GetByCatagoryId(int id)
		{
			try
			{
				var element = await _gallaryService.GetByCondition(x => x.GallaryCatagoryId == id);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByCatagoryName/{name}")]
		public async Task<IActionResult> GetByCatagoryName(string name)
		{
			try
			{
				var element = await _gallaryService.GetByCondition(x => x.GallaryCatagory != null ? x.GallaryCatagory.Name == name : true);
				return Ok(element);
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
				var element = await _gallaryService.GetByIdAsync(id);
				return Ok(element);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]

		public async Task<IActionResult> Create([FromForm] CreateGallaryImageDto createGallary)
		{
			try
			{

				await _gallaryService.Create(createGallary);
				return Ok("Created");
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (RepeatedImageException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPut]

		public async Task<IActionResult> Put(int id, [FromForm] UpdateGallaryImageDto updateGallary)
		{
			try
			{
				await _gallaryService.UpdateAsync(id, updateGallary);
				return Ok("Updated");
			}
			catch (IncorrectIdException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (IncorrectFileSizeException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
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
				await _gallaryService.Delete(id);
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
