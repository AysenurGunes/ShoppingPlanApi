using FluentValidation;
using ShoppingPlanApi.Models;

namespace ShoppingPlanApi.Validations
{
    public class ShoppingListValidation : AbstractValidator<ShoppingList>
    {
        public ShoppingListValidation()
        {

            RuleFor(c => c.CategoryID).GreaterThan(0).WithErrorCode(StatusCodes.Status204NoContent.ToString()).WithMessage("Kayıt Bulunamadı");
            RuleFor(c => c.ShoppingListName).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("Liste isim alanı boş geçilemez.");

        }
    }
}
