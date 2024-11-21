namespace OnlineStoreWeb.Areas.Common.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.ConstantBag.Common;

[Area("common")]
public class FileController : Controller
{
    private readonly IFileService _fileService;

    public FileController(
        IFileService fileService
    ) {
        _fileService = fileService;
    }

    [HttpPost]
    [Route("file/upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string filePrefix)
    {
        if (file.Length == 0)
        {
            return BadRequest(FileBag.NoFileUploaded);
        }

        string fileName = await _fileService.SaveTempFileAsync(file, filePrefix);

        return Ok(new { fileName = fileName });
    }

    [HttpPost]
    [Route("file/delete")]
    public IActionResult DeleteFile([FromForm] string fileName)
    {
        _fileService.DeleteTempFile(fileName);

        return Ok();
    }
}
