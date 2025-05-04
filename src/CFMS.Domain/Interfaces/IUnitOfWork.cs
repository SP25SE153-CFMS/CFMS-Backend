using CFMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = CFMS.Domain.Entities.Task;

namespace CFMS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<StockReceipt> StockReceiptRepository { get; }
        IGenericRepository<Assignment> AssignmentRepository { get; }
        IGenericRepository<BreedingArea> BreedingAreaRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Chicken> ChickenRepository { get; }
        IGenericRepository<ChickenBatch> ChickenBatchRepository { get; }
        IGenericRepository<ChickenCoop> ChickenCoopRepository { get; }
        IGenericRepository<ChickenDetail> ChickenDetailRepository { get; }
        IGenericRepository<CoopEquipment> CoopEquipmentRepository { get; }
        IGenericRepository<Equipment> EquipmentRepository { get; }
        IGenericRepository<EvaluatedTarget> EvaluatedTargetRepository { get; }
        IGenericRepository<EvaluationResult> EvaluationResultRepository { get; }
        IGenericRepository<EvaluationResultDetail> EvaluationResultDetailRepository { get; }
        IGenericRepository<EvaluationTemplate> EvaluationTemplateRepository { get; }
        IGenericRepository<Farm> FarmRepository { get; }
        IGenericRepository<FarmEmployee> FarmEmployeeRepository { get; }
        IGenericRepository<FeedLog> FeedLogRepository { get; }
        IGenericRepository<FeedSession> FeedSessionRepository { get; }
        IGenericRepository<Food> FoodRepository { get; }
        IGenericRepository<GrowthBatch> GrowthBatchRepository { get; }
        IGenericRepository<GrowthStage> GrowthStageRepository { get; }
        IGenericRepository<HealthLog> HealthLogRepository { get; }
        IGenericRepository<HealthLogDetail> HealthLogDetailRepository { get; }
        IGenericRepository<HarvestProduct> HarvestProductRepository { get; }
        IGenericRepository<InventoryReceipt> InventoryReceiptRepository { get; }
        IGenericRepository<InventoryReceiptDetail> InventoryReceiptDetailRepository { get; }
        IGenericRepository<InventoryRequest> InventoryRequestRepository { get; }
        IGenericRepository<InventoryRequestDetail> InventoryRequestDetailRepository { get; }
        IGenericRepository<Medicine> MedicineRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<NutritionPlan> NutritionPlanRepository { get; }
        IGenericRepository<NutritionPlanDetail> NutritionPlanDetailRepository { get; }
        IGenericRepository<QuantityLog> QuantityLogRepository { get; }
        IGenericRepository<Request> RequestRepository { get; }
        IGenericRepository<Resource> ResourceRepository { get; }
        IGenericRepository<ResourceSupplier> ResourceSupplierRepository { get; }
        IGenericRepository<RevokedToken> RevokedTokenRepository { get; }
        IGenericRepository<Shift> ShiftRepository { get; }
        IGenericRepository<ShiftSchedule> ShiftScheduleRepository { get; }
        IGenericRepository<SubCategory> SubCategoryRepository { get; }
        IGenericRepository<Domain.Entities.Task> TaskRepository { get; }
        IGenericRepository<TaskHarvest> TaskHarvestRepository { get; }
        IGenericRepository<TaskLocation> TaskLocationRepository { get; }
        IGenericRepository<TaskLog> TaskLogRepository { get; }
        IGenericRepository<TaskRequest> TaskRequestRepository { get; }
        IGenericRepository<TaskResource> TaskResourceRepository { get; }
        //IGenericRepository<FrequencySchedule> FrequencyScheduleRepository { get; }
        IGenericRepository<TemplateCriterion> TemplateCriterionRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<VaccineLog> VaccineLogRepository { get; }
        IGenericRepository<WarePermission> WarePermissionRepository { get; }
        IGenericRepository<WareStock> WareStockRepository { get; }
        IGenericRepository<WareTransaction> WareTransactionRepository { get; }
        IGenericRepository<Warehouse> WarehouseRepository { get; }
        IGenericRepository<Supplier> SupplierRepository { get; }
        IGenericRepository<SystemConfig> SystemConfigRepository { get; }

        void Save();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);
    }
}
