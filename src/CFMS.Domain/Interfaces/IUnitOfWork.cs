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
        IGenericRepository<BreedingArea> BreedingAreaRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<ChickenBatch> ChickenBatchRepository { get; }
        IGenericRepository<ChickenCoop> ChickenCoopRepository { get; }
        IGenericRepository<CoopEquipment> CoopEquipmentRepository { get; }
        IGenericRepository<Equipment> EquipmentRepository { get; }
        IGenericRepository<Farm> FarmRepository { get; }
        IGenericRepository<FarmEmployee> FarmEmployeeRepository { get; }
        IGenericRepository<Food> FoodRepository { get; }
        IGenericRepository<HealthLog> HealthLogRepository { get; }
        IGenericRepository<HealthLogDetail> HealthLogDetailRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<QuantityLog> QuantityLogRepository { get; }
        IGenericRepository<Request> RequestRepository { get; }
        IGenericRepository<SubCategory> SubCategoryRepository { get; }
        IGenericRepository<Task> TaskRepository { get; }
        IGenericRepository<TaskLog> TaskLogRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<VaccinationEmployee> VaccinationEmployeeRepository { get; }
        IGenericRepository<WareTransaction> WareTransactionRepository { get; }
        IGenericRepository<Warehouse> WarehouseRepository { get; }
        IGenericRepository<RevokedToken> RevokedTokenRepository { get; }

        void Save();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action);
    }
}
