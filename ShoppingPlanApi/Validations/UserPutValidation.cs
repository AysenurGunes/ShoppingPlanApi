using FluentValidation;
using ShoppingPlanApi.Dtos;

namespace ShoppingPlanApi.Validations
{
    public class UserPutValidation:AbstractValidator<UserPutDto>
    {
        public UserPutValidation() 
        {
            RuleFor(c => c.Name).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("İsim alanı boş geçilemez");
            RuleFor(c => c.Surname).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("Soyad alanı boş geçilemez");
            RuleFor(c => c.Email).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("Mail alanı boş geçilemez");
            RuleFor(c => c.Password).NotEmpty().WithErrorCode(StatusCodes.Status406NotAcceptable.ToString()).WithMessage("Şifre alanı boş geçilemez");
        }
    }
}
