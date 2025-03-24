using CFMS.Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.UserFeat.Auth.SignIn
{
    public class GoogleAuthProfileQueryHandler : IRequestHandler<GoogleAuthProfileQuery, BaseResponse<string>>
    {
        private readonly IConfiguration _configuration;

        public GoogleAuthProfileQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BaseResponse<string>> Handle(GoogleAuthProfileQuery request, CancellationToken cancellationToken)
        {
            var googleAuthUrl = "https://accounts.google.com/o/oauth2/v2/auth" +
                "?client_id=" + _configuration["Authentication:Google:ClientId"] +
                "&redirect_uri=" + Uri.EscapeDataString(_configuration["Authentication:Google:RedirectUri"]) +
                "&response_type=code" +
                "&scope=openid email profile";

            return BaseResponse<string>.SuccessResponse(data: googleAuthUrl);
        }
    }
}