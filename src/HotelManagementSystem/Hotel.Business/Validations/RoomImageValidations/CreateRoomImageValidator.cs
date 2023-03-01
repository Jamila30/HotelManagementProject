namespace Hotel.Business.Validations.RoomImageValidations
{
	public class CreateRoomImageValidator:AbstractValidator<CreateRoomImageDto>
	{
		public CreateRoomImageValidator()
		{
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.FlatId).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			})
				.NotEmpty()
				.NotNull();
		}
	}
}
