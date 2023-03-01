using FluentValidation;
using ShoppingPlanApi.Dtos;

namespace ShoppingPlanApi.Validations
{
    public class ShoppingListDetailPutValidation:AbstractValidator<ShoppingListDetailPutDto>
    {
        public ShoppingListDetailPutValidation()
        {
            
            RuleFor(c => c.ProductID).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt Bulunamadı");
            RuleFor(c => c.Quantity).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Miktar 0 dan büyük olmalıdır");
            RuleFor(c => c.MeasurementID).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt Bulunamadı");
        }
    }
}
