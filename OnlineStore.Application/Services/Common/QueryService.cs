namespace OnlineStore.Application.Services.Common;

using FluentValidation.Results;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.Exceptions.Common;

public abstract class QueryService
{
    protected readonly IValidationService _validationService;

    protected QueryService(
        IValidationService validationService
    ) {
        _validationService = validationService;
    }

    protected async Task ValidateQueryAsync<TQuery>(TQuery query)
    {
        ValidationResult validationResult = await _validationService.ValidateAsync(query);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(string.Format(
                "Validation failed for {0}. Errors: {1}",
                typeof(TQuery).Name,
                string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            ));
        }
    }
}
