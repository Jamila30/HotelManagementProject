namespace Hotel.Core.Entities.Stripe
{
	public record CreateCustomerResource(
	string Email,
	string Name,
	CreateCardResource Card);
}
