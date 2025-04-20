using CFMS.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMS.Application.Features.SupplierFeat.Create
{ 
    public class CreateSupplierCommand : IRequest<BaseResponse<bool>>
    {
        public CreateSupplierCommand(string? supplierName, string? supplierCode, string? address, string? phoneNumber, string? bankAccount, Guid? farmId, int? status)
        {
            SupplierName = supplierName;
            SupplierCode = supplierCode;
            Address = address;
            PhoneNumber = phoneNumber;
            BankAccount = bankAccount;
            FarmId = farmId;
            Status = status;
        }

        public string? SupplierName { get; set; }

        public string? SupplierCode { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? BankAccount { get; set; }

        public Guid? FarmId { get; set; }

        public int? Status { get; set; }
    }
}
