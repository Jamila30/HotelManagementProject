using Hotel.Business.DTOs.ServiceImageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Validations.ServiceImageValidations
{
	public class UpdateServiceImageValidator:AbstractValidator<UpdateServiceImageDto>
	{
		public UpdateServiceImageValidator()
		{
			RuleFor(x => x.Id).Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.ServiceOfferId).Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.ServiceOfferId)
				.NotNull()
				.NotEmpty();
		}
	}
}
