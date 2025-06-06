﻿using CFMS.Domain.Entities;

namespace CFMS.Application.DTOs.ChickenBatch
{
    public class ChickenBatchResponse
    {
        public Guid ChickenBatchId { get; set; }

        public string? ChickenBatchName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }

        public int MinGrowDays { get; set; }

        public int MaxGrowDays { get; set; }

        public int SoldChicken { get; set; }

        public int AliveChicken { get; set; }

        public int DeathChicken { get; set; }

        public int InitChickenQuantity { get; set; }

        public Guid? ChickenId { get; set; }

        public Guid? ChickenCoopId { get; set; }

        public Guid? ParentBatchId { get; set; }

        public Guid? CurrentStageId { get; set; }

        //[JsonIgnore]
        //public virtual ChickenBatch? ParentBatch { get; set; }

        //[JsonIgnore]
        //public virtual ChickenCoop? ChickenCoop { get; set; }

        //[JsonIgnore]
        //public virtual GrowthStage? CurrentStage { get; set; }

        public virtual Domain.Entities.Chicken? Chicken { get; set; }

        public virtual ICollection<Domain.Entities.FeedLog> FeedLogs { get; set; } = new List<Domain.Entities.FeedLog>();

        public virtual ICollection<GrowthBatch> GrowthBatches { get; set; } = new List<GrowthBatch>();

        public virtual ICollection<HealthLog> HealthLogs { get; set; } = new List<HealthLog>();

        public virtual ICollection<Domain.Entities.QuantityLog> QuantityLogs { get; set; } = new List<Domain.Entities.QuantityLog>();

        public virtual ICollection<VaccineLog> VaccineLogs { get; set; } = new List<VaccineLog>();

        public virtual ICollection<Domain.Entities.ChickenDetail> ChickenDetails { get; set; } = new List<Domain.Entities.ChickenDetail>();
    }
}
