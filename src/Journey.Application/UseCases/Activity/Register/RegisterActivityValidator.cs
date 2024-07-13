using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activity.Register
{
    public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        public RegisterActivityValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessage.NAME_EMPTY);
        }
    }
}
