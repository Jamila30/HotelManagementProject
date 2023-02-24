namespace Hotel.Business.Validations.RoomCatagoryValidations
{
	public class CreateRoomCatagoryValidator:AbstractValidator<CreateCatagoryDto>
	{
		public CreateRoomCatagoryValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
		}
	}
}
