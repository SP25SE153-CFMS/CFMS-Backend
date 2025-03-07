//using CFMS.Application.Common;
//using CFMS.Application.Services;
//using CFMS.Domain.Interfaces;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CFMS.Application.Features.User.Auth
//{
//    public class SignUpHandler : IRequestHandler<SignUpCommand, BaseResponse<string>>
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly ITokenService _tokenService;

//        public SignUpHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
//        {
//            _unitOfWork = unitOfWork;
//            _tokenService = tokenService;
//        }

//        public Task<BaseResponse<string>> Handle(SignUpCommand request, CancellationToken cancellationToken)
//        {
//            var existUser = _unitOfWork.UserRepository.Get().FirstOrDefault(x => x.Mail == request.Mail);

//            if (existUser == null)
//            {

//                if (request.Mail.Equals(_unitOfWork.UserRepository.Get()) && request.Password == "password123")
//                {
//                    var user = new User { Email = request.Email, FullName = "Test User" };

//                    var accessToken = _tokenService.GenerateAccessToken(user);
//                    var refreshToken = _tokenService.GenerateRefreshToken();

//                    var authResponse = new AuthResponse
//                    {
//                        AccessToken = accessToken,
//                        RefreshToken = refreshToken
//                    };

//                    return BaseResponse<AuthResponse>.SuccessResponse(authResponse, "Login successful");
//                }

//                return BaseResponse<AuthResponse>.FailureResponse("Invalid email or password");
//            }
//        }
//    }
