using Hotel.Business.Services.Interfaces.ForStripes;
using Hotel.Core.Entities.Stripe;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StripeController : ControllerBase
	{
		private readonly IStripeService _stripeService;

		public StripeController(IStripeService stripeService)
		{
			_stripeService = stripeService;
		}

		[HttpPost("customer")]
		public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource,
			CancellationToken cancellationToken)
		{
			try
			{
				var response = await _stripeService.CreateCustomer(resource, cancellationToken);
				return Ok(response);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("charge")]
		public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource, CancellationToken cancellationToken)
		{
			try
			{
				var response = await _stripeService.CreateCharge(resource, cancellationToken);
				return Ok(response);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
