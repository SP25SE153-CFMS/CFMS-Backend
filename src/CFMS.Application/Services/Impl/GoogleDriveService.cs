using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.Extensions.Configuration;
using File = Google.Apis.Drive.v3.Data.File;

public class GoogleDriveService : IGoogleDriveService
{
    private readonly DriveService _driveService;
    private readonly string _folderId;

    public GoogleDriveService(IConfiguration configuration)
    {
        var credentialPath = configuration["GoogleDrive:ServiceAccountKeyPath"];
        _folderId = configuration["GoogleDrive:FolderId"];

        GoogleCredential credential;
        using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(DriveService.Scope.DriveFile);
        }

        _driveService = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "CFMS-DriveImage"
        });
    }

    public async Task<string> UploadFileAsync(string filePath, string contentType, string fileName, string folderName)
    {
        var request = _driveService.Files.List();
        request.Q = $"name = '{folderName}' and mimeType = 'application/vnd.google-apps.folder' and trashed = false";
        var result = await request.ExecuteAsync();

        string folderId;
        if (result.Files.Count > 0)
        {
            folderId = result.Files[0].Id;
        }
        else
        {
            var folderMetadata = new File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };
            var folder = await _driveService.Files.Create(folderMetadata).ExecuteAsync();
            folderId = folder.Id;
        }

        var fileMeta = new File
        {
            Name = fileName,
            Parents = new List<string> { folderId }
        };

        FilesResource.CreateMediaUpload requestUpload;

        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            requestUpload = _driveService.Files.Create(fileMeta, stream, contentType);
            requestUpload.Fields = "id, webViewLink, webContentLink";

            await requestUpload.UploadAsync();
        }

        var file = requestUpload.ResponseBody;

        var permissionForAll = new Permission
        {
            Type = "anyone",
            Role = "reader"
        };
        await _driveService.Permissions.Create(permissionForAll, file.Id).ExecuteAsync();

        return file.Id;
    }
}
