namespace OnlineStore.Application.Services.Products;

using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.Command.Products;
using OnlineStore.Application.ConstantBag.Products;
using OnlineStore.Application.Factory.Products;
using OnlineStore.Application.Query.Products;
using OnlineStore.Application.Services.Common;
using OnlineStore.Domain.Model.Products;

public class ProductCommandService : CommandService, IProductCommandService
{
    private readonly ITransactionOperationService _transactionOperationService;
    private readonly IProductQueryService _queryService;
    private readonly IProductCommandRepository _commandRepository;
    private readonly IProductFactory _productFactory;
    private readonly IProductPhotoFactory _productPhotoFactory;
    private readonly IFileService _fileService;

    public ProductCommandService(
        IValidationService validationService,
        ITransactionOperationService transactionOperationService,
        IProductQueryService queryService,
        IProductCommandRepository commandRepository,
        IProductFactory productFactory,
        IProductPhotoFactory productPhotoFactory,
        IFileService fileService
    ) : base(
        validationService
    ) {
        _transactionOperationService = transactionOperationService;
        _queryService = queryService;
        _commandRepository = commandRepository;
        _productFactory = productFactory;
        _productPhotoFactory = productPhotoFactory;
        _fileService = fileService;
    }

    public async Task CreateAsync(CreateProductCommand command)
    {
        await ValidateCommandAsync(command);
        Product product = _productFactory.CreateFromCommand(command);

        await _transactionOperationService.ExecuteAsTransactionAsync(async () => {
            await _commandRepository.CreateAsync(product);
            await AttachPhotos(product, command.Photos);
        });
    }

    public async Task UpdateAsync(UpdateProductCommand command)
    {
        await ValidateCommandAsync(command);
        await _transactionOperationService.ExecuteAsTransactionAsync(async () => {
            Product product = await _queryService.GetByIdAsync(command.Id);
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.CategoryId = command.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;
            await _commandRepository.UpdateAsync(product);
            await AttachPhotos(product, command.Photos);
        });
    }

    public async Task UpdateAsync(Product product)
    {
        await _commandRepository.UpdateAsync(product);
    }

    public async Task DeleteByIdAsync(int id)
    {
        Product product = await _queryService.GetByIdAsync(id, include: ProductInclude.Photos);

        if (product.Photos.Count > 0)
        {
            _fileService.DeleteFolder(
                Path.GetDirectoryName(product.Photos.First().PhotoUrl),
                recursive: true
            );
        }
        
        await _commandRepository.DeleteAsync(product);
    }

    private async Task AttachPhotos(Product product, List<string>? photos)
    {
        if (photos?.Count > 0)
        {
            string path = string.Format(
                ProductFileBag.PhotoPath,
                product.Id.ToString()
            );
            List<string> movedPhotos = await _fileService.MoveTempFilesAsync(photos, path);

            foreach (string filePath in movedPhotos)
            {
                ProductPhoto productPhoto = _productPhotoFactory.Create(
                    productId: product.Id,
                    photoUrl: filePath
                );
                product.Photos.Add(productPhoto);
            }

            await UpdateAsync(product);
        }
    }
}
