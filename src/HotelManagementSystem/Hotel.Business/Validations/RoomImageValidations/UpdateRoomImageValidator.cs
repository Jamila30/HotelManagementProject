namespace Hotel.Business.Validations.RoomImageValidations
{
	public class UpdateRoomImageValidator:AbstractValidator<UpdateRoomImageDto>
	{
		public UpdateRoomImageValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});
			RuleFor(x => x.Image)
				.NotEmpty();
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
