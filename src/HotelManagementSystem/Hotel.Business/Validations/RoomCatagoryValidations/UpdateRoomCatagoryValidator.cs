namespace Hotel.Business.Validations.RoomCatagoryValidations
{
	public class UpdateRoomCatagoryValidator:AbstractValidator<RoomCatagory>
	{
		public UpdateRoomCatagoryValidator()
		{
			RuleFor(x => x.Id).Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});

			RuleFor(x => x.Name)
				.MaximumLength(70);
		}
	}
}
