using FluentValidation;

namespace AgentAgnostiskPlatform.Application.Contracts.DTOs.Manager;

public class ManagerCreateRequestDTOValidator : AbstractValidator<ManagerCreateRequestDTO>
{
    public ManagerCreateRequestDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username must not be empty.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty.")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
    }
}
