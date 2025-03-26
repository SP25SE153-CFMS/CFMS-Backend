using CFMS.Domain.Interfaces;
using CFMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using Task = CFMS.Domain.Entities.Task;
using CFMS.Infrastructure.Persistence;

namespace CFMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CfmsDbContext context;

        private GenericRepository<Assignment> assignmentRepository;
        private GenericRepository<BreedingArea> breedingAreaRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Chicken> chickenRepository;
        private GenericRepository<ChickenBatch> chickenBatchRepository;
        private GenericRepository<ChickenCoop> chickenCoopRepository;
        private GenericRepository<ChickenDetail> chickenDetailRepository;
        private GenericRepository<CoopEquipment> coopEquipmentRepository;
        private GenericRepository<Equipment> equipmentRepository;
        private GenericRepository<EvaluatedTarget> evaluatedTargetRepository;
        private GenericRepository<EvaluationResult> evaluationResultRepository;
        private GenericRepository<EvaluationResultDetail> evaluationResultDetailRepository;
        private GenericRepository<EvaluationTemplate> evaluationTemplateRepository;
        private GenericRepository<Farm> farmRepository;
        private GenericRepository<FarmEmployee> farmEmployeeRepository;
        private GenericRepository<FeedLog> feedLogRepository;
        private GenericRepository<FeedSession> feedSessionRepository;
        private GenericRepository<Food> foodRepository;
        private GenericRepository<GrowthBatch> growthBatchRepository;
        private GenericRepository<GrowthStage> growthStageRepository;
        private GenericRepository<HealthLog> healthLogRepository;
        private GenericRepository<HealthLogDetail> healthLogDetailRepository;
        private GenericRepository<InventoryReceipt> inventoryReceiptRepository;
        private GenericRepository<InventoryReceiptDetail> inventoryReceiptDetailRepository;
        private GenericRepository<InventoryRequest> inventoryRequestRepository;
        private GenericRepository<InventoryRequestDetail> inventoryRequestDetailRepository;
        private GenericRepository<Medicine> medicineRepository;
        private GenericRepository<Notification> notificationRepository;
        private GenericRepository<NutritionPlan> nutritionPlanRepository;
        private GenericRepository<NutritionPlanDetail> nutritionPlanDetailRepository;
        private GenericRepository<QuantityLog> quantityLogRepository;
        private GenericRepository<Request> requestRepository;
        private GenericRepository<Resource> resourceRepository;
        private GenericRepository<ResourceSupplier> resourceSupplierRepository;
        private GenericRepository<RevokedToken> revokedTokenRepository;
        private GenericRepository<Shift> shiftRepository;
        private GenericRepository<ShiftSchedule> shiftScheduleRepository;
        private GenericRepository<SubCategory> subCategoryRepository;
        private GenericRepository<Domain.Entities.Task> taskRepository;
        private GenericRepository<TaskHarvest> taskHarvestRepository;
        private GenericRepository<TaskLocation> taskLocationRepository;
        private GenericRepository<TaskLog> taskLogRepository;
        private GenericRepository<TaskRequest> taskRequestRepository;
        private GenericRepository<TaskResource> taskResourceRepository;
        private GenericRepository<TaskSchedule> taskScheduleRepository;
        private GenericRepository<TemplateCriterion> templateCriterionRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<VaccineLog> vaccineLogRepository;
        private GenericRepository<WarePermission> warePermissionRepository;
        private GenericRepository<WareStock> wareStockRepository;
        private GenericRepository<WareTransaction> wareTransactionRepository;
        private GenericRepository<Warehouse> warehouseRepository;
        private GenericRepository<SystemConfig> systemConfigRepository;
        private GenericRepository<Supplier> supplierRepository;

        public UnitOfWork(CfmsDbContext _context)
        {
            context = _context;
        }

        public IGenericRepository<Assignment> AssignmentRepository => assignmentRepository ??= new GenericRepository<Assignment>(context);
        public IGenericRepository<BreedingArea> BreedingAreaRepository => breedingAreaRepository ??= new GenericRepository<BreedingArea>(context);
        public IGenericRepository<Category> CategoryRepository => categoryRepository ??= new GenericRepository<Category>(context);
        public IGenericRepository<Chicken> ChickenRepository => chickenRepository ??= new GenericRepository<Chicken>(context);
        public IGenericRepository<ChickenBatch> ChickenBatchRepository => chickenBatchRepository ??= new GenericRepository<ChickenBatch>(context);
        public IGenericRepository<ChickenCoop> ChickenCoopRepository => chickenCoopRepository ??= new GenericRepository<ChickenCoop>(context);
        public IGenericRepository<ChickenDetail> ChickenDetailRepository => chickenDetailRepository ??= new GenericRepository<ChickenDetail>(context);
        public IGenericRepository<CoopEquipment> CoopEquipmentRepository => coopEquipmentRepository ??= new GenericRepository<CoopEquipment>(context);
        public IGenericRepository<Equipment> EquipmentRepository => equipmentRepository ??= new GenericRepository<Equipment>(context);
        public IGenericRepository<EvaluatedTarget> EvaluatedTargetRepository => evaluatedTargetRepository ??= new GenericRepository<EvaluatedTarget>(context);
        public IGenericRepository<EvaluationResult> EvaluationResultRepository => evaluationResultRepository ??= new GenericRepository<EvaluationResult>(context);
        public IGenericRepository<EvaluationResultDetail> EvaluationResultDetailRepository => evaluationResultDetailRepository ??= new GenericRepository<EvaluationResultDetail>(context);
        public IGenericRepository<EvaluationTemplate> EvaluationTemplateRepository => evaluationTemplateRepository ??= new GenericRepository<EvaluationTemplate>(context);
        public IGenericRepository<Farm> FarmRepository => farmRepository ??= new GenericRepository<Farm>(context);
        public IGenericRepository<FarmEmployee> FarmEmployeeRepository => farmEmployeeRepository ??= new GenericRepository<FarmEmployee>(context);
        public IGenericRepository<FeedLog> FeedLogRepository => feedLogRepository ??= new GenericRepository<FeedLog>(context);
        public IGenericRepository<FeedSession> FeedSessionRepository => feedSessionRepository ??= new GenericRepository<FeedSession>(context);
        public IGenericRepository<Food> FoodRepository => foodRepository ??= new GenericRepository<Food>(context);
        public IGenericRepository<GrowthBatch> GrowthBatchRepository => growthBatchRepository ??= new GenericRepository<GrowthBatch>(context);
        public IGenericRepository<GrowthStage> GrowthStageRepository => growthStageRepository ??= new GenericRepository<GrowthStage>(context);
        public IGenericRepository<HealthLog> HealthLogRepository => healthLogRepository ??= new GenericRepository<HealthLog>(context);
        public IGenericRepository<HealthLogDetail> HealthLogDetailRepository => healthLogDetailRepository ??= new GenericRepository<HealthLogDetail>(context);
        public IGenericRepository<Notification> NotificationRepository => notificationRepository ??= new GenericRepository<Notification>(context);
        public IGenericRepository<QuantityLog> QuantityLogRepository => quantityLogRepository ??= new GenericRepository<QuantityLog>(context);
        public IGenericRepository<Request> RequestRepository => requestRepository ??= new GenericRepository<Request>(context);
        public IGenericRepository<SubCategory> SubCategoryRepository => subCategoryRepository ??= new GenericRepository<SubCategory>(context);
        public IGenericRepository<Domain.Entities.Task> TaskRepository => taskRepository ??= new GenericRepository<Domain.Entities.Task>(context);
        public IGenericRepository<TaskLog> TaskLogRepository => taskLogRepository ??= new GenericRepository<TaskLog>(context);
        public IGenericRepository<User> UserRepository => userRepository ??= new GenericRepository<User>(context);
        public IGenericRepository<WareTransaction> WareTransactionRepository => wareTransactionRepository ??= new GenericRepository<WareTransaction>(context);
        public IGenericRepository<Warehouse> WarehouseRepository => warehouseRepository ??= new GenericRepository<Warehouse>(context);
        public IGenericRepository<RevokedToken> RevokedTokenRepository => revokedTokenRepository ??= new GenericRepository<RevokedToken>(context);
        public IGenericRepository<InventoryReceipt> InventoryReceiptRepository => inventoryReceiptRepository ??= new GenericRepository<InventoryReceipt>(context);
        public IGenericRepository<InventoryReceiptDetail> InventoryReceiptDetailRepository => inventoryReceiptDetailRepository ??= new GenericRepository<InventoryReceiptDetail>(context);
        public IGenericRepository<InventoryRequest> InventoryRequestRepository => inventoryRequestRepository ??= new GenericRepository<InventoryRequest>(context);
        public IGenericRepository<InventoryRequestDetail> InventoryRequestDetailRepository => inventoryRequestDetailRepository ??= new GenericRepository<InventoryRequestDetail>(context);
        public IGenericRepository<Medicine> MedicineRepository => medicineRepository ??= new GenericRepository<Medicine>(context);
        public IGenericRepository<NutritionPlan> NutritionPlanRepository => nutritionPlanRepository ??= new GenericRepository<NutritionPlan>(context);
        public IGenericRepository<NutritionPlanDetail> NutritionPlanDetailRepository => nutritionPlanDetailRepository ??= new GenericRepository<NutritionPlanDetail>(context);
        public IGenericRepository<Resource> ResourceRepository => resourceRepository ??= new GenericRepository<Resource>(context);
        public IGenericRepository<ResourceSupplier> ResourceSupplierRepository => resourceSupplierRepository ??= new GenericRepository<ResourceSupplier>(context);
        public IGenericRepository<Shift> ShiftRepository => shiftRepository ??= new GenericRepository<Shift>(context);
        public IGenericRepository<ShiftSchedule> ShiftScheduleRepository => shiftScheduleRepository ??= new GenericRepository<ShiftSchedule>(context);
        public IGenericRepository<TaskHarvest> TaskHarvestRepository => taskHarvestRepository ??= new GenericRepository<TaskHarvest>(context);
        public IGenericRepository<TaskLocation> TaskLocationRepository => taskLocationRepository ??= new GenericRepository<TaskLocation>(context);
        public IGenericRepository<TaskRequest> TaskRequestRepository => taskRequestRepository ??= new GenericRepository<TaskRequest>(context);
        public IGenericRepository<TaskResource> TaskResourceRepository => taskResourceRepository ??= new GenericRepository<TaskResource>(context);
        public IGenericRepository<TaskSchedule> TaskScheduleRepository => taskScheduleRepository ??= new GenericRepository<TaskSchedule>(context);
        public IGenericRepository<TemplateCriterion> TemplateCriterionRepository => templateCriterionRepository ??= new GenericRepository<TemplateCriterion>(context);
        public IGenericRepository<VaccineLog> VaccineLogRepository => vaccineLogRepository ??= new GenericRepository<VaccineLog>(context);
        public IGenericRepository<WarePermission> WarePermissionRepository => warePermissionRepository ??= new GenericRepository<WarePermission>(context);
        public IGenericRepository<WareStock> WareStockRepository => wareStockRepository ??= new GenericRepository<WareStock>(context);
        public IGenericRepository<Supplier> SupplierRepository => supplierRepository ??= new GenericRepository<Supplier>(context);
        public IGenericRepository<SystemConfig> SystemConfigRepository => systemConfigRepository ??= new GenericRepository<SystemConfig>(context);

        public void Save()
        {
            var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(e => e != ValidationResult.Success)
                .ToArray();
            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(Environment.NewLine,
                    validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
                throw new Exception(exceptionMessage);
            }
            context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var validationErrors = context.ChangeTracker.Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(e => e != ValidationResult.Success)
                .ToArray();
            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(Environment.NewLine,
                    validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));
                throw new Exception(exceptionMessage);
            }

            return context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action), "Transaction action cannot be null");

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                T result = await action(); 
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
