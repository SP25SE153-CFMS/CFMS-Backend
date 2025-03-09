using CFMS.Domain.Interfaces;
using CFMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = CFMS.Domain.Entities.Task;
using CFMS.Infrastructure.Persistence;

namespace CFMS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private CfmsDbContext context;
        private IDbContextTransaction _transaction;

        private GenericRepository<Assignment> assignmentRepository;
        private GenericRepository<Attendance> attendanceRepository;
        private GenericRepository<BreedingArea> breedingAreaRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<ChickenBatch> chickenBatchRepository;
        private GenericRepository<ChickenCoop> chickenCoopRepository;
        private GenericRepository<CoopEquipment> coopEquipmentRepository;
        private GenericRepository<DailyTask> dailyTaskRepository;
        private GenericRepository<Equipment> equipmentRepository;
        private GenericRepository<Farm> farmRepository;
        private GenericRepository<FarmEmployee> farmEmployeeRepository;
        private GenericRepository<FeedSchedule> feedScheduleRepository;
        private GenericRepository<Flock> flockRepository;
        private GenericRepository<FlockNutrition> flockNutritionRepository;
        private GenericRepository<Food> foodRepository;
        private GenericRepository<HarvestDetail> harvestDetailRepository;
        private GenericRepository<HarvestLog> harvestLogRepository;
        private GenericRepository<HarvestProduct> harvestProductRepository;
        private GenericRepository<HarvestTask> harvestTaskRepository;
        private GenericRepository<HealthLog> healthLogRepository;
        private GenericRepository<HealthLogDetail> healthLogDetailRepository;
        private GenericRepository<Notification> notificationRepository;
        private GenericRepository<Nutrition> nutritionRepository;
        private GenericRepository<Performance> performanceRepository;
        private GenericRepository<Product> productRepository;
        private GenericRepository<QuantityLog> quantityLogRepository;
        private GenericRepository<Request> requestRepository;
        private GenericRepository<RequestDetail> requestDetailRepository;
        private GenericRepository<Salary> salaryRepository;
        private GenericRepository<StockReceipt> stockReceiptRepository;
        private GenericRepository<SubCategory> subCategoryRepository;
        private GenericRepository<Task> taskRepository;
        private GenericRepository<TaskDetail> taskDetailRepository;
        private GenericRepository<TaskEvaluation> taskEvaluationRepository;
        private GenericRepository<TaskLog> taskLogRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<VaccinationEmployee> vaccinationEmployeeRepository;
        private GenericRepository<VaccinationLog> vaccinationLogRepository;
        private GenericRepository<Vaccine> vaccineRepository;
        private GenericRepository<WareTransaction> wareTransactionRepository;
        private GenericRepository<Warehouse> warehouseRepository;
        private GenericRepository<WarehousePermission> warehousePermissionRepository;
        private GenericRepository<WarehouseStock> warehouseStockRepository;

        public UnitOfWork(CfmsDbContext _context)
        {
            context = _context;
        }

        public IGenericRepository<Assignment> AssignmentRepository => assignmentRepository ??= new GenericRepository<Assignment>(context);
        public IGenericRepository<Attendance> AttendanceRepository => attendanceRepository ??= new GenericRepository<Attendance>(context);
        public IGenericRepository<BreedingArea> BreedingAreaRepository => breedingAreaRepository ??= new GenericRepository<BreedingArea>(context);
        public IGenericRepository<Category> CategoryRepository => categoryRepository ??= new GenericRepository<Category>(context);
        public IGenericRepository<ChickenBatch> ChickenBatchRepository => chickenBatchRepository ??= new GenericRepository<ChickenBatch>(context);
        public IGenericRepository<ChickenCoop> ChickenCoopRepository => chickenCoopRepository ??= new GenericRepository<ChickenCoop>(context);
        public IGenericRepository<CoopEquipment> CoopEquipmentRepository => coopEquipmentRepository ??= new GenericRepository<CoopEquipment>(context);
        public IGenericRepository<DailyTask> DailyTaskRepository => dailyTaskRepository ??= new GenericRepository<DailyTask>(context);
        public IGenericRepository<Equipment> EquipmentRepository => equipmentRepository ??= new GenericRepository<Equipment>(context);
        public IGenericRepository<Farm> FarmRepository => farmRepository ??= new GenericRepository<Farm>(context);
        public IGenericRepository<FarmEmployee> FarmEmployeeRepository => farmEmployeeRepository ??= new GenericRepository<FarmEmployee>(context);
        public IGenericRepository<FeedSchedule> FeedScheduleRepository => feedScheduleRepository ??= new GenericRepository<FeedSchedule>(context);
        public IGenericRepository<Flock> FlockRepository => flockRepository ??= new GenericRepository<Flock>(context);
        public IGenericRepository<FlockNutrition> FlockNutritionRepository => flockNutritionRepository ??= new GenericRepository<FlockNutrition>(context);
        public IGenericRepository<Food> FoodRepository => foodRepository ??= new GenericRepository<Food>(context);
        public IGenericRepository<HarvestDetail> HarvestDetailRepository => harvestDetailRepository ??= new GenericRepository<HarvestDetail>(context);
        public IGenericRepository<HarvestLog> HarvestLogRepository => harvestLogRepository ??= new GenericRepository<HarvestLog>(context);
        public IGenericRepository<HarvestProduct> HarvestProductRepository => harvestProductRepository ??= new GenericRepository<HarvestProduct>(context);
        public IGenericRepository<HarvestTask> HarvestTaskRepository => harvestTaskRepository ??= new GenericRepository<HarvestTask>(context);
        public IGenericRepository<HealthLog> HealthLogRepository => healthLogRepository ??= new GenericRepository<HealthLog>(context);
        public IGenericRepository<HealthLogDetail> HealthLogDetailRepository => healthLogDetailRepository ??= new GenericRepository<HealthLogDetail>(context);
        public IGenericRepository<SubCategory> SubCategoryRepository => subCategoryRepository ??= new GenericRepository<SubCategory>(context);
        public IGenericRepository<Task> TaskRepository => taskRepository ??= new GenericRepository<Task>(context);
        public IGenericRepository<TaskDetail> TaskDetailRepository => taskDetailRepository ??= new GenericRepository<TaskDetail>(context);
        public IGenericRepository<TaskEvaluation> TaskEvaluationRepository => taskEvaluationRepository ??= new GenericRepository<TaskEvaluation>(context);
        public IGenericRepository<TaskLog> TaskLogRepository => taskLogRepository ??= new GenericRepository<TaskLog>(context);
        public IGenericRepository<User> UserRepository => userRepository ??= new GenericRepository<User>(context);
        public IGenericRepository<VaccinationEmployee> VaccinationEmployeeRepository => vaccinationEmployeeRepository ??= new GenericRepository<VaccinationEmployee>(context);
        public IGenericRepository<VaccinationLog> VaccinationLogRepository => vaccinationLogRepository ??= new GenericRepository<VaccinationLog>(context);
        public IGenericRepository<Vaccine> VaccineRepository => vaccineRepository ??= new GenericRepository<Vaccine>(context);
        public IGenericRepository<WareTransaction> WareTransactionRepository => wareTransactionRepository ??= new GenericRepository<WareTransaction>(context);
        public IGenericRepository<Warehouse> WarehouseRepository => warehouseRepository ??= new GenericRepository<Warehouse>(context);
        public IGenericRepository<WarehousePermission> WarehousePermissionRepository => warehousePermissionRepository ??= new GenericRepository<WarehousePermission>(context);
        public IGenericRepository<WarehouseStock> WarehouseStockRepository => warehouseStockRepository ??= new GenericRepository<WarehouseStock>(context);
        public IGenericRepository<Notification> NotificationRepository => notificationRepository ??= new GenericRepository<Notification>(context);
        public IGenericRepository<Nutrition> NutritionRepository => nutritionRepository ??= new GenericRepository<Nutrition>(context);
        public IGenericRepository<Performance> PerformanceRepository => performanceRepository ??= new GenericRepository<Performance>(context);
        public IGenericRepository<Product> ProductRepository => productRepository ??= new GenericRepository<Product>(context);
        public IGenericRepository<QuantityLog> QuantityLogRepository => quantityLogRepository ??= new GenericRepository<QuantityLog>(context);
        public IGenericRepository<Request> RequestRepository => requestRepository ??= new GenericRepository<Request>(context);
        public IGenericRepository<RequestDetail> RequestDetailRepository => requestDetailRepository ??= new GenericRepository<RequestDetail>(context);
        public IGenericRepository<Salary> SalaryRepository => salaryRepository ??= new GenericRepository<Salary>(context);
        public IGenericRepository<StockReceipt> StockReceiptRepository => stockReceiptRepository ??= new GenericRepository<StockReceipt>(context);

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
