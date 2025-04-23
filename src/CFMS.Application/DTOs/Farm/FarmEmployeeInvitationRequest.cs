using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.DTOs.Farm
{
    public class FarmEmployeeInvitationRequest
    {
        public Guid? UserId { get; set; }
        public int? FarmRole { get; set; }
    }
}
