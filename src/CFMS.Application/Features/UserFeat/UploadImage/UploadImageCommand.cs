using CFMS.Application.Common;
using CFMS.Application.DTOs.Image;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CFMS.Application.Features.UserFeat.UploadImage
{
    public class UploadImageCommand : IRequest<BaseResponse<UploadImageResult>>
    {
        public IFormFile File { get; set; }
    }
}
