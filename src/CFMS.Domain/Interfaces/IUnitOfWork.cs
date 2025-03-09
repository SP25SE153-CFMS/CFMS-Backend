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
        IGenericRepository<Assignment> AssignmentRepository { get; }
        IGenericRepository<Attendance> AttendanceRepository { get; }
        IGenericRepository<BreedingArea> BreedingAreaRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<ChickenBatch> ChickenBatchRepository { get; }
        IGenericRepository<ChickenCoop> ChickenCoopRepository { get; }
        IGenericRepository<CoopEquipment> CoopEquipmentRepository { get; }
        IGenericRepository<DailyTask> DailyTaskRepository { get; }
        IGenericRepository<Equipment> EquipmentRepository { get; }
        IGenericRepository<Farm> FarmRepository { get; }
        IGenericRepository<FarmEmployee> FarmEmployeeRepository { get; }
        IGenericRepository<FeedSchedule> FeedScheduleRepository { get; }
        IGenericRepository<Flock> FlockRepository { get; }
        IGenericRepository<FlockNutrition> FlockNutritionRepository { get; }
        IGenericRepository<Food> FoodRepository { get; }
        IGenericRepository<HarvestDetail> HarvestDetailRepository { get; }
        IGenericRepository<HarvestLog> HarvestLogRepository { get; }
        IGenericRepository<HarvestProduct> HarvestProductRepository { get; }
        IGenericRepository<HarvestTask> HarvestTaskRepository { get; }
        IGenericRepository<HealthLog> HealthLogRepository { get; }
        IGenericRepository<HealthLogDetail> HealthLogDetailRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<Nutrition> NutritionRepository { get; }
        IGenericRepository<Performance> PerformanceRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<QuantityLog> QuantityLogRepository { get; }
        IGenericRepository<Request> RequestRepository { get; }
        IGenericRepository<RequestDetail> RequestDetailRepository { get; }
        IGenericRepository<Salary> SalaryRepository { get; }
        IGenericRepository<StockReceipt> StockReceiptRepository { get; }
        IGenericRepository<SubCategory> SubCategoryRepository { get; }
        IGenericRepository<Task> TaskRepository { get; }
        IGenericRepository<TaskDetail> TaskDetailRepository { get; }
        IGenericRepository<TaskEvaluation> TaskEvaluationRepository { get; }
        IGenericRepository<TaskLog> TaskLogRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<VaccinationEmployee> VaccinationEmployeeRepository { get; }
        IGenericRepository<VaccinationLog> VaccinationLogRepository { get; }
        IGenericRepository<Vaccine> VaccineRepository { get; }
        IGenericRepository<WareTransaction> WareTransactionRepository { get; }
        IGenericRepository<Warehouse> WarehouseRepository { get; }
        IGenericRepository<WarehousePermission> WarehousePermissionRepository { get; }
        IGenericRepository<WarehouseStock> WarehouseStockRepository { get; }

        void Save();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
