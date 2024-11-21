namespace OnlineStore.Application.Api.Services.Common;

using FluentValidation.Results;

public interface IValidationService
{
    Task<ValidationResult> ValidateAsync<T>(T instance);
}

public interface IValidationService<T>
{
    Task<ValidationResult> ValidateAsync(T instance);
}
