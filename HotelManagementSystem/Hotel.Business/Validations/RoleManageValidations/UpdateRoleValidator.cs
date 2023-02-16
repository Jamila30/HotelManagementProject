using Hotel.Business.DTOs.RoleManageDTOs;

namespace Hotel.Business.Validations.RoleManageValidations
{
	public class UpdateRoleValidator:AbstractValidator<UpdateRoleDto>
	{
		public UpdateRoleValidator()
		{
			RuleFor(x => x.OldRoleName).NotNull().NotEmpty();
			RuleFor(x => x.NewRoleName).NotNull().NotEmpty();
		}
	}
}
