namespace OnlineStore.Infrastructure.Services.Common;

using FluentValidation;
using FluentValidation.Results;
using OnlineStore.Application.Api.Services.Common;

public class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ValidationResult> ValidateAsync<T>(T instance)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(typeof(T));
        var validator = (IValidator<T>)_serviceProvider.GetService(validatorType);

        if (validator == null)
        {
            return await Task.FromResult(new ValidationResult());
        }

        return await validator.ValidateAsync(instance);
    }
}

public class ValidationService<T> : IValidationService<T>
{
    private readonly IValidator<T> _validator;

    public ValidationService(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task<ValidationResult> ValidateAsync(T instance)
    {
        return await _validator.ValidateAsync(instance);
    }
}
