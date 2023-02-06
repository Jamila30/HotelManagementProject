using FluentValidation;
using Hotel.Business.DTOs.WhyUsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Validations.WhyUsValidations
{
	public class UpdateWhyUsValidator:AbstractValidator<UpdateWhyUsDto>
	{
		public UpdateWhyUsValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter vtrue Id format");
				}
			});

			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull()
				.MaximumLength(250);
		}
	}
}
