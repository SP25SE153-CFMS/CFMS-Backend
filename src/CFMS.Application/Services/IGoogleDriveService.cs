using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

public interface IGoogleDriveService
{
    Task<string> UploadFileAsync(string filePath, string contentType, string fileName, string folderName);
}

