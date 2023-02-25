using Hotel.Business.DTOs.SettingTableDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		private readonly ISettingsService _settingsService;
		public SettingsController(ISettingsService settingsService)
		{
			_settingsService = settingsService;
		}

		[HttpGet]
		public async Task<ActionResult> GetAll() {
			try
			{
				var result= await _settingsService.GetAllAsync();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("{keyName}")]

		public async Task<ActionResult> GetByKey(string keyName)
		{
			try
			{
				var result = await _settingsService.GetByCondition(x=>x.Key==keyName);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("getAllKeys")]

		public async Task<ActionResult> GetKeys()
		{
			try
			{
				var result = await _settingsService.AllKey();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("getAllValues")]
		public async Task<ActionResult> GetValues()
		{
			try
			{
				var result = await _settingsService.AllValues();
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> Create(DictionaryDto dictionaryDto)
		{
			try
			{
				await _settingsService.Create(dictionaryDto);
				return Ok("Created");
			}
			catch(NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("updateByKey/{key}")]
		public async Task<ActionResult> Update(string key, DictionaryDto dictionaryDto)
		{
			try
			{
				await _settingsService.UpdateValueAsync(key,dictionaryDto);
				return Ok("updated");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
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

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				await _settingsService.Delete(id);
				return Ok("Deleted");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

	}
}
