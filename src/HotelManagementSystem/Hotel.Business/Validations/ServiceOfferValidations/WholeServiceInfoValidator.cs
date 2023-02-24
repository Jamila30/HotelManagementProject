﻿using Hotel.Business.DTOs.ServiceOfferDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Validations.ServiceOfferValidations
{
	public class WholeServiceInfoValidator:AbstractValidator<WholeServiceInfoDto>
	{
		public WholeServiceInfoValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Price)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.IsFree)
				.NotNull();
			
		}
	}
}