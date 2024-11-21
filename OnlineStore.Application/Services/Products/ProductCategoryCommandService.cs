namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.Command.Products;
using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.Factory.Products;
using OnlineStore.Application.Services.Common;
using OnlineStore.Domain.Model.Products;

public class ProductCategoryCommandService : CommandService, IProductCategoryCommandService
{
    private readonly ITransactionOperationService _transactionOperationService;
    private readonly IProductCategoryQueryService _queryService;
    private readonly IProductCategoryCommandRepository _commandRepository;
    private readonly IProductCategoryFactory _categoryFactory;
    private readonly IFileService _fileService;

    public ProductCategoryCommandService(
        IValidationService validationService,
        ITransactionOperationService transactionOperationService,
        IProductCategoryQueryService queryService,
        IProductCategoryCommandRepository commandRepository,
        IProductCategoryFactory categoryFactory,
        IFileService fileService
    ) : base(
        validationService
    ) {
        _transactionOperationService = transactionOperationService;
        _queryService = queryService;
        _commandRepository = commandRepository;
        _categoryFactory = categoryFactory;
        _fileService = fileService;
    }

    public async Task CreateAsync(CreateProductCategoryCommand command)
    {
        await ValidateCommandAsync(command);
        ProductCategory category = _categoryFactory.CreateFromCommand(command);

        await _transactionOperationService.ExecuteAsTransactionAsync(async () => {
            await _commandRepository.CreateAsync(category);
            if (command.Photo != null)
            {
                await AttachPhoto(category, command.Photo);
            }
        });
    }

    public async Task UpdateAsync(UpdateProductCategoryCommand command)
    {
        await ValidateCommandAsync(command);
        await _transactionOperationService.ExecuteAsTransactionAsync(async () => {
            ProductCategory category = await _queryService.GetByIdAsync(command.Id);
            category.Name = command.Name;
            category.Description = command.Description;
            category.UpdatedAt = DateTime.UtcNow;
            if (command.Photo != null)
            {
                await AttachPhoto(category, command.Photo);
            }
            await _commandRepository.UpdateAsync(category);
        });
    }

    public async Task UpdateAsync(ProductCategory category)
    {
        await _commandRepository.UpdateAsync(category);
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _commandRepository.DeleteByIdAsync(id);
    }

    private async Task AttachPhoto(ProductCategory category, string tempPhoto)
    {
        if (category.Photo != null)
        {
            _fileService.DeleteFile(category.Photo);
        }

        string path = string.Format(
            ProductCategoryFileBag.PhotoPath,
            category.Id.ToString()
        );
        string movedPhoto = await _fileService.MoveTempFileAsync(tempPhoto, path);
        category.Photo = movedPhoto;
        await UpdateAsync(category);
    }
}
