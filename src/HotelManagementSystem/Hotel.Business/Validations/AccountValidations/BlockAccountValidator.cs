using Hotel.Business.DTOs.AccountsDTOs;

namespace Hotel.Business.Validations.AccountValidations
{
	public class BlockAccountValidator:AbstractValidator<BlockAccountDto>
	{
		public BlockAccountValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.EndDate)
				.NotEmpty()
				.NotNull();
		}
	}
}
