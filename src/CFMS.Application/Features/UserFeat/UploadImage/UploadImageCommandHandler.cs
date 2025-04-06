using CFMS.Application.Common;
using CFMS.Application.DTOs.Image;
using CFMS.Domain.Interfaces;
using CFMS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, BaseResponse<UploadImageResult>>
{
    private readonly IGoogleDriveService _googleDriveService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UploadImageCommandHandler(IGoogleDriveService driveService, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    {
        _googleDriveService = driveService;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<UploadImageResult>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUserService.GetUserId();

        var existUser = _unitOfWork.UserRepository.Get(filter: u => u.UserId.ToString().Equals(user)).FirstOrDefault();

        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName));

        try
        {
            using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            var folderName = "CFMS-Images";

            var fileId = await _googleDriveService.UploadFileAsync(tempPath, request.File.ContentType, $"{user}_{DateTime.UtcNow.Ticks}.jpg", folderName);
            var result = new UploadImageResult
            {
                FileId = fileId,
                FileUrl = $"https://drive.usercontent.google.com/download?id={fileId}&export=view&authuser=0"
                
            };

            existUser.Avatar = result.FileUrl;
            _unitOfWork.UserRepository.Update(existUser);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<UploadImageResult>.SuccessResponse(result);
        }
        catch (Exception ex)
        {
            return BaseResponse<UploadImageResult>.FailureResponse("Upload failed" + ex.Message);
        }
        finally
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }
}
