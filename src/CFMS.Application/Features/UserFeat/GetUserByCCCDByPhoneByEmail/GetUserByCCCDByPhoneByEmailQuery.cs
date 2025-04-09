using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using MediatR;

namespace CFMS.Application.Features.UserFeat.GetUserByCCCDByPhoneByEmail
{
    public class GetUserByCCCDByPhoneByEmailQuery : IRequest<BaseResponse<UserResponse>>
    {
        public GetUserByCCCDByPhoneByEmailQuery(string searchTemp)
        {
            SearchTemp = searchTemp;
        }

        public string SearchTemp { get; set; }
    }
}
