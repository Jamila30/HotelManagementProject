using Hotel.Core.Entities.Stripe;

namespace Hotel.Business.Services.Interfaces.ForStripes
{
	public interface IStripeService
    {
		Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
		Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);

	}
}
