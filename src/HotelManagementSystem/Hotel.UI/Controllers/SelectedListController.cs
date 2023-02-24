using Hotel.Business.DTOs.SelectedListDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SelectedListController : ControllerBase
	{
		private readonly ISelectedListService _selectedListService;
		public SelectedListController(ISelectedListService selectedListService)
		{
			_selectedListService = selectedListService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _selectedListService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("searchByFlatId/{flatId}")]
		public async Task<IActionResult> GetByTitle(int flatId)
		{
			try
			{
				var slider = await _selectedListService.GetByCondition(x => x.FlatId == flatId);
				return Ok(slider);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}

		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var slider = await _selectedListService.GetByIdAsync(id);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}
		[HttpGet("FindTotalPrice")]
		public async Task<IActionResult> GetTotalPrice()
		{
			try
			{
				var totalPrice = await _selectedListService.GetTotalPrice();
				return Ok(totalPrice);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}
		[HttpPost("AddToList")]
		public async Task<IActionResult> AddToList([FromBody] List<int> flatIds)
		{
			try
			{
				await _selectedListService.AddToList(flatIds);
				return Ok();
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("UpdateList/{catagoryId}")]
		public async Task<IActionResult> Update(int catagoryId, UpdateSelectedListDto updateList)
		{
			try
			{
				await _selectedListService.UpdateAsync(catagoryId, updateList);
				return Ok("Updated");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (IncorrectIdException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete("DeleteItemsOfOneCatagory/{catagoryId}")]
		public async Task<IActionResult> DeleteOfOneCatagory(int catagoryId)
		{
			try
			{
				await _selectedListService.DeleteOfOneCatagory(catagoryId);
				return Ok("Deleted items which belong to this catagory");
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete("DeleteAllSelectedListItems")]
		public async Task<IActionResult> DeleteAllItems()
		{
			try
			{
				await _selectedListService.DeleteAllListItems();
				return Ok("Deleted");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

	}
}
