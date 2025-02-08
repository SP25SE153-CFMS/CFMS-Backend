using System;
using System.Collections.Generic;

namespace CFMS.Domain.Entities;

public partial class Task
{
    public Guid Taskid { get; set; }

    public string Taskname { get; set; } = null!;

    public string? Description { get; set; }

    public string? Tasktype { get; set; }

    public DateTime? Createddate { get; set; }

    public DateTime? Duedate { get; set; }

    public string? Status { get; set; }

    public string? Location { get; set; }

    public Guid? Userid { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Dailytask> Dailytasks { get; set; } = new List<Dailytask>();

    public virtual ICollection<Harvesttask> Harvesttasks { get; set; } = new List<Harvesttask>();

    public virtual User? User { get; set; }

    public virtual ICollection<Workschedule> Workschedules { get; set; } = new List<Workschedule>();
}
