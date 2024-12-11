using FluentValidation;
using MetaExchange.WebApi.Contracts.Requests;

namespace MetaExchange.WebApi.Contracts.Validators;

public class ExecutionPlanRequestValidator : AbstractValidator<ExecutionPlanRequest>
{
    public ExecutionPlanRequestValidator()
    {
        RuleFor(request => request.Amount)
            .GreaterThan(0)
            .LessThanOrEqualTo(50000);
    }
}
