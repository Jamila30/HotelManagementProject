namespace Hotel.Business.Helper_Services.Implementations
{
	public class StripeService : IStripeService
	{
		private readonly TokenService _tokenService;
		private readonly CustomerService _customerService;
		private readonly ChargeService _chargeService;
		private readonly UserManager<AppUser> _userManager;
		public StripeService(
			TokenService tokenService,
			CustomerService customerService,
			ChargeService chargeService,
			UserManager<AppUser> userManager)
		{
			_tokenService = tokenService;
			_customerService = customerService;
			_chargeService = chargeService;
			_userManager = userManager;
		}

		public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
		{
			var tokenOptions = new TokenCreateOptions
			{
				Card = new TokenCardOptions
				{
					Name = resource.Card.Name,
					Number = resource.Card.Number,
					ExpYear = resource.Card.ExpiryYear,
					ExpMonth = resource.Card.ExpiryMonth,
					Cvc = resource.Card.Cvc
				}
			};
			var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);
			
			//var emailCheck = _userManager.FindByEmailAsync(resource.Email);
			//if (emailCheck == null) throw new NotFoundException("There is no user with this email");

			var options = new CustomerSearchOptions
			{
				Query = $"email:'{resource.Email}'",

			};
			var service = new CustomerService();
			var searchresult = service.Search(options);
			var result = searchresult.Select(x => x.Id).ToList().FirstOrDefault();

			Customer customer = new Customer();
			if (result is null)
			{
				var customerOptions = new CustomerCreateOptions
				{
					Email = resource.Email,
					Name = resource.Name,
					Source = token.Id
				};
				customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);
			}
			else
			{

				customer = await _customerService.GetAsync(result, null, null, cancellationToken);
			}


			return new CustomerResource(customer.Id, customer.Email, customer.Name);
		}

		public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
		{
			var chargeOptions = new ChargeCreateOptions
			{
				Currency = resource.Currency,
				Amount = resource.Amount,
				ReceiptEmail = resource.ReceiptEmail,
				Customer = resource.CustomerId,
				Description = resource.Description
			};

			var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);

			return new ChargeResource(
				charge.Id,
				charge.Currency,
				charge.Amount,
				charge.CustomerId,
				charge.ReceiptEmail,
				charge.Description);
		}
	}
}
