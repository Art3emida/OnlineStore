namespace OnlineStore.Application.Api.Services.Common;

using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<string> SaveTempFileAsync(IFormFile file, string filePrefix);
    Task<List<string>> SaveTempFilesAsync(IFormFileCollection files, string filePrefix);
    Task<string> MoveTempFileAsync(string fileName, string destPath);
    Task<List<string>> MoveTempFilesAsync(List<string> fileNames, string destPath);
    void DeleteTempFile(string fileName);
    void DeleteTempFiles(List<string> fileNames);
    void DeleteFile(string filePath);
    void DeleteFolder(string folderPath, bool recursive = false);
}
