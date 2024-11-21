namespace OnlineStore.Infrastructure.Services.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.ConstantBag.Common;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(
        IWebHostEnvironment webHostEnvironment
    ) {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> SaveTempFileAsync(IFormFile file, string filePrefix)
    {
        string tempDir = GetTempDir();

        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        string randomPart = Guid.NewGuid().ToString().Substring(0, 8);
        string fileName = $"{filePrefix}_{randomPart}{Path.GetExtension(file.FileName)}";
        string tempPath = Path.Combine(tempDir, fileName);

        using (var stream = new FileStream(tempPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task<List<string>> SaveTempFilesAsync(IFormFileCollection files, string filePrefix)
    {
        var fileNames = new List<string>();

        foreach (var file in files)
        {
            string fileName = await SaveTempFileAsync(file, filePrefix);
            fileNames.Add(fileName);
        }

        return fileNames;
    }

    public async Task<string> MoveTempFileAsync(string fileName, string destPath)
    {
        string targetPath = Path.Combine(_webHostEnvironment.WebRootPath, destPath);

        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        
        string tempFilePath = Path.Combine(GetTempDir(), fileName);
        string targetFilePath = Path.Combine(targetPath, fileName);

        if (!File.Exists(tempFilePath))
        {
            throw new FileNotFoundException(string.Format(
                FileBag.FileNotFound,
                fileName
            ));
        }

        await Task.Run(() => File.Move(tempFilePath, targetFilePath));

        return Path.Combine(destPath, fileName);
    }

    public async Task<List<string>> MoveTempFilesAsync(List<string> fileNames, string destPath)
    {
        var filePaths = new List<string>();

        foreach (string fileName in fileNames)
        {
            string filePath = await MoveTempFileAsync(fileName, destPath);
            filePaths.Add(filePath);
        }

        return filePaths;
    }

    public void DeleteTempFile(string fileName)
    {
        string tempFilePath = Path.Combine(GetTempDir(), fileName);

        if (File.Exists(tempFilePath))
        {
            try
            {
                File.Delete(tempFilePath);
            }
            catch (Exception ex)
            {
            }
        }
    }

    public void DeleteTempFiles(List<string> fileNames)
    {
        foreach (var fileName in fileNames)
        {
            DeleteTempFile(fileName);
        }
    }

    public void DeleteFile(string filePath)
    {
        filePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
            }
        }
    }
    
    public void DeleteFolder(string folderPath, bool recursive = false)
    {
        folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

        if (Directory.Exists(folderPath))
        {
            try
            {
                Directory.Delete(folderPath, recursive);
            }
            catch (Exception ex)
            {
            }
        }
    }

    private string GetTempDir()
    {
        return Path.Combine(_webHostEnvironment.WebRootPath, FileBag.TempDirectory);
    }
}
