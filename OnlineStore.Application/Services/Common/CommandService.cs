namespace OnlineStore.Application.Services.Common;

using FluentValidation.Results;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.Exceptions.Common;

public abstract class CommandService
{
    protected readonly IValidationService _validationService;

    protected CommandService(
        IValidationService validationService
    ) {
        _validationService = validationService;
    }

    protected async Task ValidateCommandAsync<TCommand>(TCommand command)
    {
        ValidationResult validationResult = await _validationService.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(string.Format(
                "Validation failed for {0}. Errors: {1}",
                typeof(TCommand).Name,
                string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
            ));
        }
    }
}
