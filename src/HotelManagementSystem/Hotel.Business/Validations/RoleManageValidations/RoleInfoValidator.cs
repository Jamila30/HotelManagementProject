using Hotel.Business.DTOs.RoleManageDTOs;

namespace Hotel.Business.Validations.RoleManageValidations
{
	public class RoleInfoValidator:AbstractValidator<RoleInfoDto>
	{
		public RoleInfoValidator()
		{
			RuleFor(x=>x.RoleId).NotNull().NotEmpty();
			RuleFor(x=>x.RoleName).NotNull().NotEmpty();
		}
	}
}
