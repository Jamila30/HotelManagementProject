using Hotel.Business.DTOs.SelectedListDTOs;

namespace Hotel.Business.Validations.SelectedListValidations
{
    public class UpdateSelectedListValidator : AbstractValidator<UpdateSelectedListDto>
    {
        public UpdateSelectedListValidator()
        {
            RuleFor(u => u.CatagoryId).NotNull().NotEmpty().Custom((CatagoryId, context) =>
            {
                if (!int.TryParse(CatagoryId.ToString(), out int catagoryId))
                {
                    context.AddFailure("enter true format for id");
                }
            });
            RuleFor(u => u.FlatIds).NotNull().NotEmpty();
        }
    }
}
