using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public class VaccinationEmployee
{
    public Guid VaccinationEmployeeId { get; set; }

    public Guid? Employee { get; set; }

    public Guid? VaccinationLogId { get; set; }

    public virtual User? EmployeeNavigation { get; set; }

    public virtual VaccinationLog? VaccinationLog { get; set; }
}
