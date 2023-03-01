namespace Hotel.Business.Helper_Services.Interfaces
{
	public interface IStripeService
	{
		Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
		Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);

	}
}
