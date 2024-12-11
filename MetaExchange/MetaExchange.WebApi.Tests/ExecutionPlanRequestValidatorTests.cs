using FluentValidation.TestHelper;
using MetaExchange.WebApi.Contracts.Requests;
using MetaExchange.WebApi.Contracts.Validators;

namespace MetaExchange.WebApi.Tests;

public class ExecutionPlanRequestValidatorTests
{
    private readonly ExecutionPlanRequestValidator _validator = new();
    
    [Fact]
    public void Should_HaveError_WhenAmountIsLessThanOrEqualToZero()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = 0};

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Amount);
    }
       
    [Fact]
    public void Should_HaveError_WhenAmountExceedsLimit()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = 60000 };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(r => r.Amount);
    }
    
    [Fact]
    public void Should_NotHaveError_WhenAmountIsWithinRange()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = 25000 };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(r => r.Amount);
    }
}